using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;
namespace LMS.Utility;

public class EmailSender:IEmailSender
{
   public  async Task SendEmailAsync(string email, string subject, string message)
{
        // return Task.CompletedTask;
        var mail = "info@ll-institute.com";
        var pw = "Vz9q3#g32";

        using (var client = new SmtpClient("smtp-mail.outlook.com", 587))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(mail, pw);

            var sendmessage = new MailMessage(from: mail, to: email, subject, message)
            {
                IsBodyHtml = true
            };

            try
            {
                await client.SendMailAsync(sendmessage);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log it)
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw; // Re-throw the exception to propagate it further if needed
            }
        }
    }


    //public Task SendEmailAsync(string email, string subject, string htmlMessage)
    //{
    //    var mail = " info@ll-institute.com";
    //    var pw = "Vz9q3#g32";
    //    var client = new SmtpClient("smtp-mail.outlook.com", 587)
    //    {
    //        EnableSsl = true,
    //        Credentials = new NetworkCredential(mail, pw)
    //    };

    //    return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, htmlMessage));


}
