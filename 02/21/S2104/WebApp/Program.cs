using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseUrls("http://0.0.0.0:3721")
                    .Configure(app => app.Run(ProcessAsync)))
                .Build()
                .Run();

            static async Task ProcessAsync(HttpContext httpContext)
            {
                httpContext.Response.ContentType = "text/html";
                var html =
                @"<html>
                <body>
                    <ul id='contacts'></ul>
                    <script src='http://code.jquery.com/jquery-3.3.1.min.js'></script>
                    <script>
                    $(function()
                    {
                        var url = 'http://www.gux.com:8080/contacts';
                        $.getJSON(url, null, function(contacts) {
                            $.each(contacts, function(index, contact)
                            {
                                var html = '<li><ul>';
                                html += '<li>Name: ' + contact.Name + '</li>';
                                html += '<li>Phone No:' + contact.PhoneNo + '</li>';
                                html += '<li>Email Address: ' + contact.EmailAddress + '</li>';
                                html += '</ul>';
                                $('#contacts').append($(html));
                            });
                        });
                    });
                    </script >
                </body>
                </html>";
                await httpContext.Response.WriteAsync(html);
            }
        }
    }
}