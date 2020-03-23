using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace App
{
    public class HomeController
    {
        private readonly IStringLocalizer _localizer;
        public HomeController(IStringLocalizerFactory localizerFactory) => _localizer = localizerFactory.Create("SharedResource", "App");

        [HttpGet("/")]
        public string Index() => _localizer.GetString("Greeting");
    }
}
