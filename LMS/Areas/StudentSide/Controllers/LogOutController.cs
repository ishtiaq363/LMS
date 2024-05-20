using LMS.DataAccess.Data;
using LMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.StudentSide.Controllers
{
    public class LogOutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LogOutController(ApplicationDbContext db,  SignInManager<IdentityUser> signInManager)
        { 
            _db = db;
            _signInManager = signInManager;
        }
        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
