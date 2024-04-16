using ContactUsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using ContactUsAPI.Data;
using System.Net;


namespace ContactUsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ProccessContactPage([FromBody] ContactInfo info)
        {
            

            MailMessage mail = new MailMessage(WebsiteEmail.email, WebsiteEmail.email);
            mail.Subject = "New Inquiry - " + info.Subject + " From " + info.Email;
            mail.Body = "You have a new inquiry from " + info.Name + " Message: " + info.Message;

           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

           System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient smtpClient = new SmtpClient("smtp.onmail.com");
            smtpClient.Port = 465;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(WebsiteEmail.email, WebsiteEmail.password);
            smtpClient.EnableSsl = true;
            
            

            try
            {
                // Send email
                smtpClient.Send(mail);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

    }
}
