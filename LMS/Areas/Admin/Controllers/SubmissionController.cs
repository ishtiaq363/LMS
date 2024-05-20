using LMS.DataAccess.Data;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers;

public class SubmissionController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public SubmissionController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _webHostEnvironment = webHostEnvironment;

    }


    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
       
            var submissionList = _db.Submission.Where(p => p.Status == "Submitted").Select(p => new SubmissionViewModel
            {
                Id = p.Id,
                StudentId = p.StudentId,
                AssessmentScheduleId = p.AssessmentScheduleId,
                FullName = p.Student.FullName,
                BatchName = p.AssessmentSchedule.Batch.Name,
               SubmissionUrl = p.SubmissionUrl,
                Status = p.Status
            }).ToList();

            ViewBag.Submission = submissionList;
            return View();
        

    }
}
