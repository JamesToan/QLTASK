using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class BHTNController : Controller
    {

        private ApplicationDbContext _context;

        public BHTNController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? pCount, int? pPage)
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;

            ViewBag.pPage = vPage;

            var tinbhtn = _context.NewsBanTin.Where(p => p.ChuyenMucId == 10 && p.NguoiDuyetId != null).ToList();
            ViewBag.TinBHTNAll = tinbhtn;

            var tinbhtnskip = _context.NewsBanTin.Where(p => p.ChuyenMucId == 10 && p.NguoiDuyetId != null).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList(); 
            ViewBag.TinBHTNSkip = tinbhtnskip;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 33 && p.NguoiDuyetId != null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 23 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            ViewBag.PageCount = (int)(tinbhtn.Count / (vCount + 1)) + 1;
            return View();
        }



        public ActionResult TracuuBHTN(string SoBNhan, string CMND, string SoBHXH)
        {
            var objbhtn = new BHTNService(_context);
            var bhtn = objbhtn.TraCuu(SoBNhan, CMND, SoBHXH);

            ViewBag.BHTN = bhtn;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 33 && p.NguoiDuyetId != null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 23 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }

        public IActionResult BHTNDetail(int? newsId , string searchString)
        {
            ViewBag.Searchstring = searchString;

            var tinbhtnall = _context.NewsBanTin.Where(p => p.ChuyenMucId == 10).ToList();
            ViewBag.TinBHTNAll = tinbhtnall;

            var tinbhtn = _context.NewsBanTin.Where(p => p.ChuyenMucId == 10 && p.Id == newsId).FirstOrDefault();
            ViewBag.TinBHTN = tinbhtn;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 33 && p.NguoiDuyetId != null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 23 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }

    }
}
