using LMS.DataAccess.Data;

using LMSWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.StudentSide.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DashboardController( ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _signInManager  = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                string userEmail = user.Email; 
                var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                StudentViewModel studentVM = new StudentViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FullName,
                    FatherName = currentUser.FatherName,
                    DOB = currentUser.DOB,
                    Password = currentUser.Password,
                    RegistrationNo = currentUser.RegistrationNo,
                    MobileNo = currentUser.MobileNo,
                    Gender = currentUser.Gender,
                    Address = currentUser.Address,
                    City = currentUser.City,
                    Email = currentUser.Email,
                    Status = currentUser.Status,
                    ImageURL = currentUser.ImageURL,
                };
                ViewBag.User = studentVM;
            }
            return View();
        }
    }
}
