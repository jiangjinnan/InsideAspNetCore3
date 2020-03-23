using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class HomeController : Controller
    {
        private readonly IFoo _foo;
        public HomeController(IFoo foo) => _foo = foo;

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.Foo = _foo;
            return View();
        }
    }
}
