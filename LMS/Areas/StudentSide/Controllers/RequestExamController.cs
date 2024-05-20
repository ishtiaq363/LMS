using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.StudentSide.Controllers
{
    public class RequestExamController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RequestExamController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;

        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task<IActionResult> Index()
        {
            /////////////////////////////
           

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            else
            {

                string userEmail = user.Email;
                var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                ViewBag.currentUser = currentUser;
                var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);
                ViewBag.IsDetailInformationSaved = userDetails.Any();
                if (userDetails.Any())
                {
                    ViewBag.StudentDetails = userDetails.FirstOrDefault();
                }


                var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
                ViewBag.Batch = batch;
                if (batch != null)
                {
                    var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
                    ViewBag.Notifications = notification;
                }
                var examResquestList = _db.Exam.Select(p => new ExamViewModel
                {
                    Id = p.Id,                   
                    ExamType = p.ExamType,
                    Status = p.Status
                }).ToList();

                ViewBag.Requests = examResquestList;
                return View();
            }
           
            ///////////////////////////////


        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task< IActionResult> Create() {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            else
            {

                string userEmail = user.Email;
                var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                ViewBag.currentUser = currentUser;
                var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);
                ViewBag.IsDetailInformationSaved = userDetails.Any();
                if (userDetails.Any())
                {
                    ViewBag.StudentDetails = userDetails.FirstOrDefault();
                }


                var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
                ViewBag.Batch = batch;
                if (batch != null)
                {
                    var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
                    ViewBag.Notifications = notification;
                }
                ///////////////////////
                ///
                //Exam examViewModel = new StudentViewModel
                //{
                //    Id = currentUser.Id,
                //    FullName = currentUser.FullName,
                //    FatherName = currentUser.FatherName,
                //    DOB = currentUser.DOB,
                //    Password = currentUser.Password,
                //    RegistrationNo = currentUser.RegistrationNo,
                //    MobileNo = currentUser.MobileNo,
                //    Gender = currentUser.Gender,
                //    Address = currentUser.Address,
                //    City = currentUser.City,
                //    Email = currentUser.Email,
                //    Status = currentUser.Status,
                //    ImageURL = currentUser.ImageURL,
                //};



                return View();
            }

            ///////////////////////////////


        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> Create(ExamViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam
                {
                    Id = Guid.NewGuid(),
                    BatchId = obj.BatchId,
                    StudentId = obj.StudentId,
                    ExamType = obj.ExamType,
                    Status="Request"
                };

                _db.Exam.Add(exam);

                _db.SaveChanges();
                TempData["success"] = "Exam Request submitted successfully";
                return RedirectToAction("index");
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {

                    string userEmail = user.Email;
                    var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                    ViewBag.currentUser = currentUser;
                    var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);
                    ViewBag.IsDetailInformationSaved = userDetails.Any();
                    if (userDetails.Any())
                    {
                        ViewBag.StudentDetails = userDetails.FirstOrDefault();
                    }

                    var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
                    ViewBag.Batch = batch;

                    return View();
                }

            }
        }


    }
}
