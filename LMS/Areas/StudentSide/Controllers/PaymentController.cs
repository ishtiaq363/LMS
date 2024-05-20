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
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PaymentController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
           
        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task< IActionResult> Index()
        {
            /////////////////////////////
            ///
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
                if (batch != null)
                {
                    var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
                    ViewBag.Notifications = notification;
                }

                if (batch != null)
                {
                    var course = _db.Batch.Where(b => b.Id == batch.BatchId).FirstOrDefault();
                    ViewBag.Title = _db.Course.Where(c => c.Id == course.CourseId).Select(c => c.Title).FirstOrDefault();
                    if (course != null)
                    {
                        var subjects = _db.CourseSubject.Where(c => c.CourseId == course.CourseId).Select(c => c.SubjectId).ToList();
                        if (subjects.Any())
                        {
                            var subjectList = _db.Subject.Where(s => subjects.Contains(s.Id)).ToList();
                            ViewBag.Subject = subjectList;
                        }


                    }

                }


                StudentViewModel studentVM = new StudentViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FullName,
                    FatherName = currentUser.FatherName,
                    DOB = currentUser.DOB,
                    Password = currentUser.Password,
                    RegistrationNo = currentUser.RegistrationNo,
                    MobileNo = currentUser.MobileNo,
                    Gender = currentUser.Gender,
                    Address = currentUser.Address,
                    City = currentUser.City,
                    Email = currentUser.Email,
                    Status = currentUser.Status,
                    ImageURL = currentUser.ImageURL,
                };
                ViewBag.User = studentVM;

               
                    var payments = _db.Payment.Where(p => p.StudentId == currentUser.Id).ToList();
                    ViewBag.Payments = payments;
                   
               

                return View();
            }
            ///////////////////////////////

           
        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task<IActionResult> Create()
        {
            ///////////////////////////////
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");

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
                ViewBag.CurrentUserId = currentUser.Id;
                var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);

                ViewBag.IsDetailInformationSaved = userDetails.Any();
                if (userDetails.Any())
                {
                    ViewBag.StudentDetails = userDetails.FirstOrDefault();
                }


                var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
                var FeeInfo = _db.BatchFee.Where(x => x.BatchId == batch.BatchId).FirstOrDefault();
                ViewBag.BatchInfo = batch;
                ViewBag.FeeInfo = FeeInfo;
                if (batch != null)
                {
                    var course = _db.Batch.Where(b => b.Id == batch.BatchId).FirstOrDefault();
                    ViewBag.Title = _db.Course.Where(c => c.Id == course.CourseId).Select(c => c.Title).FirstOrDefault();
                    if (course != null)
                    {
                        var subjects = _db.CourseSubject.Where(c => c.CourseId == course.CourseId).Select(c => c.SubjectId).ToList();
                        if (subjects.Any())
                        {
                            var subjectList = _db.Subject.Where(s => subjects.Contains(s.Id)).ToList();
                            ViewBag.Subject = subjectList;
                        }


                    }

                }
                //var StudentList = _db.Student.Select(studentVM => new StudentViewModel
                //{
                //    Id = studentVM.Id,
                //    FullName = studentVM.FullName,
                //    FatherName = studentVM.FatherName,
                //    DOB = studentVM.DOB,
                //    Password = studentVM.Password,
                //    RegistrationNo = studentVM.RegistrationNo,
                //    MobileNo = studentVM.MobileNo,
                //    Gender = studentVM.Gender,
                //    Address = studentVM.Address,
                //    City = studentVM.City,
                //    Email = studentVM.Email,
                //    Status = studentVM.Status,
                //    ImageURL = studentVM.ImageURL,
                //}).ToList();
                ////return View(StudentList);
                ///
                StudentViewModel studentVM = new StudentViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FullName,
                    FatherName = currentUser.FatherName,
                    DOB = currentUser.DOB,
                    Password = currentUser.Password,
                    RegistrationNo = currentUser.RegistrationNo,
                    MobileNo = currentUser.MobileNo,
                    Gender = currentUser.Gender,
                    Address = currentUser.Address,
                    City = currentUser.City,
                    Email = currentUser.Email,
                    Status = currentUser.Status,
                    ImageURL = currentUser.ImageURL,
                };
                ViewBag.User = studentVM;

               


               

                return View();
            }

        }
        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel obj,  IFormFile? file)
        {
            if (ModelState.IsValid && file != null)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string StudentPath = Path.Combine(wwwRootPath, @"resources\receipts\");

                    using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ReceiptUrl = @"\resources\receipts\" + fileName;

                }
                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    BatchId = obj.BatchId,
                    Status = obj.Status,
                    StudentId = obj.StudentId,
                    ReceiptUrl = obj.ReceiptUrl,
                    PaymentDate = obj.PaymentDate,
                    PaymentAmount = obj.PaymentAmount,
                };
                _db.Payment.Add(payment);
                _db.SaveChanges();
                TempData["success"] = "Fee Voucher submitted successfully for Review ";
                return RedirectToAction("index");
            }
            else
            {
                /////////////////////////////
                ///
               
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {

                    string userEmail = user.Email;
                    var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                    var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);
                    ViewBag.IsDetailInformationSaved = userDetails.Any();
                    if (userDetails.Any())
                    {
                        ViewBag.StudentDetails = userDetails.FirstOrDefault();
                    }


                    var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();

                    if (batch != null)
                    {
                        var course = _db.Batch.Where(b => b.Id == batch.BatchId).FirstOrDefault();
                        ViewBag.Title = _db.Course.Where(c => c.Id == course.CourseId).Select(c => c.Title).FirstOrDefault();
                        if (course != null)
                        {
                            var subjects = _db.CourseSubject.Where(c => c.CourseId == course.CourseId).Select(c => c.SubjectId).ToList();
                            if (subjects.Any())
                            {
                                var subjectList = _db.Subject.Where(s => subjects.Contains(s.Id)).ToList();
                                ViewBag.Subject = subjectList;
                            }


                        }

                    }


                    StudentViewModel studentVM = new StudentViewModel
                    {
                        Id = currentUser.Id,
                        FullName = currentUser.FullName,
                        FatherName = currentUser.FatherName,
                        DOB = currentUser.DOB,
                        Password = currentUser.Password,
                        RegistrationNo = currentUser.RegistrationNo,
                        MobileNo = currentUser.MobileNo,
                        Gender = currentUser.Gender,
                        Address = currentUser.Address,
                        City = currentUser.City,
                        Email = currentUser.Email,
                        Status = currentUser.Status,
                        ImageURL = currentUser.ImageURL,
                    };
                    ViewBag.User = studentVM;


                    return View();
                }
                ///////////////////////////////
                return View();
            }

        }


       

    }
}
