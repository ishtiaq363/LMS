using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class NotificationController : Controller
    {
        public readonly ApplicationDbContext _db;
        public NotificationController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var NotificationList = _db.Notification.Select(p => new NotificationViewModel
            {

                Id = p.Id,
                BatchId = p.BatchId,
                BatchName = p.Batch.Name,
                Title =p.Title,
                Content= p.Content,
                DateCreated=p.DateCreated
            }).ToList();

            return View(NotificationList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var batchList = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batchList, "Id", "Name");
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(NotificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Notification notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    BatchId = obj.BatchId,
                    Title = obj.Title,
                    Content = obj.Content,
                    DateCreated = obj.DateCreated
                };

                _db.Notification.Add(notification);
                _db.SaveChanges();
                TempData["success"] = "Notification created successfully";
                return RedirectToAction("index");
            }
            else
            {
                var batches = _db.Batch.ToList();
                ViewBag.BatchList = new SelectList(batches, "Id", "Name");
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

            Notification notification = _db.Notification.Find(Id);
            if (notification == null)
            {
                return NotFound();
            }

            var model = new NotificationViewModel
            {
                Id = notification.Id,
                BatchId = notification.BatchId,
                Title = notification.Title,
                Content = notification.Content,
                DateCreated = notification.DateCreated
            };
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");
            
            return View(model);
        }



        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(NotificationViewModel obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                Notification notification = new Notification
                {
                    Id = obj.Id,
                    BatchId = obj.BatchId,
                    Title = obj.Title,
                    Content = obj.Content,
                    DateCreated = obj.DateCreated
                };
                _db.Notification.Update(notification);
                _db.SaveChanges();
                TempData["success"] = "Notification Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                var batches = _db.Batch.ToList();
                ViewBag.BatchList = new SelectList(batches, "Id", "Name");

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

            Notification notification = _db.Notification.Find(Id);
            if (notification == null)
            {
                return NotFound();
            }

            var model = new NotificationViewModel
            {
                Id = notification.Id,
                BatchId = notification.BatchId,
                Title = notification.Title,
                Content = notification.Content,
                DateCreated = notification.DateCreated
            };
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            Notification? notification = _db.Notification.Find(Id);
            if (notification == null)
            {
                return NotFound();
            }
            _db.Notification.Remove(notification);
            _db.SaveChanges();
            TempData["success"] = "Notification deleted successfully";
            return RedirectToAction("index");
        }

    }
}
