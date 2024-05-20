using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Linq;

namespace LMSWeb.Areas.StudentSide.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PasswordViewModel PM { get; set; }
        public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            PM = new PasswordViewModel();
        }

       

            [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> Index(StudentDetailViewModel obj)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            string userEmail = user.Email;
            var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
           
            var userDetails = _db.StudentDetail.FirstOrDefault(di => di.StudentId == currentUser.Id);
            var batch = _db.BatchStudent.Where(x => x.StudentId == currentUser.Id).FirstOrDefault();
            if (batch != null)
            {
                var notification = _db.Notification.Where(n => n.BatchId == batch.BatchId).ToList();
                ViewBag.Notifications = notification;
            }

            if (userDetails == null)
            {
                // If user details are not found, create a new StudentDetail entity
                var studentDetail = new StudentDetail
                {
                    Id = Guid.NewGuid(),
                    StudentId = currentUser.Id,
                    Religon = obj.Religon,
                    PostalCode = obj.PostalCode,
                    Province = obj.Province,
                    Phone = obj.Phone,
                    Country = obj.Country,
                    Nationality = obj.Nationality,
                    GuardianName = obj.GuardianName,
                    GuardianContactNo = obj.EmergencyContactNo,
                    EmergencyContactNo = obj.EmergencyContactNo,
                    PreviousQualification = obj.PreviousQualification,
                    InstituteName = obj.InstituteName,
                    PassingYear = obj.PassingYear,
                    MarksAverage = obj.MarksAverage,
                };

                _db.StudentDetail.Add(studentDetail);
                await _db.SaveChangesAsync();

                TempData["success"] = "Student Details are saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                // If user details are found, update the existing StudentDetail entity
                userDetails.Religon = obj.Religon;
                userDetails.PostalCode = obj.PostalCode;
                userDetails.Province = obj.Province;
                userDetails.Phone = obj.Phone;
                userDetails.Country = obj.Country;
                userDetails.Nationality = obj.Nationality;
                userDetails.GuardianName = obj.GuardianName;
                userDetails.GuardianContactNo = obj.EmergencyContactNo;
                userDetails.EmergencyContactNo = obj.EmergencyContactNo;
                userDetails.PreviousQualification = obj.PreviousQualification;
                userDetails.InstituteName = obj.InstituteName;
                userDetails.PassingYear = obj.PassingYear;
                userDetails.MarksAverage = obj.MarksAverage;

                _db.StudentDetail.Update(userDetails);
                await _db.SaveChangesAsync();

                TempData["success"] = "Student Details are updated successfully";
                return RedirectToAction("Index");
            }
        }

        //public async Task<IActionResult> Index(StudentDetailViewModel obj)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    string userEmail = user.Email;
        //    var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
        //    var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id).FirstOrDefault();
        //    if (userDetails==null)
        //    {


        //            var studentDetail = new StudentDetail
        //            {
        //                Id = Guid.NewGuid(),
        //                StudentId = currentUser.Id,
        //                Religon = obj.Religon,
        //                PostalCode = obj.PostalCode,
        //                Province = obj.Province,
        //                Phone = obj.Phone,
        //                Country = obj.Country,
        //                Nationality = obj.Nationality,
        //                GuardianName = obj.GuardianName,
        //                GuardianContactNo = obj.EmergencyContactNo,
        //                EmergencyContactNo = obj.EmergencyContactNo,
        //                PreviousQualification = obj.PreviousQualification,
        //                InstituteName = obj.InstituteName,
        //                PassingYear = obj.PassingYear,
        //                MarksAverage = obj.MarksAverage,
        //            };

        //            _db.StudentDetail.Add(studentDetail);
        //           await _db.SaveChangesAsync();

        //            TempData["success"] = "Student Details are Saved successfully";
        //            return RedirectToAction("index");


        //    }
        //    else
        //    {
        //        //if user details are already entered than just updation will be perform
        //        if (ModelState.IsValid) 
        //        {
        //           // var cUser = userDetails.FirstOrDefault();
        //            StudentDetail studentDetail = new StudentDetail {
        //                Id = obj.Id,
        //                StudentId = obj.StudentId,
        //                Religon = obj.Religon,
        //                PostalCode = obj.PostalCode,
        //                Province = obj.Province,
        //                Phone = obj.Phone,
        //                Country = obj.Country,
        //                Nationality = obj.Nationality,
        //                GuardianName = obj.GuardianName,
        //                GuardianContactNo = obj.EmergencyContactNo,
        //                EmergencyContactNo = obj.EmergencyContactNo,
        //                PreviousQualification = obj.PreviousQualification,
        //                InstituteName = obj.InstituteName,
        //                PassingYear = obj.PassingYear,
        //                MarksAverage = obj.MarksAverage,
        //            };
        //            _db.StudentDetail.Update(studentDetail);
        //           await  _db.SaveChangesAsync();

        //            TempData["success"] = "Student Details are Updated successfully";
        //            return RedirectToAction("index");

        //        } else
        //        {
        //            return View();
        //        }
        //    }

        //}

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task<IActionResult> GetFormData()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return null;
            }
            else
            {

                string userEmail = user.Email;
                var currentUser = _db.Student.FirstOrDefault(x => x.Email == userEmail);
                ViewBag.currentUser = currentUser;
                var userDetails = _db.StudentDetail.Where(di => di.StudentId == currentUser.Id);


                return Json(userDetails);
            }
        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        public async Task<IActionResult> Index()
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
                var studentStatus = _db.BatchStudent.Where(bs => bs.StudentId == currentUser.Id).FirstOrDefault();
                ViewBag.StudentStatus = studentStatus;
                var userDetails = _db.StudentDetail.Where(di => di.StudentId==currentUser.Id);
                ViewBag.defaultId = Guid.NewGuid();
                ViewBag.IsDetailInformationSaved = userDetails.Any();
                if (userDetails.Any())
                {
                    ViewBag.StudentDetails= userDetails.FirstOrDefault();
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


               

                
                return View();
            }
        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpGet]
        public async Task<IActionResult> GetSubjectLinksInformation(Guid id) 
        {
            ViewBag.Resource = null;
            var subjectDetails = _db.SubjectDetail.Where(sub => sub.SubjectId ==id).ToList();
            if(subjectDetails.Any())
            {
                ViewBag.Resource = subjectDetails;
                return Json(subjectDetails);
            }
            else
            {
                return null;
            }

        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpGet]
        public async Task<IActionResult> GetPaperInformation(Guid id)
        {
            ViewBag.AssessmentResource = null;
            var assesmentDetails = _db.AssessmentSchedule.Where(sub => sub.SubjectId == id && sub.Status == "Release").ToList();
            if (assesmentDetails.Any())
            {
                ViewBag.AssessmentResource = assesmentDetails;
                return Json(assesmentDetails);
            }
            else
            {
                return null;
            }


        }
        
        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpGet]
        public async Task<IActionResult> GetSubjectOutlineInformation(Guid id)
        {
            
            var subjectOutlineDetails = _db.SubjectOutline.Where(sub => sub.SubjectId == id ).FirstOrDefault();
            if (subjectOutlineDetails!=null)
            {
               
                return Json(subjectOutlineDetails);
            }
            else
            {
                return null;
            }


        }

        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(StudentDetailViewModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (obj.MyEmail.Length > 0 && obj.MyPassword.Length > 7 && obj.MyRePassword.Length > 7)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(obj.MyEmail);
                    if (user != null)
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, obj.MyRePassword);
                        //     var result = await _userManager.ChangePasswordAsync(user, obj.MyPassword, obj.MyRePassword);
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            TempData["success"] = "Password updated successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Failed to update password";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error"] = "User not found";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Index");
                }
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



        [Area("StudentSide")]
        [Authorize(Roles = SD.Role_Student)]
        [HttpPost]
        public async Task<IActionResult> SubmitAssessment(IFormFile file, Guid assessmentScheduleId, Guid studentId)
        {
           
                if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            String obj = "";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string StudentPath = Path.Combine(wwwRootPath, @"Submissions\");

                using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj = @"\Submissions\" + fileName;

            }

           
                Submission submission = new Submission
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentId,
                    AssessmentScheduleId = assessmentScheduleId,
                    Status = "Submitted",
                    SubmissionUrl = obj
                };
                _db.Submission.Add(submission);
                _db.SaveChanges();
                TempData["success"] = "Assessment is Uloaded successfully";


                return Ok();
            
           
        }


        /*  public async Task<IActionResult> UpdatePassword(StudentDetailViewModel obj)
          {

              if (obj == null)
              {
                  return NotFound();
              }
              if (obj.MyEmail.Length> 0 && obj.MyPassword.Length > 7  && obj.MyRePassword.Length >7 )
              {
                  try
                  {
                      var user = new IdentityUser { UserName = obj.MyEmail, Email = obj.MyEmail };
                      user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, obj.MyRePassword);
                  var result = await _userManager.UpdateAsync(user);                  


                      if (!result.Succeeded)
                      {
                          TempData["success"] = "Password Updated successfully";
                          return RedirectToAction("index");

                      }
                      else
                      {
                          throw new Exception();
                      }
                  }
                  catch(Exception ex)
                  {
                      TempData["error"] = ex.ToString();
                     // TempData["error"] = JsonConvert.DeserializeObject(ex.ToString());
                      return RedirectToAction("index");
                  }



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

          */


    }
}
