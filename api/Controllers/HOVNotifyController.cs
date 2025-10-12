using Microsoft.AspNetCore.Mvc;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;
using System.Net;
using System.Net.Mail;

namespace HOVLaneViolation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotifyCustomerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public NotifyCustomerController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public class NotifyRequest
        {
            public string LicensePlate { get; set; } = string.Empty;
            public string Method { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Notify([FromBody] NotifyRequest request)
        {
            var customer = _context.HOVCustomers.FirstOrDefault(c => c.LicensePlate == request.LicensePlate);

            if (customer == null)
                return NotFound("Customer not found.");

            try
            {
                switch (request.Method.ToLower())
                {
                    case "email":
                        await SendEmailNotification(customer.Email);
                        break;
                        
                    case "sms":
                        // Keep SMS as simulation
                        Console.WriteLine($"📱 SMS would be sent to {customer.Phone}: You committed a violation.");
                        break;
                        
                    default:
                        return BadRequest("Invalid notification method.");
                }

                return Ok("Notification sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send notification: {ex.Message}");
            }
        }

        private async Task SendEmailNotification(string recipientEmail)
        {
            var smtpSettings = _config.GetSection("SmtpSettings");
            
            using (var client = new SmtpClient(smtpSettings["Host"]))
            {
                client.Port = int.Parse(smtpSettings["Port"]);
                client.Credentials = new NetworkCredential(
                    smtpSettings["Username"],
                    smtpSettings["Password"]);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["FromEmail"]),
                    Subject = "HOV Lane Violation Notice",
                    Body = @"Dear Customer,

You have been recorded committing an HOV lane violation. 

Please review the violation details in your account or contact support if you believe this is an error.

Regards,
HOV Lane Compliance Team",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(recipientEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}