using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class HomeController : Controller
    {
        private readonly IHostApplicationLifetime _lifetime;

        public HomeController(IHostApplicationLifetime lifetime, IFoo foo, IBar bar1, IBar bar2, IBaz baz1, IBaz baz2)
            => _lifetime = lifetime;

        [HttpGet("/index")]
        public void Index() { }

        [HttpGet("/stop")]
        public void Stop() => _lifetime.StopApplication();
    }

}
