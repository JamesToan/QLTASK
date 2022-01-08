using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class BanTinController : Controller
    {

        private ApplicationDbContext _context;

        public BanTinController(ApplicationDbContext context)
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

            var bt = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.NguoiDuyetId != null).ToList();
            ViewBag.NewsBanTin = bt;

            var tintuc = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.NguoiDuyetId != null).OrderByDescending(p =>p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
            ViewBag.TinTuc = tintuc;

            var tintucall = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.NguoiDuyetId != null).ToList();
            ViewBag.TinTucAll = tintucall;

            var chuyenmuc = _context.NewsChuyenMuc.Where(p => p.LoaiChuyenMuc == "news").ToList();
            ViewBag.ChuyenMuc = chuyenmuc;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            if (indexLast > tintucall.Count) indexLast = tintucall.Count;
            ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
            ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;



            return View();
        }

        public IActionResult Detail(int nId , string searchString)
        {
            var bt = _context.NewsBanTin.ToList();
            ViewBag.NewsBanTin = bt;
            for (int i = 0; i < bt.Count; i++)
            {
                if (bt[i].Id == nId && bt[i].ChuyenMucId == 1)
                {
                    ViewBag.Content = bt[i];
                    ViewBag.ChuyenMucID = bt[i].Id;
                }
            }

            ViewBag.Searchstring = searchString;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            var chuyenmuc = _context.NewsChuyenMuc.Where(p => p.LoaiChuyenMuc == "news").ToList();
            ViewBag.ChuyenMuc = chuyenmuc;

            return View();
        }

        //public IActionResult ListTinTuc(int? pCount, int? pPage, int? cmID)
        //{
        //    int vCount = (pCount == null ? 4 : (int)pCount);

        //    int vPage = (pPage == null ? 1 : (int)pPage);
        //    ViewBag.pageCurrent = vPage;
        //    if (vPage < 1) vPage = 1;
        //    int indexFirst = (int)(vCount * (vPage - 1));
        //    int indexLast = (int)(vCount * vPage);
        //    ViewBag.pageCurrent = vPage;
        //    ViewBag.Skip = vCount;
        //    ViewBag.pPage = vPage;
        //    ViewBag.ChuyenMucID = cmID;

        //    var bt = _context.NewsBanTin.ToList();
        //    ViewBag.NewsBanTin = bt;

        //    var tintuc = _context.NewsBanTin.Where(p => p.ChuyenMucId == cmID).Skip((vPage - 1) * vCount).Take(vCount).ToList();
        //    ViewBag.TinTuc = tintuc;

        //    var tintucall = _context.NewsBanTin.Where(p => p.ChuyenMucId == cmID).ToList();
        //    ViewBag.TinTucAll = tintucall;

        //    var chuyenmuc = _context.NewsChuyenMuc.Where(p => p.LoaiChuyenMuc == "news").ToList();
        //    ViewBag.ChuyenMuc = chuyenmuc;

        //    if (indexLast > tintucall.Count) indexLast = tintucall.Count;
        //    ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
        //    ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;



        //    return View();
        //}

        
        public ActionResult SearchTinTuc(int? pCount, int? pPage, string searchString)
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
           

            var bt = _context.NewsBanTin.ToList();
            ViewBag.NewsBanTin = bt;

            ViewBag.SearchSrting = searchString;

            if (searchString == null)
            {
                var tintuc = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinTuc = tintuc;

                var tintucall = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.NguoiDuyetId != null).ToList();
                ViewBag.TinTucAll = tintucall;

                if (indexLast > tintucall.Count) indexLast = tintucall.Count;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
            }
            else
            {
                var tintuc = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.TieuDe.Contains(searchString) || p.ChuyenMucId == 1 && p.NoiDung.Contains(searchString) || p.ChuyenMucId == 1 && p.TomTat.Contains(searchString) && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinTuc = tintuc;

                var tintucall = _context.NewsBanTin.Where(p => p.ChuyenMucId == 1 && p.TieuDe.Contains(searchString) || p.ChuyenMucId == 1 && p.NoiDung.Contains(searchString) || p.ChuyenMucId == 1 && p.TomTat.Contains(searchString) && p.NguoiDuyetId != null).ToList();
                ViewBag.TinTucAll = tintucall;

                if (indexLast > tintucall.Count) indexLast = tintucall.Count;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
            }

           

            var chuyenmuc = _context.NewsChuyenMuc.Where(p => p.LoaiChuyenMuc == "news").ToList();
            ViewBag.ChuyenMuc = chuyenmuc;

            

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }
    }
}
