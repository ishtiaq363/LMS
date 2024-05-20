using LMS.DataAccess.Data;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSWeb.Areas.Admin.Controllers;

public class PaymentController : Controller
{
    private readonly ApplicationDbContext _db;

    public PaymentController(ApplicationDbContext db)
    {
        _db = db;
    }
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public IActionResult Index()
{
    var StudentList = _db.Payment.Select(paymentViewModel => new PaymentViewModel
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
    [HttpPost]
    public IActionResult UpdateStatus(Guid paymentId, string status)
    {
        try
        {
            // Retrieve the payment record from the database
            var payment = _db.Payment.FirstOrDefault(p=> p.Id==paymentId);

            if (payment == null)
            {
                // If payment record not found, return error response
                return NotFound("Payment record not found");
            }

            // Update the status of the payment
            payment.Status = status;
            _db.Payment.Update(payment);
            // Save changes to the database
            _db.SaveChanges();

            TempData["success"] = "Payment Status updated successfully";
            return RedirectToAction("Payment");
            // Return success response
            return Ok("Fee status updated successfully");
        }
        catch (Exception ex)
        {
            TempData["error"] = "There is something wrong. Failed";
            return RedirectToAction("Payment");
            // Return error response if an exception occurs
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }



}
