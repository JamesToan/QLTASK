using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class TimViecController : Controller
    {

        private ApplicationDbContext _context;

        public TimViecController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? pCount, int? pPage)
        {

            int vCount = (pCount == null ? 10 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);

            var timviec = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
            ViewBag.TimViec = timviec.OrderByDescending(p => p.Id).ToList();

            var countTimviec = _context.HoSoTimViec.Where(p => p.XacThuc == true).OrderByDescending(p => p.Id).Skip((vPage - 1) * vCount).Take(vCount).ToList();
            ViewBag.ListTTimViec = countTimviec;

            var vitricongviec = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCongViec = vitricongviec;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var dmTinh = _context.DMTinh.ToList();
            ViewBag.DMTinh = dmTinh;

            var dmHuyen = _context.DMHuyen.Where(p => p.TinhId == "80").ToList();
            ViewBag.DMHuyen = dmHuyen;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            var thoigianlamviec = _context.DMThoiGianLamViec.ToList();
            ViewBag.TGLamViec = thoigianlamviec;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var vanhoa = _context.DMVanHoa.ToList();
            ViewBag.VanHoa = vanhoa;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 15).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            var nguoilaodong = _context.NguoiLaoDong.ToList();
            ViewBag.NguoiLaoDong = nguoilaodong;

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.DMGioiTinh = gioitinh;

            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.Skip = vCount;
            ViewBag.pPage = vPage;

            if (indexLast > timviec.Count) indexLast = timviec.Count;
            ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            

            return View();
        }
        public IActionResult Detail(int hsID)
        {
            
            var timviec = _context.HoSoTimViec.Where(p => p.Id == hsID  & p.XacThuc == true).FirstOrDefault();
            ViewBag.TimViec = timviec;
            if (timviec != null)
            {
                var ldID = timviec.NguoiLaoDongId;

                var nguoilaodong = _context.NguoiLaoDong.Where(p => p.Id == ldID).FirstOrDefault();
                ViewBag.NguoiLaoDong = nguoilaodong;

                var dmTinh = _context.DMTinh.Where(p => p.Id == nguoilaodong.DiaChiTinhId).FirstOrDefault();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.Where(p => p.Id == nguoilaodong.DiaChiHuyenId).FirstOrDefault();
                ViewBag.DMHuyen = dmHuyen;

                var dmxa = _context.DMXa.Where(p => p.Id == nguoilaodong.DiaChiXaId).FirstOrDefault();
                ViewBag.DMXa = dmxa;

                var dmNgoaiNgu = _context.DMNgoaiNgu.Where(p => p.Id == timviec.NgoaiNguId).FirstOrDefault();
                ViewBag.NgoaiNgu = dmNgoaiNgu;

                var tinhoc = _context.DMTinHoc.Where(p => p.Id == timviec.TinHocId).FirstOrDefault();
                ViewBag.TinHoc = tinhoc;
            }

           

            var countTimviec = _context.HoSoTimViec.Skip(10).Take(10).ToList();
            ViewBag.ListTTimViec = countTimviec;

            var vitricongviec = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCongViec = vitricongviec;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            var vanhoa = _context.DMVanHoa.ToList();
            ViewBag.VanHoa = vanhoa;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 15).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

            var dmNganhNghe = _context.DMNganhNghe.ToList();
            ViewBag.DMNganhNghe = dmNganhNghe;

            var thoigianlamviec = _context.DMThoiGianLamViec.ToList();
            ViewBag.TGLamViec = thoigianlamviec;

            var vanbang = _context.DMVanBang.ToList();
            ViewBag.VanBang = vanbang;

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.DMGioiTinh = gioitinh;

          

            return View();
        }


        public ActionResult SearchTimViec(string searchStr, int? searchNN, int? searchDD, int? pCount, int? pPage)
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
             if (string.IsNullOrEmpty(searchStr) && searchDD == null & searchNN != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchNN == null && searchDD == null && searchStr != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr)|| p.NguoiLaoDong.HoTen.Contains(searchStr)) & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.Searchstring = searchStr;
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchNN == null && searchDD != null && searchStr == null)
            {
                var timviec = _context.HoSoTimViec.Where(p => p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.Searchstring = searchStr;
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (string.IsNullOrEmpty(searchStr) && searchDD != null && searchNN != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN && p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN && p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchDD == null && searchNN != null && searchStr != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN && (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => p.NganhNgheId == searchNN && (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.Searchstring = searchStr;
                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchDD != null && searchNN == null && searchStr != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) && p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) && p.NoiLamViecHuyenId == searchDD.ToString() & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Searchstring = searchStr;
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchDD != null && searchNN != null && searchStr != null)
            {
                var timviec = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) && p.NoiLamViecHuyenId == searchDD.ToString() && p.NganhNgheId == searchNN & p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => (p.TenCongViec.Contains(searchStr) || p.NguoiLaoDong.HoTen.Contains(searchStr)) && p.NoiLamViecHuyenId == searchDD.ToString() && p.NganhNgheId == searchNN & p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;

                ViewBag.SNN = _context.DMNganhNghe.Where(p => p.Id == searchNN).Select(e => e.TenNganhNghe).Distinct().FirstOrDefault();
                ViewBag.Searchstring = searchStr;
                ViewBag.SDD = _context.DMHuyen.Where(p => p.Id == searchDD.ToString()).Select(e => e.TenHuyen).Distinct().FirstOrDefault();
                ViewBag.Count = timviec.Count;

                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
            }
            else if (searchDD == null && searchNN == null && searchStr == null)
            {
                var timviec = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
                ViewBag.TimViec = timviec;

                var timviecskip = _context.HoSoTimViec.Where(p => p.XacThuc == true).OrderByDescending(p => p.Id).ToList().Skip((vPage - 1) * vCount).Take(vCount);
                ViewBag.TimViecSkip = timviecskip;
                
                ViewBag.Count = timviec.Count;
                ViewBag.PageCount = (int)(timviec.Count / (vCount + 1)) + 1;
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

            var dmlinhvuc = _context.DMLinhVuc.ToList();
            ViewBag.DMLinhVuc = dmlinhvuc;

            var nguoilaodong = _context.NguoiLaoDong.ToList();
            ViewBag.NguoiLaoDong = nguoilaodong;

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.DMGioiTinh = gioitinh;
            
            var vanhoa = _context.DMVanHoa.ToList();
            ViewBag.VanHoa = vanhoa;

            var cauhinh = _context.CauHinh.Where(p => p.Id == 16).FirstOrDefault();
            ViewBag.CauHinh = cauhinh;

            var bannerdt = _context.NewsBanner.Where(p => p.ChuyenMucId == 21 && p.NguoiDuyetId != null).ToList();
            ViewBag.Banner = bannerdt;

            return View();
        }
    }
}
