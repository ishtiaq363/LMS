using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
