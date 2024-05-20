using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class BatchFeeController : Controller
    {
        public readonly ApplicationDbContext _db;
        public BatchFeeController(ApplicationDbContext db)
        {
            _db=db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var model = _db.BatchFee.Select(p => new BatchFeeViewModel
            {
                Id = p.Id,
                BatchId = p.BatchId,     
                batchList= p.Batch.Name,
                FeeAmount = p.FeeAmount,
                EffectiveDate = p.EffectiveDate
            }).ToList();
            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create() {

            // var batches = _db.Batch.Where(c => !_db.BatchFee.Any(cf => cf.BatchId == c.Id)).ToList();
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");

            return View();
        }
        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(BatchFeeViewModel obj) 
        {
            if (ModelState.IsValid)
            {
                var courseFee = new BatchFee
                {
                    Id = Guid.NewGuid(),
                    BatchId = obj.BatchId,
                    FeeAmount = obj.FeeAmount,
                    EffectiveDate = obj.EffectiveDate,
                };
                _db.BatchFee.Add(courseFee);
                _db.SaveChanges(); 
                TempData["success"] = "Fee Added successfully";
                return RedirectToAction("index");
            }
            else
            {
                var courses = _db.Course.ToList();
                ViewBag.CourseList = new SelectList(courses, "Id", "Title");
                return View();
            }
        
        }

        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Update(Guid? id) 
        { 
            if(id==null)
            {
                return  NotFound();
            }
            var courseFee = _db.BatchFee.Find(id);
            if (courseFee == null) {
                return BadRequest();
            }
                BatchFeeViewModel model = new BatchFeeViewModel {
                    Id = courseFee.Id,
                    BatchId = courseFee.BatchId,
                   // BatchList= _db.Batch.FirstOrDefault(c => c.Id == courseFee.BatchId).Title,
                    FeeAmount = courseFee.FeeAmount,
                    EffectiveDate = courseFee.EffectiveDate,
                };         

            return View(model); }

        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(BatchFeeViewModel  courseFeeViewModel)
        {
            if (ModelState.IsValid)
            {
                BatchFee courseFee = new BatchFee()
                {
                    Id = courseFeeViewModel.Id,
                    BatchId = courseFeeViewModel.BatchId,
                    FeeAmount = courseFeeViewModel.FeeAmount,
                    EffectiveDate = courseFeeViewModel.EffectiveDate,
                };
                _db.BatchFee.Update(courseFee);
                _db.SaveChanges();
                TempData["success"] = "Course Updated successfully";
                return RedirectToAction("index");

            }

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseFee = _db.BatchFee.Find(id);
            if (courseFee == null)
            {
                return BadRequest();
            }
            BatchFeeViewModel model = new BatchFeeViewModel
            {
                Id = courseFee.Id,
                BatchId = courseFee.BatchId,
             //   BatchList = _db.Course.FirstOrDefault(c => c.Id == courseFee.BatchId).Title,
                FeeAmount = courseFee.FeeAmount,
                EffectiveDate = courseFee.EffectiveDate,
            };

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid? id)
        {
            BatchFee? courseFee = _db.BatchFee.Find(id);
            if (courseFee == null)
            {
                return NotFound();
            }
            _db.BatchFee.Remove(courseFee);
            _db.SaveChanges();
            TempData["success"] = "Course Fee deleted successfully";
            return RedirectToAction("index");
        }

    }
}
