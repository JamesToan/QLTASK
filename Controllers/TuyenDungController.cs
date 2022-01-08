using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using coreWeb.Controllers;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class TuyenDungController : Controller
    {

        private ApplicationDbContext _context;

        public TuyenDungController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? pSkip, int? pCount, int? pPage )
        {
            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);

            var tuyendungmoi = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).ToList();
            ViewBag.TuyenDung = tuyendungmoi.OrderByDescending(p => p.Id).ToList();

            var countTuyenDung = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e =>e.MucLuong).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
            ViewBag.ListTuyenDung = countTuyenDung;

            var vitricongviec = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCongViec = vitricongviec;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var dmTinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmTinh;

            var dmHuyen = _context.DMHuyen.ToList();
            ViewBag.DMHuyen = dmHuyen;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            var thoigianlamviec = _context.DMThoiGianLamViec.ToList();
            ViewBag.TGLamViec = thoigianlamviec;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 16).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.Skip = vCount;
            ViewBag.pPage = vPage;

            if (indexLast > tuyendungmoi.Count) indexLast = tuyendungmoi.Count;
            ViewBag.PageCount = (int)(tuyendungmoi.Count / (vCount + 1)) + 1;

            return View();
        }

        public IActionResult Detail(int tdID)
        {
          
            var tuyendung = _context.HoSoTuyenDung.Where(p => p.Id == tdID && p.XacThuc == true).Include(e => e.MucLuong).FirstOrDefault();
            ViewBag.TuyenDung = tuyendung;

            if (tuyendung != null)
            {
                var dnghiepID = tuyendung.DoanhNghiepId;

                var vitricongviec = _context.DMViTriCongViec.Where(p => p.Id == tuyendung.ViTriCongViecId).FirstOrDefault();
                ViewBag.ViTriCongViec = vitricongviec;

                var doanhnghiep = _context.DoanhNghiep.Where(p => p.Id == dnghiepID).FirstOrDefault();
                ViewBag.DoanhNghiep = doanhnghiep;

                var dmTinh = _context.DMTinh.Where(p => p.Id == tuyendung.NoiLamViecTinhId).FirstOrDefault();
                ViewBag.DMTinh = dmTinh;

                var thoigianlamviec = _context.DMThoiGianLamViec.Where(p => p.Id == tuyendung.ThoiGianLamViecId).FirstOrDefault();
                ViewBag.TGLamViec = thoigianlamviec;


            }
            

            var timviec = _context.HoSoTimViec.ToList();
            ViewBag.TimViec = timviec;

            

            var dmHuyen = _context.DMHuyen.ToList();
            ViewBag.DMHuyen = dmHuyen;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 16).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

           

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var nguoild = _context.NguoiLaoDong.ToList();
            ViewBag.NguoiLD = nguoild;

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.DMGioiTinh = gioitinh;

            var dmNgoaiNgu = _context.DMNgoaiNgu.ToList();
            ViewBag.NgoaiNgu = dmNgoaiNgu;

            var tinhoc = _context.DMTinHoc.ToList();
            ViewBag.TinHoc = tinhoc;

            var loaihinh = _context.DMLoaiHinh.ToList();
            ViewBag.LoaiHinh = loaihinh;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }

        
        public ActionResult SearchTuyenDung( string searchStr, int? searchNN, int? searchDD, int? pCount, int? pPage)
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
            ViewBag.NN = searchNN;
            ViewBag.DD = searchDD;
            if (string.IsNullOrEmpty(searchStr) && searchNN == null && searchDD != null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => p.NoiLamViecHuyenId == searchDD.ToString() && p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => p.NoiLamViecHuyenId == searchDD.ToString() && p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip;

                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if (string.IsNullOrEmpty(searchStr) && searchDD == null && searchNN != null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => p.NganhNgheId == searchNN && p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => p.NganhNgheId == searchNN && p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if (searchNN == null && searchDD == null && searchStr != null)
            {
                
                var tuyendung = _context.HoSoTuyenDung.Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.Searchstring = searchStr;
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if(string.IsNullOrEmpty(searchStr) && searchDD !=null && searchNN != null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => p.NganhNgheId == searchNN && p.NoiLamViecHuyenId == searchDD.ToString() && p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => p.NganhNgheId == searchNN && p.NoiLamViecHuyenId == searchDD.ToString() && p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if (searchDD == null && searchNN != null && searchStr !=  null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => p.NganhNgheId == searchNN && (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => p.NganhNgheId == searchNN && (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.Searchstring = searchStr;
                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if (searchDD != null && searchNN == null && searchStr != null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true && p.NoiLamViecHuyenId == searchDD.ToString()).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true && p.NoiLamViecHuyenId == searchDD.ToString()).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault(); 
                ViewBag.Searchstring = searchStr;
                ViewBag.Count = tuyendung.Count;
                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if(searchDD != null && searchNN != null && searchStr != null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true && p.NoiLamViecHuyenId == searchDD.ToString() && p.NganhNgheId == searchNN).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => (p.TenCongViec.Contains(searchStr) || p.DoanhNghiep.TenDoanhNghiep.Contains(searchStr)) && p.XacThuc == true && p.NoiLamViecHuyenId == searchDD.ToString() && p.NganhNgheId == searchNN).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Searchstring = searchStr;
                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id==searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Count = tuyendung.Count;

                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;
            }
            else if (searchDD == null && searchNN == null && searchStr == null)
            {
                var tuyendung = _context.HoSoTuyenDung.Where(p =>  p.XacThuc == true).ToList();
                ViewBag.TuyenDung = tuyendung;

                var tuyendungskip = _context.HoSoTuyenDung.Include(e => e.DoanhNghiep).Include(e => e.GioiTinh).Include(e => e.MucLuong).Where(p => p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TuyenDungSkip = tuyendungskip.OrderByDescending(p => p.Id).ToList();

                ViewBag.PageCount = (int)(tuyendung.Count / (vCount + 1)) + 1;

            }


            var vitricongviec = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCongViec = vitricongviec;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var dmTinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmTinh;

            var dmHuyen = _context.DMHuyen.ToList();
            ViewBag.DMHuyen = dmHuyen;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            var thoigianlamviec = _context.DMThoiGianLamViec.ToList();
            ViewBag.TGLamViec = thoigianlamviec;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 16).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }
    }
}
