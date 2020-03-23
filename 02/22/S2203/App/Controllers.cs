using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace App
{
    public abstract class BaseController
    {
        public IStringLocalizer Localizer { get; }
        public BaseController(IStringLocalizerFactory localizerFactory)
            => Localizer = localizerFactory.Create(GetType());
    }

    public class FooController : BaseController
    {
        public FooController(IStringLocalizerFactory localizerFactory) : base(localizerFactory)
        { }

        [HttpGet("/foo")]
        public string Index() => Localizer.GetString("Greeting");
    }

    public class BarController : BaseController
    {
        public BarController(IStringLocalizerFactory localizerFactory) : base(localizerFactory)
        { }

        [HttpGet("/bar")]
        public string Index() => Localizer.GetString("Greeting");
    }
}
