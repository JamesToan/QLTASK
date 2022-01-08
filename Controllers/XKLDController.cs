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
    public class XKLDController : Controller
    {
        private ApplicationDbContext _context;

        public XKLDController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? pCount, int? pPage, int? newsCatId)
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;
            if (newsCatId == null)
            {
                ViewBag.NewsCatId = null;
            }
            else
            {
                ViewBag.NewsCatId = 0;
            }
            int vNewCatId = (newsCatId == null ? 7 : (int)newsCatId);

            ViewBag.NewCatID = vNewCatId;

            if (ViewBag.NewsCatId == null)
            {
                var xkld = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.NguoiDuyetId != null).ToList();
                ViewBag.xkld = xkld;

                var xkldskip = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.NguoiDuyetId != null).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.XKLDskip = xkldskip;
                ViewBag.PageCount = (int)(xkld.Count / (vCount + 1)) + 1;
            }
            else if (ViewBag.NewCatID != 0)
            {
                var xkld = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId && p.NguoiDuyetId != null).ToList();
                ViewBag.xkld = xkld;

                var xkldskip = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId && p.NguoiDuyetId != null).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.XKLDskip = xkldskip;
                ViewBag.PageCount = (int)(xkld.Count / (vCount + 1)) + 1;
            }

           

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 34).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 24).ToList();
            ViewBag.Banner = bannerdt;

           
            ViewBag.pPage = vPage;

            var thitruong = _context.DMThiTruong.ToList();
            ViewBag.ThiTruong = thitruong;
            return View();
        }

        public IActionResult XKLDDetail(int? dtID, int? newsCatId, string searchString)
        {

            var tindaotaoall = _context.NewsBanTin.Where(p => p.ChuyenMucId == newsCatId && p.NguoiDuyetId != null).ToList();
            ViewBag.TinDaoTaoAll = tindaotaoall;

            var tindaotao = _context.NewsBanTin.Where(p => p.ChuyenMucId == newsCatId && p.Id == dtID && p.NguoiDuyetId != null).FirstOrDefault();
            ViewBag.TinDaoTao = tindaotao;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var loaihinh = _context.DMLoaiHinhDaoTao.ToList();
            ViewBag.LoaiHinh = loaihinh;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 34).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 24).ToList();
            ViewBag.Banner = bannerdt;

            ViewBag.NewCatID = newsCatId;

            

            var thitruong = _context.DMThiTruong.ToList();
            ViewBag.ThiTruong = thitruong;

            ViewBag.Searchstring = searchString;

            return View();
        }

        public ActionResult DangKyVL(int thitruong, string Hoten, string Phone , string Email)
        {
            DateTime now = DateTime.Now.Date;

            DangKyViecLam dangky = new DangKyViecLam()
            {
                ThiTruongId = thitruong,
                HoTen = Hoten,
                Email = Email,
                SoDienThoai = Phone,
                NgayDangKy = now,
                TinhTrangId = 0

            };
            _context.Add(dangky);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult SearchXKLD(int? pCount, int? pPage, string searchStr)
        {

            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;

            if (searchStr == null)
            {
                var xkldskip = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId ==  8 || p.ChuyenMucId == 9) && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.xkldskip = xkldskip;

                var xkldall = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.NguoiDuyetId != null).ToList();
                ViewBag.xkldall = xkldall;
                ViewBag.PageCount = (int)(xkldall.Count / (vCount + 1)) + 1;
            }
            else
            {
                var xkldskip = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.TieuDe.Contains(searchStr) || (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.NoiDung.Contains(searchStr) || (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.TomTat.Contains(searchStr) && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.xkldskip = xkldskip;

                var xkldall = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.TieuDe.Contains(searchStr) || (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.NoiDung.Contains(searchStr) || (p.ChuyenMucId == 6 || p.ChuyenMucId == 7 || p.ChuyenMucId == 8 || p.ChuyenMucId == 9) && p.TomTat.Contains(searchStr) && p.NguoiDuyetId != null).ToList();
                ViewBag.xkldall = xkldall;

               

                ViewBag.SearchSrting = searchStr;

                ViewBag.PageCount = (int)(xkldall.Count / (vCount + 1)) + 1;
            }
            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 34).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 24).ToList();
            ViewBag.Banner = bannerdt;
            ViewBag.pPage = vPage;

            return View();
        }
    }
}
