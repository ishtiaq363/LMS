using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Controllers
{
    public class ContactusController : Controller
    {
        public readonly ApplicationDbContext _db;
        public ContactusController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
       
       

       
        [HttpPost]
        public IActionResult Index(MessageViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message
                {
                    Id = Guid.NewGuid(),
                    Name = obj.Name,
                    Email = obj.Email,
                    Subject = obj.Subject,
                    Msg = obj.Msg,                   
                };

                _db.Message.Add(message);

                _db.SaveChanges();
                TempData["success"] = "Your Message submitted successfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }

        }
    }
}
