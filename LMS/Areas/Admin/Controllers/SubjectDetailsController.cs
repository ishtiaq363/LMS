using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class SubjectDetailsController : Controller
    {
        public readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public SubjectDetailsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var subjectDetailsList = _db.SubjectDetail.Select(subjectDetailsVM => new SubjectDetailsViewModel
            {
                Id = subjectDetailsVM.Id,
                SubjectId = subjectDetailsVM.SubjectId,
                ResourceType = subjectDetailsVM.ResourceType,
                ResourceUrl = subjectDetailsVM.ResourceUrl,
                Subject = subjectDetailsVM.Subject 
                
            }).ToList();
            return View(subjectDetailsList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var subjects = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");
            ViewBag.AvailableResourceType = new List<SelectListItem> {
                new SelectListItem{ Value="Notes" , Text = "Notes"},
                new SelectListItem{ Value="Video" , Text = "Video"},
                new SelectListItem{ Value="Url" , Text = "Url"},
                new SelectListItem{ Value="Audio" , Text = "Audio"},
                new SelectListItem{ Value="Image" , Text = "Image"},
            
            };           

            return View();
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(SubjectDetailsViewModel subjectDetailsVM, IFormFile? file)
        {
            if (ModelState.IsValid && file != null)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string StudentPath = Path.Combine(wwwRootPath, @"resources\subjects\");

                    using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    subjectDetailsVM.ResourceUrl = @"\resources\subjects\" + fileName;

                }
                SubjectDetail subjectDetail = new SubjectDetail
                {

                    Id = Guid.NewGuid(),
                    SubjectId = subjectDetailsVM.SubjectId,
                    ResourceType = subjectDetailsVM.ResourceType,
                    ResourceUrl = subjectDetailsVM.ResourceUrl,
                    
                   
                };

                _db.SubjectDetail.Add(subjectDetail);
                _db.SaveChanges();
                TempData["success"] = "Subject Details uploaded successfully";
                return RedirectToAction("index");


            }
            else
            {
                var subjects = _db.Subject.ToList();
                ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");
                ViewBag.AvailableResourceType = new List<SelectListItem> {
                new SelectListItem{ Value="Notes" , Text = "Notes"},
                new SelectListItem{ Value="Video" , Text = "Video"},
                new SelectListItem{ Value="Url" , Text = "Url"},
                new SelectListItem{ Value="Audio" , Text = "Audio"},
                new SelectListItem{ Value="Image" , Text = "Image"},
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
        public IActionResult Delete(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            SubjectDetail? subject = _db.SubjectDetail.Find(Id);
            if (subject == null)
            {
                return NotFound();
            }

            var model = new SubjectDetailsViewModel
            {
                Id = subject.Id,
                SubjectId = subject.SubjectId,
                ResourceType = subject.ResourceType,
                ResourceUrl = subject.ResourceUrl
               
            };

            var subjects = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");
            ViewBag.AvailableResourceType = new List<SelectListItem> {
                new SelectListItem{ Value="Notes" , Text = "Notes"},
                new SelectListItem{ Value="Video" , Text = "Video"},
                new SelectListItem{ Value="Url" , Text = "Url"},
                new SelectListItem{ Value="Audio" , Text = "Audio"},
                new SelectListItem{ Value="Image" , Text = "Image"},

            };
            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            SubjectDetail? subjectDetail = _db.SubjectDetail.Find(Id);
            if (subjectDetail == null)
            {
                return NotFound();
            }
            _db.SubjectDetail.Remove(subjectDetail);
            _db.SaveChanges();
            TempData["success"] = "Subject Detail Record Deleted successfully";
            return RedirectToAction("index");
        }


    }
}
