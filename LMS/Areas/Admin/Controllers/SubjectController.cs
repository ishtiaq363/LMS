using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class SubjectController : Controller
    {
        public readonly ApplicationDbContext _db;
        public SubjectController(ApplicationDbContext db) {
        
            _db = db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var subjectList = _db.Subject.Select(p => new SubjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Level = p.Level,
                Status = p.Status,
                ResourceScope = p.ResourceScope
            }).ToList();

            return View(subjectList);
        }

        /// <summary>
        /// create
        /// </summary>
        /// <returns></returns>

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var model = new SubjectViewModel();
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
        [Authorize(Roles =SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(SubjectViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name,
                    Level = obj.Level,
                    Status = obj.Status,
                    ResourceScope = obj.ResourceScope,
                };
                _db.Subject.Add(subject);
                _db.SaveChanges();
                TempData["success"] = "Subject created successfully";
                return RedirectToAction("index");

            }
            else
            {
                ViewBag.AvailableOptions = new List<SelectListItem> 
                {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                 };
                ViewBag.AvailableLevels = new List<SelectListItem> 
                {
                new SelectListItem{ Value="Beginners" , Text = "Beginners"},
                new SelectListItem{ Value="Advance" , Text = "Advance"},

                };

                return View();
            }
        }



        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>


        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Update(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subject = _db.Subject.Find(id);
            if(subject == null)
            {
                return BadRequest();
            }
            var model = new SubjectViewModel {
            Id= subject.Id,
            Name= subject.Name,
            Level= subject.Level,
            Status= subject.Status,
            ResourceScope= subject.ResourceScope,
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
        [Authorize(Roles =SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(SubjectViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var subject = new Subject
                {

                    Id = obj.Id,
                    Name = obj.Name,
                    Level = obj.Level,
                    Status = obj.Status,
                    ResourceScope= obj.ResourceScope,
                };
                _db.Subject.Update(subject);
                _db.SaveChanges();
                TempData["success"] = "Subject Updated successfully";
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
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Delete(Guid? id) {

            if (id == null)
            {
                return NotFound();
            }
            var subject = _db.Subject.Find(id);
            if (subject == null)
            {
                return BadRequest();
            }
            var model = new SubjectViewModel
            {
                Id = subject.Id,
                Name = subject.Name,
                Level = subject.Level,
                Status = subject.Status,
                ResourceScope = subject.ResourceScope,
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
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid? id)
        {
            Subject? subject = _db.Subject.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            _db.Subject.Remove(subject);
            _db.SaveChanges();
            TempData["success"] = "Subject deleted successfully";
            return RedirectToAction("index");
        }


        [Area("admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult AssignSubjectToCourse()
        {
            return View();
        }
    }
}
