using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class VanBanController : Controller
    {

        private ApplicationDbContext _context;

        public VanBanController(ApplicationDbContext context)
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

            var vanban = _context.NewsVanBan.Where(p => p.ChuyenMucId == 11 && p.NguoiDuyetId != null).ToList();
            ViewBag.VanBan = vanban;

            var vanbanskip = _context.NewsVanBan.Where(p => p.NguoiDuyetId != null && p.ChuyenMucId == 11).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
            ViewBag.VanBanSkip = vanbanskip;

            ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;
            ViewBag.pPage = vPage;

            var vbhdan = _context.NewsVanBan.Where(p => p.ChuyenMucId == 13).ToList();
            ViewBag.VBHD = vbhdan;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            var loaivb = _context.DMLoaiVanBan.ToList();
            ViewBag.LoaiVB = loaivb;

            return View();
        }

        public IActionResult ListVanBan(int? pCount, int? pPage, int? lvID)
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;

            var vanban = _context.NewsVanBan.Where(p => p.LinhVucId == lvID && p.NguoiDuyetId != null).ToList();
            ViewBag.VanBan = vanban;

            var vanbanskip = _context.NewsVanBan.Where(p => p.LinhVucId == lvID && p.NguoiDuyetId != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);

            ViewBag.VanBanSkip = vanbanskip;

            ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;
            ViewBag.pPage = vPage;

            var loaivb = _context.DMLoaiVanBan.ToList();
            ViewBag.LoaiVB = loaivb;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            ViewBag.LinhVuc1 = lvID;

            return View();
        }

        public IActionResult Detail(Guid? vbUID)
        {
            var vanban = _context.NewsVanBan.Where(p => p.UID == vbUID && p.NguoiDuyetId != null).FirstOrDefault();
            ViewBag.VanBan = vanban;

            var loaivb = _context.DMLoaiVanBan.ToList();
            ViewBag.LoaiVB = loaivb;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;


            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SearchVanBan(int? pCount, int? pPage, int? lvbID, string searchString)
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;

            ViewBag.SearchString = searchString;
            ViewBag.LoaiVanBan = lvbID;

            if (!string.IsNullOrEmpty(searchString) & lvbID != null)
            {
                var vanban = _context.NewsVanBan.Where(p => p.LinhVucId == lvbID && (p.TrichYeu.Contains(searchString) || p.SoKyHieu.Contains(searchString)) && p.NguoiDuyetId != null).ToList();
                ViewBag.VanBan = vanban;

                var vanbanskip = _context.NewsVanBan.Where(p => p.LinhVucId == lvbID && (p.TrichYeu.Contains(searchString) || p.SoKyHieu.Contains(searchString)) && p.NguoiDuyetId != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.VanBanSkip = vanbanskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;
            }
            else if (searchString != null & lvbID == null)
            {
                var vanban = _context.NewsVanBan.Where(p => (p.TrichYeu.Contains(searchString) || p.SoKyHieu.Contains(searchString)) && p.NguoiDuyetId != null).ToList();
                ViewBag.VanBan = vanban;

                var vanbanskip = _context.NewsVanBan.Where(p => (p.TrichYeu.Contains(searchString) || p.SoKyHieu.Contains(searchString)) && p.NguoiDuyetId != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.VanBanSkip = vanbanskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;
            }
            else if (lvbID != null & string.IsNullOrEmpty(searchString))
            {
                var vanban = _context.NewsVanBan.Where(p => p.LinhVucId == lvbID && p.NguoiDuyetId != null).ToList();
                ViewBag.VanBan = vanban;

                var vanbanskip = _context.NewsVanBan.Where(p => p.LinhVucId == lvbID && p.NguoiDuyetId != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.VanBanSkip = vanbanskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;

            }
            else if (lvbID == null & string.IsNullOrEmpty(searchString))
            {
                var vanban = _context.NewsVanBan.Where(p => p.NguoiDuyetId != null).ToList();
                ViewBag.VanBan = vanban;

                var vanbanskip = _context.NewsVanBan.Where(p => p.NguoiDuyetId != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.VanBanSkip = vanbanskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(vanban.Count / (vCount + 1)) + 1;

            }

            ViewBag.pPage = vPage;

            var loaivb = _context.DMLoaiVanBan.ToList();
            ViewBag.LoaiVB = loaivb;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;


            return View();
        }
    }
}
