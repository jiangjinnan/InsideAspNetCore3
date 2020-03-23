using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace App
{
    public abstract class BaseController<T>
    {
        public IStringLocalizer Localizer { get; }
        public BaseController(IStringLocalizer<T> localizer) => Localizer = localizer;
    }

    public class FooController : BaseController<FooController>
    {
        public FooController(IStringLocalizer<FooController> localizer) : base(localizer)
        { }

        [HttpGet("/foo")]
        public string Index() => Localizer.GetString("Greeting");
    }

    public class BarController : BaseController<BarController>
    {
        public BarController(IStringLocalizer<BarController> localizer) : base(localizer)
        { }

        [HttpGet("/bar")]
        public string Index() => Localizer.GetString("Greeting");
    }
}
