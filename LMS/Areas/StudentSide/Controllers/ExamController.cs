using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.StudentSide.Controllers;

public class ExamController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ExamController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
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

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        else { 


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
        ViewBag.Batch = batch;
        if (batch != null)
        {
            var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
            ViewBag.Notifications = notification;
        }
        var examResquestList = _db.Exam.Where(p => p.Status == "Accepted" && p.StudentId == currentUser.Id).Select(p => new ExamViewModel
        {
            Id = p.Id,
            StudentId = p.StudentId,
            BatchId = p.BatchId,
            ExamType = p.ExamType,
            AssessmentDate = p.AssessmentDate,
            AssessmentSource = p.AssessmentSource,
            StartTime = p.StartTime,
            EndTime = p.EndTime,
            TotalMarks = p.TotalMarks,
            Passingmarks = p.Passingmarks,
            UploadPaper = p.UploadPaper,
            Status = p.Status
        }).ToList();

        ViewBag.Requests = examResquestList;
        return View();
    }

        ///////////////////////////////


    }

    [Area("StudentSide")]
    [Authorize(Roles = SD.Role_Student)]
    [HttpPost]
    public async Task<IActionResult> SubmitPaper(IFormFile file, Guid examId, Guid studentId, Guid batchId, string examType)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty.");
        }
              
        String obj = "";
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        if (file != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string StudentPath = Path.Combine(wwwRootPath, @"Submissions\");

            using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            obj = @"\Submissions\" + fileName;

        }

        Exam exam = new Exam
        {
            Id = examId,
            StudentId = studentId,
            BatchId = batchId,
            ExamType = examType,
            UploadPaper=obj,
            Status = "Submitted"
        };
        _db.Exam.Update(exam);
        _db.SaveChanges();
        TempData["success"] = "Exam is Uloaded successfully";
       

        return Ok(); 
    }


}