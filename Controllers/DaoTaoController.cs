using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class DaoTaoController : Controller
    {
        private ApplicationDbContext _context;

        public DaoTaoController(ApplicationDbContext context)
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
            int vNewCatId = (newsCatId == null ? 3 : (int)newsCatId);

            ViewBag.NewCatID = vNewCatId;
            if (ViewBag.NewsCatId == null)
            {
                var tindaotao = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NguoiDuyetId != null).ToList();
                ViewBag.TinDaoTao = tindaotao;

                var tindaotaoskip = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NguoiDuyetId != null).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinDaoTaoSkip = tindaotaoskip;

                ViewBag.PageCount = (int)(tindaotao.Count / (vCount + 1)) + 1;
            }
            else if(ViewBag.NewCatID != null)
            {
                var tindaotao = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId && p.NguoiDuyetId != null).ToList();
                ViewBag.TinDaoTao = tindaotao;

                var tindaotaoskip = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId && p.NguoiDuyetId != null).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinDaoTaoSkip = tindaotaoskip;

                ViewBag.PageCount = (int)(tindaotao.Count / (vCount + 1)) + 1;
            }
           

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 32 && p.NguoiDuyetId !=null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 22 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;
            // var cosodaotao = _context.CoSoDaoTao.ToList();
            // ViewBag.CoSoDaoTao = cosodaotao;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;
            
            var daotao = _context.DaoTaoNghe.ToList();
            ViewBag.DaoTao = daotao;

            var tenkhoahoc = daotao.Select(e => e.TenKhoaHoc).Distinct().ToList();
            ViewBag.TenKhoaHoc = tenkhoahoc;

            var thoigiandt = daotao.Select(e => e.ThoiGianDaoTao).Distinct().ToList();
            ViewBag.ThoiGianDT = thoigiandt;

            var loaihinh = _context.DMLoaiHinhDaoTao.ToList();
            ViewBag.LoaiHinh = loaihinh;

            
            ViewBag.pPage = vPage;

            return View();
        }
        public IActionResult ListDaoTao(int? lhID, int? pCount, int? pPage)
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);
            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.pageCurrent = vPage;
            ViewBag.Skip = vCount;

            // var cosodaotao = _context.CoSoDaoTao.ToList();
            // ViewBag.CoSoDaoTao = cosodaotao;

            var loaihinh = _context.DMVanBang.ToList();
            ViewBag.LoaiHinh = loaihinh;

            var tindaotaoall = _context.NewsBanTin.Where(p => p.ChuyenMucId == 2 && p.NguoiDuyetId != null).ToList();
            ViewBag.TinDaoTaoAll = tindaotaoall;


            

            

            int VBID = (int)lhID;
            ViewBag.VBID = VBID;

            return View();
        }

        public IActionResult DaoTaoDetail(int? dtID, int? newsCatId, string searchStr)
        {
            int vNewCatId = (newsCatId == null ? 2 : (int)newsCatId);

            ViewBag.NewCatID = vNewCatId;
            ViewBag.SearchString = searchStr;

            var tindaotaoall = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId &&  p.NguoiDuyetId != null).ToList();
            ViewBag.TinDaoTaoAll = tindaotaoall;

            var tindaotao = _context.NewsBanTin.Where(p => p.ChuyenMucId == vNewCatId && p.Id == dtID && p.NguoiDuyetId != null).FirstOrDefault();
            ViewBag.TinDaoTao = tindaotao;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var loaihinh = _context.DMLoaiHinhDaoTao.ToList();
            ViewBag.LoaiHinh = loaihinh;

            var daotao = _context.DaoTaoNghe.ToList();
            ViewBag.DaoTao = daotao;

            var tenkhoahoc = daotao.Select(e => e.TenKhoaHoc).Distinct().ToList();
            ViewBag.TenKhoaHoc = tenkhoahoc;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 32 && p.NguoiDuyetId != null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 22 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;
            // var cosodaotao = _context.CoSoDaoTao.ToList();
            // ViewBag.CoSoDaoTao = cosodaotao;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult DaoTaoDetail(string tenkhoahoc, string hoten , string phone, string email)
        //{
        //    var khoahoc = _context.DaoTaoNghe.Where(p => p.TenKhoaHoc == tenkhoahoc).FirstOrDefault();


        //    return View();
        //}

        public ActionResult DangKyHN(string Tenkhoahoc, string Hoten, string Phone, string Email)
        {

            var khoahoc = _context.DaoTaoNghe.Where(p => p.TenKhoaHoc == Tenkhoahoc).FirstOrDefault();

            var nguoild = _context.NguoiLaoDong.Where(p => p.HoTen == Hoten).FirstOrDefault();

            if (nguoild != null)
            {
                DangKyHocNghe hocnghe1 = new DangKyHocNghe()
                {
                    DaoTaoNgheId = khoahoc.Id,
                    NguoiLaoDongId = nguoild.Id,
                    HoTen = Hoten,
                    SoDienThoai = Phone,
                    Email = Email,
                    NgayDangKy = DateTime.Now,
                    TinhTrangId = 0
                };
                _context.Add(hocnghe1);
                _context.SaveChangesAsync();
            }
            else
            {
                DangKyHocNghe hocnghe = new DangKyHocNghe()
                {
                    DaoTaoNgheId = khoahoc.Id,
                    HoTen = Hoten,
                    SoDienThoai = Phone,
                    Email = Email,
                    NgayDangKy = DateTime.Now,
                    TinhTrangId = 0
                    
                };
                _context.Add(hocnghe);
                _context.SaveChangesAsync();
            }

            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);


            return RedirectToAction("Index");
        }

        
        public ActionResult SearchDaoTao(int? pCount, int? pPage, string searchString)
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

            if (searchString == null)
            {
                var tintuc = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2|| p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinTuc = tintuc;

                var tintucall = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NguoiDuyetId != null).ToList();
                ViewBag.TinTucAll = tintucall;
                if (indexLast > tintucall.Count) indexLast = tintucall.Count;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.SearchString = searchString;
            }
            else
            {
                var tintuc = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.TieuDe.Contains(searchString) || (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NoiDung.Contains(searchString) || (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.TomTat.Contains(searchString) && p.NguoiDuyetId != null).Skip((vPage - 1) * vCount).Take(vCount).ToList();
                ViewBag.TinTuc = tintuc;

                var tintucall = _context.NewsBanTin.Where(p => (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.TieuDe.Contains(searchString) || (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.NoiDung.Contains(searchString) || (p.ChuyenMucId == 2 || p.ChuyenMucId == 3 || p.ChuyenMucId == 4 || p.ChuyenMucId == 5) && p.TomTat.Contains(searchString) && p.NguoiDuyetId != null).ToList();
                ViewBag.TinTucAll = tintucall;
                if (indexLast > tintucall.Count) indexLast = tintucall.Count;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.PageCount = (int)(tintucall.Count / (vCount + 1)) + 1;
                ViewBag.SearchString = searchString;
            }
                
           
            

            var chuyenmuc = _context.NewsChuyenMuc.Where(p => p.LoaiChuyenMuc == "news").ToList();
            ViewBag.ChuyenMuc = chuyenmuc;

           

            var daotao = _context.DaoTaoNghe.ToList();
            ViewBag.DaoTao = daotao;

            var tenkhoahoc = daotao.Select(e => e.TenKhoaHoc).Distinct().ToList();
            ViewBag.TenKhoaHoc = tenkhoahoc;

            var thoigiandt = daotao.Select(e => e.ThoiGianDaoTao).Distinct().ToList();
            ViewBag.ThoiGianDT = thoigiandt;

            ViewBag.SearchStr = searchString;

            var videodt = _context.NewsVideo.Where(p => p.ChuyenMucId == 32 && p.NguoiDuyetId != null).ToList();
            ViewBag.Video = videodt;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 22 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }

    }
}
