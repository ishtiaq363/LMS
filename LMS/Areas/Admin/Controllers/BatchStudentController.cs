using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class BatchStudentController : Controller
    {
        public readonly ApplicationDbContext _db;
        public BatchStudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public IActionResult GetStudents(Guid Id)
        {
            var assignedStudentsIds = _db.BatchStudent.Select(bs => bs.StudentId).ToList();
            if (assignedStudentsIds.Any())
            {
                var studentsWhoAreNotAssigned = _db.Student.Where(s => !assignedStudentsIds.Contains(s.Id));
                return Json(new SelectList(studentsWhoAreNotAssigned, "Id", "FullName"));
            }
            else
            {
                var student = _db.Student.ToList();
                return Json(new SelectList(student, "Id", "FullName"));
            }

        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var BatchStudentList = _db.BatchStudent.Select(p => new BatchStudentViewModel
            {

                Id = p.Id,
                BatchId = p.BatchId,
                BatchName = p.Batch.Name,
                StudentId = p.StudentId,
                StudentName = p.Student.FullName,
                EnrollmentDate = p.EnrollmentDate,
                EnrollmentNo = p.EnrollmentNo,
                Remarks = p.Remarks,
                Status = p.Status
               
            }).ToList();

            return View(BatchStudentList);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");
            //var students = _db.Student.ToList();
            //var studentList = new List<SelectListItem>();
            //foreach (var student in students)
            //{
            //    var displayText = $"{student.FullName} ( {student.RegistrationNo} )";
            //    studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
            //}

            //ViewBag.StudentList = new SelectList(studentList, "Value", "Text");
           
            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Enrolled" , Text = "Enrolled"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(BatchStudentViewModel obj)
        {
            if (ModelState.IsValid)
            {
                BatchStudent batchStudent = new BatchStudent
                {
                    Id = Guid.NewGuid(),
                    BatchId = obj.BatchId,
                    StudentId = obj.StudentId,
                    EnrollmentNo = obj.EnrollmentNo,
                    EnrollmentDate = obj.EnrollmentDate,
                    Remarks = obj.Remarks,
                    Status = obj.Status
                };

                _db.BatchStudent.Add(batchStudent);

                _db.SaveChanges();
                TempData["success"] = "Student is Successfully added to Batch";
                return RedirectToAction("index");
            }
            else
            {
                var batches = _db.Batch.ToList();
                ViewBag.BatchList = new SelectList(batches, "Id", "Name");
                var students = _db.Student.ToList();
                var studentList = new List<SelectListItem>();
                foreach (var student in students)
                {
                    var displayText = $"{student.FullName} ( {student.RegistrationNo} )";
                    studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
                }

                ViewBag.StudentList = new SelectList(studentList, "Value", "Text");

                ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Enrolled" , Text = "Enrolled"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
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

            BatchStudent batchStudent = _db.BatchStudent.Find(Id);
            if (batchStudent == null)
            {
                return NotFound();
            }

            var model = new BatchStudentViewModel
            {
                Id = batchStudent.Id,
                BatchId = batchStudent.BatchId,
               
                StudentId = batchStudent.StudentId,
               
                EnrollmentDate = batchStudent.EnrollmentDate,
                EnrollmentNo = batchStudent.EnrollmentNo,
                Remarks = batchStudent.Remarks,
                Status = batchStudent.Status
            };
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");
            var students = _db.Student.ToList();
            var studentList = new List<SelectListItem>();
            foreach (var student in students)
            {
                var displayText = $"{student.FullName} ( {student.RegistrationNo} )";
                studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
            }

            ViewBag.StudentList = new SelectList(studentList, "Value", "Text");

            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Enrolled" , Text = "Enrolled"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            return View(model);
        }



        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(BatchStudentViewModel obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                BatchStudent batchStudent = new BatchStudent
                {
                    Id = obj.Id,
                    BatchId = obj.BatchId,
                    StudentId = obj.StudentId,
                    EnrollmentDate = obj.EnrollmentDate,
                    EnrollmentNo = obj.EnrollmentNo,
                    Remarks = obj.Remarks,
                    Status = obj.Status
                };
                _db.BatchStudent.Update(batchStudent);
                _db.SaveChanges();
                TempData["success"] = " Batch Student Record Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                var batches = _db.Batch.ToList();
                ViewBag.BatchList = new SelectList(batches, "Id", "Name");
                var students = _db.Student.ToList();
                var studentList = new List<SelectListItem>();
                foreach (var student in students)
                {
                    var displayText = $"{student.FullName} ( {student.RegistrationNo} )";
                    studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
                }

                ViewBag.StudentList = new SelectList(studentList, "Value", "Text");

                ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Enrolled" , Text = "Enrolled"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
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
        public IActionResult Delete(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            BatchStudent batchStudent = _db.BatchStudent.Find(Id);
            if (batchStudent == null)
            {
                return NotFound();
            }

            var model = new BatchStudentViewModel
            {
                Id = batchStudent.Id,
                BatchId = batchStudent.BatchId,

                StudentId = batchStudent.StudentId,

                EnrollmentDate = batchStudent.EnrollmentDate,
                EnrollmentNo = batchStudent.EnrollmentNo,
                Remarks = batchStudent.Remarks,
                Status = batchStudent.Status
            };
            var batches = _db.Batch.ToList();
            ViewBag.BatchList = new SelectList(batches, "Id", "Name");
            var students = _db.Student.ToList();
            var studentList = new List<SelectListItem>();
            foreach (var student in students)
            {
                var displayText = $"{student.FullName} ( {student.RegistrationNo} )";
                studentList.Add(new SelectListItem { Value = student.Id.ToString(), Text = displayText });
            }

            ViewBag.StudentList = new SelectList(studentList, "Value", "Text");

            ViewBag.AvailableOptions = new List<SelectListItem> {
                new SelectListItem{ Value="Enrolled" , Text = "Enrolled"},
                new SelectListItem{ Value="Suspend" , Text = "Suspend"},
            };
            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            BatchStudent? batchStudent = _db.BatchStudent.Find(Id);
            if (batchStudent == null)
            {
                return NotFound();
            }
            _db.BatchStudent.Remove(batchStudent);
            _db.SaveChanges();
            TempData["success"] = "Batch Student deleted successfully";
            return RedirectToAction("index");
        }
    }
}
