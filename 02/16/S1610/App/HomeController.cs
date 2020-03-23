using Microsoft.AspNetCore.Mvc;

namespace App
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() => View();
    }
}
