using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
