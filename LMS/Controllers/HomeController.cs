using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMSWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
                return View();
        }

        public IActionResult Dashboard()
        {
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            if (!roles.Any())
            {
                return   RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            foreach (var role in roles)
            {

                if (role == "Admin")
                {

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (role == "Student")
                {
                    return RedirectToAction("Index", "Home", new { area = "StudentSide" });
                }
            }
            return View();
        }
    }
}
