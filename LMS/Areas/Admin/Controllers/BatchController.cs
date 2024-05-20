using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class BatchController : Controller
    {
        public readonly ApplicationDbContext _db;
        public BatchController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
           var BatchList = _db.Batch.Select(p => new BatchViewModel {
                
                Id = p.Id,
                CourseId= p.CourseId,
                CourseTitle= p.Course.Title,
                Name = p.Name,
                StartDate=p.StartDate,
                EndDate=p.EndDate,
                Capacity= p.Capacity,
                Status= p.Status,
                Description= p.Description,

            }).ToList();
            
            return View(BatchList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var courses= _db.Course.ToList();           
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(BatchViewModel obj)
        {
            if (ModelState.IsValid) 
            {
                Batch batch = new Batch {
                Id = Guid.NewGuid(),
                CourseId = obj.CourseId,
                Name = obj.Name,
                StartDate= obj.StartDate,
                EndDate= obj.EndDate,
                Capacity= obj.Capacity,
                Status= obj.Status,
                Description= obj.Description,
                };

                _db.Batch.Add(batch);

                _db.SaveChanges();
                TempData["success"] = "Batch created successfully";
                return RedirectToAction("index");
            }
            else
            {
                var courses = _db.Course.ToList();
                ViewBag.CourseList = new SelectList(courses, "Id", "Title");
                ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
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

            Batch batch = _db.Batch.Find(Id);
            if (batch == null)
            {
                return NotFound();
            }

            var model = new BatchViewModel {
                Id= batch.Id,
                CourseId = batch.CourseId,
                Name = batch.Name,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate,
                Capacity = batch.Capacity,
                Status = batch.Status,
                Description = batch.Description,
            };
            var courses = _db.Course.ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                 };
            return View(model);
        }



        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(BatchViewModel obj)
        {
           
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                Batch batch = new Batch
                {
                    Id = obj.Id,
                    CourseId = obj.CourseId,
                    Name = obj.Name,
                    StartDate = obj.StartDate,
                    EndDate = obj.EndDate,
                    Capacity = obj.Capacity,
                    Status = obj.Status,
                    Description = obj.Description,
                };
                _db.Batch.Update(batch);
                _db.SaveChanges();
                TempData["success"] = "Batch Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                var courses = _db.Course.ToList();
                ViewBag.CourseList = new SelectList(courses, "Id", "Title");
                ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
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

            Batch batch = _db.Batch.Find(Id);
            if (batch == null)
            {
                return NotFound();
            }

            var model = new BatchViewModel {
                Id= batch.Id,
                CourseId = batch.CourseId,
                Name = batch.Name,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate,
                Capacity = batch.Capacity,
                Status = batch.Status,
                Description = batch.Description,
            };
            var courses = _db.Course.ToList();
            ViewBag.CourseList = new SelectList(courses, "Id", "Title");
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Open" , Text = "Open"},
                new SelectListItem{ Value="Closed" , Text = "Closed"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                 };
            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]    
        public IActionResult DeletePost(Guid Id)
        {
            Batch? batch = _db.Batch.Find(Id);
            if (batch == null)
            {
                return NotFound();
            }
            _db.Batch.Remove(batch);
            _db.SaveChanges();
            TempData["success"] = "Batch deleted successfully";
            return RedirectToAction("index");
        }

    }
}
