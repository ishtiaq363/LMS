using LMS.DataAccess.Data;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers;

public class FeePrintController : Controller
{
    private readonly ApplicationDbContext _db;

    public FeePrintController(ApplicationDbContext db)
    {
        _db = db;
    }
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Index()
    {
        var StudentList = _db.Payment.Where(py => py.Status == "Paid").Select(paymentViewModel => new PaymentViewModel
        {
            Id = paymentViewModel.Id,
            StudentName = paymentViewModel.Student.FullName,
            BatchName = paymentViewModel.Batch.Name,
            PaymentAmount = paymentViewModel.PaymentAmount,
            PaymentDate = paymentViewModel.PaymentDate,
            ReceiptUrl = paymentViewModel.ReceiptUrl,
            Status = paymentViewModel.Status,

        }).ToList();

        return View(StudentList);
    }

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [HttpGet]
    public IActionResult GetStudentFee(DateTime paymentDate)
    {
        try
        {
          
          var payment =  _db.Payment.Where(p => p.PaymentDate == paymentDate).Select(paymentViewModel => new PaymentViewModel
            {
                Id = paymentViewModel.Id,
                StudentName = paymentViewModel.Student.FullName,
                BatchName = paymentViewModel.Batch.Name,
                PaymentAmount = paymentViewModel.PaymentAmount,
                PaymentDate = paymentViewModel.PaymentDate,
                ReceiptUrl = paymentViewModel.ReceiptUrl,
                Status = paymentViewModel.Status,

            }).ToList();
            if (payment == null)
            {
                // If payment record not found, return error response
                return NotFound("Payment record not found");
            }
            return Json(payment);
        }
        catch (Exception ex)
        {
            TempData["error"] = "There is something wrong. Failed";
            return Json(null);
            // Return error response if an exception occurs
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


}
