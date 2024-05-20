using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Areas.Admin.Controllers;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]       
        public IActionResult Index()
        {
            var MessageList = _db.Message.Select(p => new MessageViewModel
            {

                Id = p.Id,
                Name = p.Name,
                Subject = p.Subject,
                Email = p.Email,
                Msg = p.Msg,               

            }).ToList();
            ViewBag.Messages = MessageList;
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult DeleteMessage(Guid? id)
        {
            try
            {
                Message? message = _db.Message.Find(id);
                if (message == null)
                {
                    return NotFound();
                }
                _db.Message.Remove(message);
                _db.SaveChanges();
                TempData["success"] = "Message deleted successfully";
               return Ok(" successfully");

            }
            catch (Exception ex)
            {
                TempData["error"] = "There is something wrong. Failed";
                return RedirectToAction("Payment");
               
            }
        }




    }
}


