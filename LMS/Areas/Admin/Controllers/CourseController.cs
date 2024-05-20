using LMSWeb.Models;
using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        public readonly ApplicationDbContext _db;

        public CourseController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var courseList = _db.Course.Select(p=> new CourseViewModel {
            Id=p.Id,
            Title= p.Title,
            Description= p.Description,
            Duration = p.Duration,
            Price = p.Price,
            Prerequisites=p.Prerequisites,
            Level= p.Level,
            Status=p.Status            
            }).ToList();
          
            return View(courseList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var model = new CourseViewModel();
            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            ViewBag.AvailableLevels = new List<SelectListItem> {
                new SelectListItem{ Value="Beginners" , Text = "Beginners"},
                new SelectListItem{ Value="Advance" , Text = "Advance"},
                
            };

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(CourseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var course = new Course {                
                
                Id = Guid.NewGuid(),
                Title = obj.Title,
                Description=obj.Description,
                Duration= obj.Duration,
                Price = obj.Price,
                Prerequisites = obj.Prerequisites,
                Level = obj.Level,
                Status = obj.Status
                };
            _db.Course.Add(course);
                _db.SaveChanges();
                TempData["success"] = "Course created successfully";
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
                ViewBag.AvailableLevels = new List<SelectListItem> {
                new SelectListItem{ Value="Beginners" , Text = "Beginners"},
                new SelectListItem{ Value="Advance" , Text = "Advance"},

            };
                return View();
            }
        }

       

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Update(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = _db.Course.Find(id);
            if(course== null)
            {
                return NotFound();
            }

            var model = new CourseViewModel { 
            
                Id = course.Id,
                Title = course.Title,
                Description= course.Description,
                Duration = course.Duration,
                Status = course.Status,
                Level = course.Level,
                Prerequisites= course.Prerequisites,
                Price = course.Price
            };
            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            ViewBag.AvailableLevels = new List<SelectListItem> {
                new SelectListItem{ Value="Beginners" , Text = "Beginners"},
                new SelectListItem{ Value="Advance" , Text = "Advance"},

            };

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(CourseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {

                    Id = obj.Id,
                    Title = obj.Title,
                    Description = obj.Description,
                    Duration = obj.Duration,
                    Price = obj.Price,
                    Prerequisites = obj.Prerequisites,
                    Level = obj.Level,
                    Status = obj.Status
                };
                _db.Course.Update(course);
                _db.SaveChanges();
                TempData["success"] = "Course Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
                ViewBag.AvailableLevels = new List<SelectListItem> {
                new SelectListItem{ Value="Beginners" , Text = "Beginners"},
                new SelectListItem{ Value="Advance" , Text = "Advance"},

            };
                return View();
            }
        }


        /////////////////////////delete
        ///

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = _db.Course.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            var couseFromDb = new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Duration = course.Duration,
                Status = course.Status,
                Level = course.Level,
                Prerequisites = course.Prerequisites,
                Price = course.Price
            };

            return View(couseFromDb);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid? id)
        {
            Course? course = _db.Course.Find(id);
            if (course==null)
            {
                return NotFound();
            }
                _db.Course.Remove(course);
                _db.SaveChanges();
            TempData["success"] = "Course deleted successfully";
            return RedirectToAction("index");
        }

    }
}
