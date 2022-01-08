using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class ModContentController : Controller
    {

        private ApplicationDbContext _context;

        public ModContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult FooterView() =>
           new PartialViewResult
           {
               ViewName = "_Footer",
               ViewData = ViewData,
           };
        public IActionResult Footer()
        {

            var cauhinh = _context.CauHinh.ToList();
            ViewBag.CauHinh = cauhinh;

            var tuyendung = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).ToList();
            ViewBag.TuyenDung = tuyendung;

            var timviec = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
            ViewBag.TimViec = timviec;

            ViewBag.TuyenDungCount = tuyendung.Count;
            ViewBag.TimViecCount = timviec.Count;

            var thanhvien = _context.User.Where(p => p.RoleId == 3 || p.RoleId == 4).ToList();
            ViewBag.ThanhVienCount = thanhvien.Count;

            var cauhinhd = _context.CauHinh.Where(p => p.Id == 20).FirstOrDefault();
            ViewBag.TruyCap = cauhinhd.GiaTri;

            ViewData.Add(new KeyValuePair<string, object>("truycapc", cauhinhd.GiaTri));
            ViewData.Add(new KeyValuePair<string, object>("timvieccc", timviec.Count));
            ViewData.Add(new KeyValuePair<string, object>("tuyendungcc", tuyendung.Count));
            ViewData.Add(new KeyValuePair<string, object>("thanhviencc", thanhvien.Count));


            return PartialView("_Footer.cshtml");

        }
    }
}
