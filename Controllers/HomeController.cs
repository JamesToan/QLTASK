using coreWeb.Helps;
using coreWeb.Models;
using coreWeb.Models.Entities;
using coreWeb.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace coreWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private IConfiguration _config;
        int userID;
        int roleID;
        string hoten = "";
       
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {
            var bt = _context.NewsBanTin.ToList();
            ViewBag.NewsBanTin = bt;

            //var banner = _context.NewsBanner.ToList();
            //ViewBag.NewBanner = banner;

            var hosotuyendung = _context.HoSoTuyenDung.ToList();
            ViewBag.HoSoTuyenDung = hosotuyendung;

            var tuyendung = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).ToList();
            ViewBag.TuyenDung = tuyendung;

            var timviec = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
            ViewBag.TimViec = timviec;

            ViewBag.TuyenDungCount = tuyendung.Count;
            ViewBag.TimViecCount = timviec.Count;

            var thanhvien = _context.User.Where(p => p.RoleId == 3 || p.RoleId == 4).ToList();
            ViewBag.ThanhVienCount = thanhvien.Count;

            var tuyendungmoi = _context.HoSoTuyenDung.ToList();
            ViewBag.TuyenDung = tuyendungmoi.OrderByDescending(p => p.Id);

            var dmtinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmtinh;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var dmnganhnghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmnganhnghe;

            var vitricongviec = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCongViec = vitricongviec;

            var banner = _context.NewsBanner.Where(p => p.ChuyenMucId == 20).ToList();
            ViewBag.NewBanner = banner;

            var tuyendungc = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).ToList();
            ViewBag.TuyenDung = tuyendungc;

            var timviecc = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
            ViewBag.TimViecc = timviecc;

            var footer = _context.NewsBanTin.Where(p => p.ChuyenMucId == 36 && p.TieuDe == "Footer Text").FirstOrDefault();
            ViewBag.FooterText = footer;
            var footer1 = _context.NewsBanTin.Where(p => p.ChuyenMucId == 36 && p.TieuDe == "Footer Link").FirstOrDefault();
            ViewBag.FooterLink = footer1;
            ViewBag.TuyenDungCount = tuyendungc.Count;
            ViewBag.TimViecCount = timviecc.Count;
            var timvieccount = timviecc.Count;
            var tuyendungcount = tuyendungc.Count;
            var thanhvienc = _context.User.Where(p => p.RoleId == 3 || p.RoleId == 4).ToList();
            ViewBag.ThanhVienCountc = thanhvienc.Count;
            var thanhviencount = thanhvienc.Count;

            var truycap = _context.CauHinh.Where(p => p.Id == 20).FirstOrDefault();
            var giatri = truycap.GiaTri;

            

            int num = Convert.ToInt32(giatri);
            int count = num + 1;
            truycap.GiaTri = count.ToString();

            _context.Update(truycap);
            _context.SaveChanges();


            var cauhinh = _context.CauHinh.Where(p => p.Id == 20).FirstOrDefault();
            ViewBag.TruyCap = cauhinh.GiaTri;

           
            HttpContext.Session.SetInt32("Timvieccount", timvieccount);
            HttpContext.Session.SetInt32("Tuyendungcount", tuyendungcount);
            HttpContext.Session.SetInt32("Thanhviencount", thanhviencount);
            HttpContext.Session.SetString("Truycapcount", cauhinh.GiaTri);
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            var dmTinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmTinh;

            var dmHuyen = _context.DMHuyen.ToList();
            ViewBag.DMHuyen = dmHuyen;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            var role = _context.Role.ToList();
            ViewBag.Role = role;

            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Registers(string Email, string Password, int Role)
        //{



        //    var objUser = new UserService(_context);

        //    var result = objUser.Add(Email, Password, Role, null);
        //    if (result == 1)
        //    {
        //        var login = new LoginModel();
        //        login.Email = Email;
        //        login.Password = Password;
        //        var user = Authenticate(login);

        //        if (user != null)
        //        {

        //            return RedirectToAction("Index");
        //        }
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }

        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Register(string Username, string Password, string FullName,  int Role)
        {
            //IActionResult response = Unauthorized();
            
            var objUser = new UserService(_context);
            
            
                var result = objUser.Add(Username, Password, FullName, Role, null);
                if (result == 1)
                {

                var configEmail = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "EmailAdress");
                var configPass = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "EmailPass");
                if (configEmail != null && configPass != null)
                {
                    // create email message
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(configEmail.GiaTri));
                    email.To.Add(MailboxAddress.Parse(Username));
                    email.Subject = "Đăng ký thành công tại vieclamlongan.vn";
                    email.Body = new TextPart(TextFormat.Html) { Text = "Trung tâm dịch vụ việc làm Long An thông báo<br />Bạn đã đăng ký tài khoảng thành công <br /> Tên đăng nhập: " + Username + "<br />Trân trọng." };

                    // send email
                    var smtp = new SmtpClient();
                    smtp.Connect("mail.vieclamlongan.vn", 25, MailKit.Security.SecureSocketOptions.None);
                    smtp.Authenticate(configEmail.GiaTri, configPass.GiaTri);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Register");
                }
          
            
        }


        private string BuildToken(User user)
        {
            string PlainData = string.Format("{0}^{1}", user.Id, user.RoleId);
            string EncodeData = Encryption.Encrypt(PlainData);
            var claims = new[] {
                new Claim("Data", EncodeData),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        [HttpGet]
        public ActionResult Login()
        {
            var dmTinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmTinh;

            var dmHuyen = _context.DMHuyen.ToList();
            ViewBag.DMHuyen = dmHuyen;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
               
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    string input = model.Password;
                     var hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
                    var sb = new StringBuilder(hash.Length * 2);

                    foreach (byte b in hash)
                    {
                        sb.Append(b.ToString("x2")); // can be "x2" if you want lowercase
                    }
                     string pass = sb.ToString();
                    var data = _context.User.Where(s => s.UserName.Equals(model.Email) && s.Password.Equals(pass)).FirstOrDefault();

                    if (data != null)
                    {

                       
                        HttpContext.Session.SetString("UserName",model.Email);
                        
                        
                        ViewBag.User = data;
                       
                            userID = data.Id;
                            roleID = data.RoleId;
                            hoten = data.FullName;

                        HttpContext.Session.SetInt32("UserId", userID);
                        HttpContext.Session.SetInt32("RoleId", roleID);
                        HttpContext.Session.SetString("FullName", hoten);
                        if (roleID == 3)
                        {
                            var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
                            if (ngld != null)
                            {
                                HttpContext.Session.SetInt32("NLDID", ngld.Id);
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("NLDID", 0);
                            }
                            
                            //return RedirectToAction("Employees", "UserProfile", new { userId = userID, roleId = roleID });
                            return RedirectToAction("Index");
                        }
                        else if (roleID == 4)
                        {
                            var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
                            if (doanhnghiep != null)
                            {
                                HttpContext.Session.SetInt32("DNID", doanhnghiep.Id);
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("DNID", 0);
                            }                            //return RedirectToAction("Employers", "UserProfile", new { userId = userID, roleId = roleID });
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return RedirectToAction("Login");
                    }
                }
                
                
            }
            return View();
        }

        private User Authenticate(LoginModel login)
        {
            User _user = null;
            var objUser = new UserService(_context);
            _user = objUser.Login(login.Email, login.Password);

            if (_user.Id != 0)
            {
                return _user;
            }
            return null;
        }

        
        public ActionResult Logout()
        {

            HttpContext.Session.SetString("UserName" , "");
            HttpContext.Session.SetString("FullName", "");
            HttpContext.Session.SetInt32("RoleId", 0);
            
            HttpContext.Session.SetInt32("DNID", 0);
            HttpContext.Session.SetInt32("NLDID", 0);
            HttpContext.Session.SetInt32("UserId", 0);
            return RedirectToAction("Index");
        }


        public JsonResult Checkemail (string email)
        {
           
            var user = _context.User.ToList();
            string pass = "";
            bool check;
            int num = 0;
            foreach (var item in user)
            {
                if (item.UserName == email)
                {
                    num = 1;
                }
               
            }

            if (num == 1)
            {
                check = true;
                return Json(check, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                check = false;
                return Json(check, new Newtonsoft.Json.JsonSerializerSettings());
            }

        }
    }
}
