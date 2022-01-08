using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using coreWeb.Models;

using MailKit.Net.Smtp;

using MimeKit;

using MimeKit.Text;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IApplicationLifetime _applicationLifetime;

        public ToolController(IApplicationLifetime applicationLifetime, ApplicationDbContext context)
        {
            _applicationLifetime = applicationLifetime;
            _context = context;
        }

        [HttpPost]
        public IActionResult StopApp()
        {
            _applicationLifetime.StopApplication();
            return Ok();
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("vieclam@vieclamlongan.vn"));
            email.To.Add(MailboxAddress.Parse("vqhuy1986@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

            // send email
            var smtp = new SmtpClient();
            smtp.Connect("mail.vieclamlongan.vn", 25);
            smtp.Authenticate("vieclam@vieclamlongan.vn", "Admin871871");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
        }
    }
}
