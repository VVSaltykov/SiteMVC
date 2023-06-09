using Microsoft.AspNetCore.Mvc;

namespace SiteMVC.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
