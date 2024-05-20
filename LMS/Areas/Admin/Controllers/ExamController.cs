using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers;

public class ExamController : Controller
{
    public readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ExamController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpGet]
    public IActionResult GetSubjects(Guid Id)
    {
        List<SelectListItem> items = new List<SelectListItem>();

        var course = _db.Batch.Where(p => p.Id == Id).FirstOrDefault();


        var subjectIds = _db.CourseSubject.Where(p => p.CourseId == course.CourseId).Select(p => p.SubjectId).ToList();
        if (subjectIds.Any())
        {
            var subjects = _db.Subject.Where(s => subjectIds.Contains(s.Id)).ToList();
            // items.Add( new SelectList(subjects, "Id", "Name"));
            return Json(new SelectList(subjects, "Id", "Name"));
            //  return Json(new { CourseName = subject, Subjects = subjects });
        }


        return Json(null);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Index()
    {
        var examList = _db.Exam.Select(p => new ExamViewModel
        {

            Id = p.Id,
            BatchId = p.BatchId,
            BatchName = p.Batch.Name,
            StudentName = p.Student.FullName,
            StudentId = p.StudentId,
            ExamType = p.ExamType,
            AssessmentSource = p.AssessmentSource,
            AssessmentDate = p.AssessmentDate,
            StartTime = p.StartTime,
            EndTime = p.EndTime,
            TotalMarks = p.TotalMarks,
            Passingmarks = p.Passingmarks,
            ObtainedMarks =p.ObtainedMarks,
            UploadPaper = p.UploadPaper,
            Result = p.Result,
            Status = p.Status,

        }).ToList();

        return View(examList);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Create()
    {
        var batches = _db.Batch.ToList();
        ViewBag.BatchList = new SelectList(batches, "Id", "Name");

        //var subjects = _db.Subject.ToList();
        //ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

        var assessment = _db.Assessment.ToList();
        ViewBag.AssessmentList = new SelectList(assessment, "Id", "Name");

        ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Release" , Text = "Release"},
                new SelectListItem{ Value="Block" , Text = "Block"},

                 };

        return View();
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Create(AssessmentScheduleViewModel obj, IFormFile? file)
    {
        if (ModelState.IsValid && file != null)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string StudentPath = Path.Combine(wwwRootPath, @"papers\");

                using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.AssessmentSource = @"\papers\" + fileName;

            }
            AssessmentSchedule assessmentSchedule = new AssessmentSchedule
            {
                Id = Guid.NewGuid(),
                BatchId = obj.BatchId,
                SubjectId = obj.SubjectId,
                AssessmentId = obj.AssessmentId,
                AssessmentSource = obj.AssessmentSource,
                AssessmentDate = obj.AssessmentDate,
                StartTime = obj.StartTime,
                EndTime = obj.EndTime,
                TotalMarks = obj.TotalMarks,
                Passingmarks = obj.Passingmarks,
                Status = obj.Status,
            };

            _db.AssessmentSchedule.Add(assessmentSchedule);

            _db.SaveChanges();
            TempData["success"] = "Assessment created successfully";
            return RedirectToAction("index");
        }
        else
        {
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");

            //var subjects = _db.Subject.ToList();
            //ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

            var assessment = _db.Assessment.ToList();
            ViewBag.AssessmentList = new SelectList(assessment, "Id", "Name");

            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Release" , Text = "Release"},
                new SelectListItem{ Value="Block" , Text = "Block"},

                 };

            return View();
        }

    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Update(Guid Id)
    {
        if (Id == null)
        {
            return NotFound();
        }

        Exam exam = _db.Exam.Find(Id);
        if (exam == null)
        {
            return NotFound();
        }

        var model = new ExamViewModel
        {
            Id = exam.Id,
            BatchId = exam.BatchId,
            StudentId = exam.StudentId,
            ExamType  = exam.ExamType,
            AssessmentSource = exam.AssessmentSource,
            AssessmentDate = exam.AssessmentDate,
            StartTime = exam.StartTime,
            EndTime = exam.EndTime,
            TotalMarks = exam.TotalMarks,
            Passingmarks = exam.Passingmarks,
            ObtainedMarks = exam.ObtainedMarks,
            Result = exam.Result,
            Status = exam.Status,
        };
        var batches = _db.Batch.ToList();
        ViewBag.BatchList = new SelectList(batches, "Id", "Name");

        var students = _db.Student.ToList();
        ViewBag.StudentList = new SelectList(students, "Id", "FullName");
        ViewBag.ExamType = model.ExamType;
      

        ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Accepted" , Text = "Accepted"},
                new SelectListItem{ Value="Requested" , Text = "Requested"},
                new SelectListItem{ Value="Pending" , Text = "Pending"},
                new SelectListItem{ Value="Rejected" , Text = "Rejected"},
                 };

        return View(model);
    }



    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Update(ExamViewModel obj, IFormFile? file)
    {

        if (obj == null && file != null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string StudentPath = Path.Combine(wwwRootPath, @"Exams\");

                using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.AssessmentSource = @"\Exams\" + fileName;

            }

            Exam exam = new Exam
            {
                Id = obj.Id,
                BatchId = obj.BatchId,
                StudentId = obj.StudentId,
                ExamType = obj.ExamType,
                AssessmentSource = obj.AssessmentSource,
                AssessmentDate = obj.AssessmentDate,
                StartTime = obj.StartTime,
                EndTime = obj.EndTime,
                TotalMarks = obj.TotalMarks,
                Passingmarks = obj.Passingmarks,
                Status = obj.Status,
            };
            _db.Exam.Update(exam);
            _db.SaveChanges();
            TempData["success"] = "Exam is Uloaded successfully";
            return RedirectToAction("index");
        }
        else
        {
          
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");
            

            var students = _db.Student.ToList();
            ViewBag.StudentList = new SelectList(students, "Id", "FullName");
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Accepted" , Text = "Accepted"},
                new SelectListItem{ Value="Requested" , Text = "Requested"},
                new SelectListItem{ Value="Pending" , Text = "Pending"},
                new SelectListItem{ Value="Rejected" , Text = "Rejected"},
                 };
            return View();
        }
    }

    /// <summary>
    /// delete
    /// </summary>
    /// <returns></returns>

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Delete(Guid Id)
    {
        if (Id == null)
        {
            return NotFound();
        }

        AssessmentSchedule assessmentSchedule = _db.AssessmentSchedule.Find(Id);
        if (assessmentSchedule == null)
        {
            return NotFound();
        }

        var model = new AssessmentScheduleViewModel
        {
            Id = assessmentSchedule.Id,
            BatchId = assessmentSchedule.BatchId,
            SubjectId = assessmentSchedule.SubjectId,
            AssessmentId = assessmentSchedule.AssessmentId,
            AssessmentSource = assessmentSchedule.AssessmentSource,
            AssessmentDate = assessmentSchedule.AssessmentDate,
            StartTime = assessmentSchedule.StartTime,
            EndTime = assessmentSchedule.EndTime,
            TotalMarks = assessmentSchedule.TotalMarks,
            Passingmarks = assessmentSchedule.Passingmarks,
            Status = assessmentSchedule.Status,
        };
        var batches = _db.Batch.ToList();
        ViewBag.BatchList = new SelectList(batches, "Id", "Name");

        var subjects = _db.Subject.ToList();
        ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

        var assessment = _db.Assessment.ToList();
        ViewBag.AssessmentList = new SelectList(assessment, "Id", "Name");

        ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Release" , Text = "Release"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                 };
        return View(model);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(Guid Id)
    {
        AssessmentSchedule? assessmentSchedule = _db.AssessmentSchedule.Find(Id);
        if (assessmentSchedule == null)
        {
            return NotFound();
        }
        _db.AssessmentSchedule.Remove(assessmentSchedule);
        _db.SaveChanges();
        TempData["success"] = "Assessment deleted successfully";
        return RedirectToAction("index");
    }
}