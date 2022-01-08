using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using coreWeb.Models;
using coreWeb.Models.Entities;

using MailKit.Net.Smtp;

using MimeKit;

using MimeKit.Text;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsHoiDapController : ControllerBase
    {
        private ApplicationDbContext _context;


        public NewsHoiDapController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string uid)
        {
            //var user = new UserClaim(HttpContext);
            var result = _context.NewsHoiDap.Where(e => e.UID.ToString() == uid)
            // .Where(e => e.UserId == user.UserId)
            .Include(e => e.LinhVuc)
            .Include(e => e.NguoiHoi)
            .FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.NewsHoiDap
                // .Where(e => e.UserId == user.UserId)
                .Include(e => e.LinhVuc)
                .Include(e => e.NguoiHoi)
                .OrderByDescending(e => e.Id)
                .ToList();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] NewsHoiDap model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
                    model.UID = Guid.NewGuid();
                    model.NguoiHoiId = user.UserId;
                    model.NgayHoi = DateTime.Now;
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(model.Id);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] NewsHoiDap model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsHoiDap.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.LinhVucId = model.LinhVucId;
                        result.TieuDe = model.TieuDe;
                        result.NoiDung = model.NoiDung;
                        result.TraLoi = model.TraLoi;
                        result.VanBan = model.VanBan;
                        result.NguoiTraLoiId = user.UserId;
                        result.NgayTraLoi = DateTime.Now;

                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Active([FromBody] NewsHoiDap model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsHoiDap.Include(e => e.NguoiHoi)
                    .SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        if (result.NguoiDuyetId == null)
                        {
                            result.NguoiDuyetId = user.UserId;
                            result.NgayDuyet = DateTime.Now;
                            //send email
                            var config = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "GuiEmailTraLoiCauHoi");
                            var configEmail = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "EmailAdress");
                            var configPass = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "EmailPass");
                            if (config != null && config.GiaTri == "1" && configEmail != null && configPass != null)
                            {
                                // create email message
                                var email = new MimeMessage();
                                email.From.Add(MailboxAddress.Parse(configEmail.GiaTri));
                                email.To.Add(MailboxAddress.Parse(result.Email));
                                email.Subject = "vieclamlongan.vn trả lời câu hỏi của bạn";
                                email.Body = new TextPart(TextFormat.Html) { Text = "vieclamlongan.vn xin trả lời câu hỏi của bạn như sau:<br />" + result.TraLoi + "<br />Trân trọng." };

                                // send email
                                var smtp = new SmtpClient();
                                smtp.Connect("mail.vieclamlongan.vn", 25);
                                smtp.Authenticate(configEmail.GiaTri, configPass.GiaTri);
                                smtp.Send(email);
                                smtp.Disconnect(true);
                            }
                        }
                        else
                        {
                            result.NguoiDuyetId = null;
                            result.NgayDuyet = null;
                        }
                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _context.NewsHoiDap.SingleOrDefault(e => e.Id == id);
                if (result != null)
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                    return Ok(1);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }


}
