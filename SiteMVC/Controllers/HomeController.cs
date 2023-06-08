using Microsoft.AspNetCore.Mvc;

namespace SiteMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
