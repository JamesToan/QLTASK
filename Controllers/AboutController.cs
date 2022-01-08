using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class AboutController : Controller
    {

        private ApplicationDbContext _context;
        // GET: /<controller>/
        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var about = _context.NewsBanTin.Where(p => p.ChuyenMucId == 35 && p.TieuDe == "About Index").FirstOrDefault();
            ViewBag.About = about;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;
            return View();
        }

        public IActionResult OrgaStructure()
        {
            var about = _context.NewsBanTin.Where(p => p.ChuyenMucId == 35 && p.TieuDe == "About Orga").FirstOrDefault();
            ViewBag.About = about;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }

        public IActionResult Address()
        {
            var about = _context.NewsBanTin.Where(p => p.ChuyenMucId == 35 && p.TieuDe == "About Address").FirstOrDefault();
            ViewBag.About = about;

            return View();
        }
    }
}
