using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class RegistrationController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
