using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class CourseSubjectController : Controller
    {
        public readonly ApplicationDbContext _db;

        public CourseSubjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public IActionResult GetSubjects(Guid Id)
        {
            var courseSubjects = _db.CourseSubject.Where(cs=> cs.CourseId == Id).ToList();
            if (courseSubjects.Any())
            {
                var subjectIds = courseSubjects.Select(cs => cs.SubjectId).ToList();
                var subjects = _db.Subject.Where(s => !subjectIds.Contains(s.Id));
                return Json(new SelectList(subjects, "Id", "Name"));
            }
            else
            {
                var subjects = _db.Subject.ToList();
                return Json(new SelectList(subjects,"Id", "Name"));
            }
            
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var courseSubjectList = _db.CourseSubject.Select(p => new CourseSubjectViewModel
            {
                Id = p.Id,
                CourseId = p.CourseId,
                SubjectId = p.SubjectId,
                CourseName =p.crouse.Title,
                SubjectName = p.Subject.Name
            }).ToList();

            return View(courseSubjectList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var courses = _db.Course.ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            //var subjects = _db.Subject.ToList();
            //ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");
            
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(CourseSubjectViewModel obj)
        {
            if (ModelState.IsValid)
            {
                CourseSubject courseSubject = new CourseSubject
                {
                    Id = Guid.NewGuid(),
                    CourseId = obj.CourseId,
                    SubjectId = obj.SubjectId
                };

                _db.CourseSubject.Add(courseSubject);

                _db.SaveChanges();
                TempData["success"] = "Subject successfully assigned to Course";
                return RedirectToAction("index");
            }
            else
            {
                var courses = _db.Course.ToList();
                ViewBag.CourseList = new SelectList(courses, "Id", "Title");
                var subjects = _db.Subject.ToList();
                ViewBag.SujectList = new SelectList(subjects, "Id", "Name");
                return View();
            }

        }


        ///Update
        ///
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Update(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseSuject = _db.CourseSubject.Find(id);
            if (courseSuject == null)
            {
                return BadRequest();
            }
            var model = new CourseSubjectViewModel
            {
                Id = courseSuject.Id,
                CourseId = courseSuject.CourseId,
                SubjectId = courseSuject.SubjectId                
            };
            var courses = _db.Course.ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            var subjects = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(CourseSubjectViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var courseSubject = new CourseSubject
                {

                    Id = obj.Id,
                    CourseId = obj.CourseId,
                    SubjectId = obj.SubjectId,
                                   };
                _db.CourseSubject.Update(courseSubject);
                _db.SaveChanges();
                TempData["success"] = "Subject of course Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                var courses = _db.Course.ToList();
                ViewBag.CourseList = new SelectList(courses, "Id", "Title");
                var subjects = _db.Subject.ToList();
                ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

                return View();
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <returns></returns>

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            CourseSubject? courseSubject = _db.CourseSubject.Find(Id);
            if (courseSubject == null)
            {
                return NotFound();
            }

            var model = new CourseSubjectViewModel
            {
                Id = courseSubject.Id,
                CourseId = courseSubject.CourseId,
                SubjectId = courseSubject.SubjectId,                
            };
            var courses = _db.Course.ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            var subjects = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            CourseSubject? courseSubject = _db.CourseSubject.Find(Id);
            if (courseSubject == null)
            {
                return NotFound();
            }
            _db.CourseSubject.Remove(courseSubject);
            _db.SaveChanges();
            TempData["success"] = "Course with subject deleted successfully";
            return RedirectToAction("index");
        }

    }
}
