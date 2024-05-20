using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace LMSWeb.Areas.Admin.Controllers;

public class StudentController : Controller
{
  //  private readonly EmailSender _emailSender;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public StudentController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager,
       SignInManager<IdentityUser> signInManager )
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
        _signInManager = signInManager;
     //  _emailSender = emailSender;
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Index()
    {
        var StudentList = _db.Student.Select(studentVM => new StudentViewModel
        {
            Id = studentVM.Id,
            FullName = studentVM.FullName,
            FatherName = studentVM.FatherName,
            DOB = studentVM.DOB,
            Password = studentVM.Password,
            RegistrationNo = studentVM.RegistrationNo,
            MobileNo = studentVM.MobileNo,
            Gender = studentVM.Gender,
            Address = studentVM.Address,
            City = studentVM.City,
            Email = studentVM.Email,
            Status = studentVM.Status,
            ImageURL = studentVM.ImageURL,
        }).ToList();
        return View(StudentList);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Create()
    {


        ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
        ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Register" , Text = "Register"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                new SelectListItem{ Value="Passout" , Text = "Passout"},
                 };

        return View();
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(StudentViewModel studentVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
                var user = new IdentityUser { UserName = studentVM.Email, Email = studentVM.Email };
            var result = await _userManager.CreateAsync(user, studentVM.Password);
            if (result.Succeeded)
            {
                //save role for student
                var currentUser = await _userManager.FindByNameAsync(studentVM.Email);
                var roleresult = await _userManager.AddToRoleAsync(user, SD.Role_Student);

                if (roleresult.Succeeded)
                {
                    string email = studentVM.Email;
                    string password = studentVM.Password;
                    string welcomeMessage = $"Welcome! To LLinstitute Your email is {email} and your password is {password}.";
                    
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string StudentPath = Path.Combine(wwwRootPath, @"images\students\");

                        using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        studentVM.ImageURL = @"\images\students\" + fileName;

                    }
                    Student student = new Student
                    {

                        Id = Guid.NewGuid(),
                        FullName = studentVM.FullName,
                        FatherName = studentVM.FatherName,
                        DOB = studentVM.DOB,
                        Password = studentVM.Password,
                        RegistrationNo = studentVM.RegistrationNo,
                        MobileNo = studentVM.MobileNo,
                        Gender = studentVM.Gender,
                        Address = studentVM.Address,
                        City = studentVM.City,
                        Email = studentVM.Email,
                        Status = studentVM.Status,
                        ImageURL = studentVM.ImageURL,
                    };

                    _db.Student.Add(student);
                    _db.SaveChanges();
                    TempData["success"] = "Student Registed successfully";
                  //  await _emailSender.SendEmailAsync(studentVM.Email, "Your Credentials from L.L Institute", welcomeMessage);
                    return RedirectToAction("index");
                }
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Register" , Text = "Register"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                new SelectListItem{ Value="Passout" , Text = "Passout"},
                 };

            return View();
        }
        else
        {
            ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Register" , Text = "Register"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                new SelectListItem{ Value="Passout" , Text = "Passout"},
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

        Student student = _db.Student.Find(Id);
        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            FatherName = student.FatherName,
            DOB = student.DOB,
            Password = student.Password,
            RegistrationNo = student.RegistrationNo,
            MobileNo = student.MobileNo,
            Gender = student.Gender,
            Address = student.Address,
            City = student.City,
            Email = student.Email,
            Status = student.Status,
            ImageURL = student.ImageURL,
        };
        ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
        ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Register" , Text = "Register"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                new SelectListItem{ Value="Passout" , Text = "Passout"},
                 };
        return View(model);
    }


    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public IActionResult Update(StudentViewModel obj, IFormFile? file)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
       
        if (obj == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            if(file !=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string StudentPath = Path.Combine(wwwRootPath, @"images\students\");

                if (!string.IsNullOrEmpty(obj.ImageURL))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(StudentPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                obj.ImageURL = @"\images\students\" + fileName;

            }
            

            Student student = new Student
            {
                Id = obj.Id,
                FullName = obj.FullName,
                FatherName = obj.FatherName,
                DOB = obj.DOB,
                Password = obj.Password,
                RegistrationNo = obj.RegistrationNo,
                MobileNo = obj.MobileNo,
                Gender = obj.Gender,
                Address = obj.Address,
                City = obj.City,
                Email = obj.Email,
                Status = obj.Status,
                ImageURL = obj.ImageURL,
            };
            _db.Student.Update(student);
            _db.SaveChanges();
            TempData["success"] = "Student Updated successfully";
            return RedirectToAction("index");
        }
        else
        {
            ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
            ViewBag.AvailableOptions = new List<SelectListItem>
                {
                new SelectListItem{ Value="Register" , Text = "Register"},
                new SelectListItem{ Value="Block" , Text = "Block"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
                new SelectListItem{ Value="Passout" , Text = "Passout"},
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

        Student? student = _db.Student.Find(Id);
        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            FatherName = student.FatherName,
            DOB = student.DOB,
            Password = student.Password,
            RegistrationNo = student.RegistrationNo,
            MobileNo = student.MobileNo,
            Gender = student.Gender,
            Address = student.Address,
            City = student.City,
            Email = student.Email,
            ImageURL = student.ImageURL,
        };

        ViewBag.GenderOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Male" , Text = "Male"},
                new SelectListItem{ Value="Female" , Text = "Female"},

            };
        return View(model);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(Guid Id)
    {
        Student? student = _db.Student.Find(Id);
        if (student == null)
        {
            return NotFound();
        }
        _db.Student.Remove(student);
        _db.SaveChanges();
        TempData["success"] = "Student Record Deleted successfully";
        return RedirectToAction("index");
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult AssignStudent()
    {
        // var batches = _db.Batch.Where(c => !_db.BatchFee.Any(cf => cf.BatchId == c.Id)).ToList();
        var batches = _db.Batch.ToList();
        ViewBag.BatchList = new SelectList(batches, "Id", "Name");
        var students = _db.Student.ToList();
        var studentList = new List<SelectListItem>();
        foreach (var student in students)
        {            
            var displayText = $"{student.FullName} - {student.RegistrationNo}";
            studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
        }

        ViewBag.StudentList = new SelectList(studentList, "Id","FullName");
        return View();
    }


    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Profile() { return View(); }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult ForgotPassword(Guid Id)
    {
        if (Id == null)
        {
            return NotFound();
        }

        Student student = _db.Student.Find(Id);
        if (student == null)
        {
            return NotFound();
        }

        var model = new StudentViewModel
        {
            Id = student.Id,
            FullName = student.FullName,
            FatherName = student.FatherName,
            DOB = student.DOB,
            Password = student.Password,
            RegistrationNo = student.RegistrationNo,
            MobileNo = student.MobileNo,
            Gender = student.Gender,
            Address = student.Address,
            City = student.City,
            Email = student.Email,
            Status = student.Status,
            ImageURL = student.ImageURL,
        };
        return View(model);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(StudentViewModel obj)
    {
        if (obj.Email == null || obj.RePassword== null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByEmailAsync(obj.Email);
        if (user != null)
        {         
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, obj.RePassword);
            if (result.Succeeded)
            {
                TempData["success"] = "Password updated successfully";
                return RedirectToAction("Index", "Student");
            }
            else
            {
                TempData["error"] = "Failed to update password";
                return RedirectToAction("Index", "Student");
            }

        }

        return View();
    }
}
