using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class DNghiepController : Controller
    {
        private ApplicationDbContext _context;

        public DNghiepController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index(string tendn)
        {
            var doanhnghiep = _context.DoanhNghiep.Where(p => p.TenDoanhNghiep == tendn).FirstOrDefault();
            ViewBag.DoanhNghiep = doanhnghiep;

            var loaihinh = _context.DMLoaiHinh.Where(p => p.Id == doanhnghiep.LoaiHinhId).FirstOrDefault();
            ViewBag.LoaiHinh = loaihinh;

            var nkt = _context.DMNganhKinhTe.Where(p => p.Id == doanhnghiep.NganhKinhTeId).FirstOrDefault();
            ViewBag.NKT = nkt;

            var tuyendung = _context.HoSoTuyenDung.Where(p => p.DoanhNghiepId == doanhnghiep.Id && p.HanNop >= DateTime.Now && p.XacThuc == true ).Include(e => e.MucLuong).Include(e => e.NoiLamViecHuyen).ToList();
            ViewBag.TuyenDung = tuyendung;

            var datuyendung = _context.HoSoTuyenDung.Where(p => p.DoanhNghiepId == doanhnghiep.Id && p.HanNop < DateTime.Now && p.XacThuc == true).Include(e => e.MucLuong).Include(e => e.NoiLamViecHuyen).ToList();
            ViewBag.DaTuyenDung = datuyendung;

            var cauhinh = _context.CauHinh.Where(p => p.TenCauHinh == "Công khai hồ sơ tuyển dụng").FirstOrDefault();
            ViewBag.CauHinh = cauhinh;
            return View();
        }
    }
}
