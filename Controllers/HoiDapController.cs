using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class HoiDapController : Controller
    {
        private ApplicationDbContext _context;

        public HoiDapController(ApplicationDbContext context)
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

            var hoidap = _context.NewsHoiDap.Where(p => p.NguoiDuyet != null).ToList();
            ViewBag.HoiDap = hoidap;

            var hoidapSkip = _context.NewsHoiDap.Where(p => p.NguoiDuyet!= null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
            ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

            ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;
            ViewBag.pPage = vPage;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            var user = _context.User.ToList();
            ViewBag.User = user;

            return View();
        }

        public IActionResult ListHoiDap(int? pCount, int? pPage, int? lvID)
        {

            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;
            ViewBag.LinhVucId = lvID;
            var hoidap = _context.NewsHoiDap.Where(p=> p.LinhVucId == lvID & p.NguoiDuyet != null).ToList();
            ViewBag.HoiDap = hoidap;

            var hoidapSkip = _context.NewsHoiDap.Where(p => p.LinhVucId == lvID & p.NguoiDuyet != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
            ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

            ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;
            ViewBag.pPage = vPage;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            var user = _context.User.ToList();
            ViewBag.User = user;

            return View();
        }

        public IActionResult Detail(int? hdID)
        {

            var hoidap = _context.NewsHoiDap.Where(p => p.Id == hdID & p.NguoiDuyet != null).FirstOrDefault();
            ViewBag.HoiDap = hoidap;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            var user = _context.User.ToList();
            ViewBag.User = user;

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SearchHoiDap(int? pCount, int? pPage, int? lvID, string searchString)
        {

            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;
            ViewBag.LinhVucId = lvID;
            ViewBag.Searchstring = searchString;

            if (!string.IsNullOrEmpty(searchString) & lvID != null)
            {
                var hoidap = _context.NewsHoiDap.Where(p => p.LinhVucId == lvID && (p.TieuDe.Contains(searchString) || p.NoiDung.Contains(searchString) || p.TraLoi.Contains(searchString)) & p.NguoiDuyet != null).ToList();
                ViewBag.HoiDap = hoidap;

                var hoidapSkip = _context.NewsHoiDap.Where(p => p.LinhVucId == lvID && (p.TieuDe.Contains(searchString) || p.NoiDung.Contains(searchString) || p.TraLoi.Contains(searchString) ) & p.NguoiDuyet != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;
            }
            else if(searchString != null & lvID == null)
            {
                var hoidap = _context.NewsHoiDap.Where(p => (p.TieuDe.Contains(searchString) || p.NoiDung.Contains(searchString) || p.TraLoi.Contains(searchString)) & p.NguoiDuyet != null).ToList();
                ViewBag.HoiDap = hoidap;

                var hoidapSkip = _context.NewsHoiDap.Where(p => (p.TieuDe.Contains(searchString) || p.NoiDung.Contains(searchString) || p.TraLoi.Contains(searchString)) & p.NguoiDuyet != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;
            }
            else if (lvID != null & string.IsNullOrEmpty(searchString))
            {
                var hoidap = _context.NewsHoiDap.Where(p => p.LinhVucId == lvID & p.NguoiDuyet != null).ToList();
                ViewBag.HoiDap = hoidap;

                var hoidapSkip = _context.NewsHoiDap.Where(p => p.LinhVucId == lvID & p.NguoiDuyet != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;

            }
            else if (lvID == null & string.IsNullOrEmpty(searchString))
            {
                var hoidap = _context.NewsHoiDap.Where(p => p.NguoiDuyet != null).ToList();
                ViewBag.HoiDap = hoidap;

                var hoidapSkip = _context.NewsHoiDap.Where(p => p.NguoiDuyet != null).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.HoiDapSkip = hoidapSkip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(hoidap.Count / (vCount + 1)) + 1;

            }


            ViewBag.pPage = vPage;

            var linhvuc = _context.DMLinhVuc.ToList();
            ViewBag.LinhVuc = linhvuc;

            var user = _context.User.ToList();
            ViewBag.User = user;

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DatCauHoi(string HoTen, string Phone, string Email, int? LinhVuc, string TieuDe, string NoiDung)
        {

            var linhvuc = _context.DMLinhVuc.Where(p => p.Id == LinhVuc).FirstOrDefault();

            var date = DateTime.Now;

            NewsHoiDap hoidap = new NewsHoiDap()
            {
                LinhVucId = LinhVuc,
                TieuDe = TieuDe,
                NoiDung = NoiDung,
                HoTen = HoTen,
                Email = Email,
                SoDienThoai = Phone,
                NgayHoi = date,
                UID = Guid.NewGuid(),
        };
            _context.Add(hoidap);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
