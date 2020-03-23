using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Principal;
using System.Security.Claims;

namespace App
{
    public class Program
    {
        private static Dictionary<string, string> _accounts;

        static Program()
        {
            _accounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _accounts.Add("Foo", "password");
            _accounts.Add("Bar", "password");
            _accounts.Add("Baz", "password");
        }
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting()
                        .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie())
                    .Configure(app => app
                        .UseAuthentication()
                        .UseRouting()
                        .UseEndpoints(endpoints =>
                        {
                            endpoints.Map(pattern: "/", RenderHomePageAsync);
                            endpoints.Map("Account/Login", SignInAsync);
                            endpoints.Map("Account/Logout", SignOutAsync);
                        })))
                .Build()
                .Run();
        }

        public static async Task RenderHomePageAsync(HttpContext context)
        {
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                await context.Response.WriteAsync(
                    @"<html>
                    <head><title>Index</title></head>
                    <body>" +
                            $"<h3>Welcome {context.User.Identity.Name}</h3>" +
                            @"<a href='Account/Logout'>Sign Out</a>
                    </body>
                </html>");
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
                var userName = context.Request.Form["username"];
                var password = context.Request.Form["password"];
                if (_accounts.TryGetValue(userName, out var pwd) && pwd == password)
                {
                    var identity = new GenericIdentity(userName, "Passord");
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
            context.Response.Redirect("/");
        }
    }
}