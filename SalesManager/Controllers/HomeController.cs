using Microsoft.AspNetCore.Mvc;

namespace SalesManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello World");
        }
    }
}
