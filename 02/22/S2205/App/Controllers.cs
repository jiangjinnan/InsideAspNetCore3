using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace App
{
    public class FooController
    {
        private readonly IStringLocalizer _localizer;
        public FooController(IStringLocalizer<FooController> localizer)
            => _localizer = localizer;

        [HttpGet("/foo")]
        public string Index() => _localizer.GetString("Greeting");
    }

    public class BarController
    {
        private readonly IStringLocalizer _localizer;
        public BarController(IStringLocalizer<BarController> localizer) => _localizer = localizer;

        [HttpGet("/bar")]
        public string Index() => _localizer.GetString("Greeting");
    }

}
