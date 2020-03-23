using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddDbContext<UserDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AuthorizationDemo;Trusted_Connection=True;MultipleActiveResultSets=true"))
                        .AddRouting()
                        .AddAuthorization()
                        .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie())
                .Configure(app => app
                    .UseAuthentication()
                    .UseRouting()
                    .UseEndpoints(endpoints =>
                    {
                        endpoints.Map("", RenderHomePageAsync);
                        endpoints.Map("Account/Login", SignInAsync);
                        endpoints.Map("Account/Logout", SignOutAsync);
                        endpoints.Map("Account/AccessDenied", DenyAccessAysnc);
                    })))
                .Build()
                .Run();
        }

        public static async Task RenderHomePageAsync(HttpContext context)
        {
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                var requirement = new RolesAuthorizationRequirement(new string[] { "ADMIN" });
                var authorizationService = context.RequestServices.GetRequiredService<IAuthorizationService>();
                var result = await authorizationService.AuthorizeAsync(context.User, null, new IAuthorizationRequirement[] { requirement });
                if (result.Succeeded)
                {
                    await context.Response.WriteAsync(
                    @"<html>
                    <head><title>Index</title></head>
                    <body>" +
                            $"<h3>{context.User.Identity.Name}, you are authorized.</h3>" +
                            @"<a href='Account/Logout'>Sign Out</a>
                    </body>
                </html>");
                }
                else
                {
                    await context.ForbidAsync();
                }
            }
            else
            {
                await context.ChallengeAsync();
            }
        }


        public static async Task SignInAsync(HttpContext context)
        {
            if (string.Compare(context.Request.Method, "GET") == 0)
            {
                await RenderLoginPageAsync(context, null, null, null);
            }
            else
            {
                string userName = context.Request.Form["username"];
                string password = context.Request.Form["password"];
                var dbContext = context.RequestServices.GetRequiredService<UserDbContext>();
                var user = await dbContext.Users.Include(it => it.Roles).SingleOrDefaultAsync(it => it.UserName == userName.ToUpper());
                if (user?.Password == password)
                {
                    var identity = new GenericIdentity(userName, CookieAuthenticationDefaults.AuthenticationScheme);
                    foreach (var role in user.Roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role.NormalizedRoleName));
                    }
                    var principal = new ClaimsPrincipal(identity);
                    await context.SignInAsync(principal);
                }
                else
                {
                    await RenderLoginPageAsync(context, userName, password, "Invalid user name or password!");
                }
            }
        }

        private static Task RenderLoginPageAsync(HttpContext context, string userName, string password, string errorMessage)
        {
            context.Response.ContentType = "text/html";
            return context.Response.WriteAsync(
                @"<html>
                <head><title>Login</title></head>
                <body>
                    <form method='post'>" +
                            $"<input type='text' name='username' placeholder='User name' value = '{userName}' /> " +
                            $"<input type='password' name='password' placeholder='Password' value = '{password}' /> " +
                            @"<input type='submit' value='Sign In' />
                    </form>" +
                            $"<p style='color:red'>{errorMessage}</p>" +
                    @"</body>
            </html>");
        }

        public static async Task SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync();
            await context.ChallengeAsync(new AuthenticationProperties { RedirectUri = "/" });
        }
        public static Task DenyAccessAysnc(HttpContext context) => context.Response.WriteAsync(
            @"<html>
                <head><title>Index</title></head>
                <body>" +
                        $"<h3>{context.User.Identity.Name}, your access is denied.</h3>" +
                        @"<a href='/Account/Logout'>Sign Out</a>
                </body>
            </html>");
    }
}
