using LMS.DataAccess.Data;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.StudentSide.Controllers;

public class NotificationController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public NotificationController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _webHostEnvironment = webHostEnvironment;

    }

    [Area("StudentSide")]
    [Authorize(Roles = SD.Role_Student)]
    public async Task<IActionResult> Index()
    {
        /////////////////////////////
        ///
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        else
        {

            string userEmail = user.Email;
            var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
            ViewBag.currentUser = currentUser;
            var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);
            ViewBag.IsDetailInformationSaved = userDetails.Any();
            if (userDetails.Any())
            {
                ViewBag.StudentDetails = userDetails.FirstOrDefault();
            }


            var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
            if (batch != null)
            {
                var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
                ViewBag.Notifications = notification;
            }

            if (batch != null)
            {
                var course = _db.Batch.Where(b => b.Id == batch.BatchId).FirstOrDefault();
                ViewBag.Title = _db.Course.Where(c => c.Id == course.CourseId).Select(c => c.Title).FirstOrDefault();
                if (course != null)
                {
                    var subjects = _db.CourseSubject.Where(c => c.CourseId == course.CourseId).Select(c => c.SubjectId).ToList();
                    if (subjects.Any())
                    {
                        var subjectList = _db.Subject.Where(s => subjects.Contains(s.Id)).ToList();
                        ViewBag.Subject = subjectList;
                    }
                }
                

            }


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

            var payments = _db.Payment.Where(p => p.StudentId == currentUser.Id).ToList();
            ViewBag.Payments = payments;

            return View();
        }
        ///////////////////////////////


    }


}
