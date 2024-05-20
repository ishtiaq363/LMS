using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class AssessmentController : Controller
    {
        public readonly ApplicationDbContext _db;
        public AssessmentController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var AssessmentList = _db.Assessment.Select(p => new AssessmentViewModel
            {

                Id = p.Id,
                Name = p.Name,

            }).ToList();

            return View(AssessmentList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(AssessmentViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Assessment assessment = new Assessment
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name
                };

                _db.Assessment.Add(assessment);

                _db.SaveChanges();
                TempData["success"] = "Assessment Type created successfully";
                return RedirectToAction("index");
            }
            else
            {
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

            Assessment assessment = _db.Assessment.Find(Id);
            if (assessment == null)
            {
                return NotFound();
            }

            var model = new AssessmentViewModel
            {
                Id = assessment.Id,
                Name = assessment.Name
            };
           
            return View(model);
        }



        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(AssessmentViewModel obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                Assessment assessment = new Assessment
                {
                    Id = obj.Id,
                    Name = obj.Name,
                };
                _db.Assessment.Update(assessment);
                _db.SaveChanges();
                TempData["success"] = "Assessment Type Updated successfully";
                return RedirectToAction("index");
            }
            else
            {               
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

            Assessment assessment = _db.Assessment.Find(Id);
            if (assessment == null)
            {
                return NotFound();
            }

            var model = new AssessmentViewModel
            {
                Id = assessment.Id,
                Name = assessment.Name
            };

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            Assessment? assessment = _db.Assessment.Find(Id);
            if (assessment == null)
            {
                return NotFound();
            }
            _db.Assessment.Remove(assessment);
            _db.SaveChanges();
            TempData["success"] = "Assessment Type deleted successfully";
            return RedirectToAction("index");
        }

    }
}
