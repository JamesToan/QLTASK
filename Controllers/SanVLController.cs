using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using coreWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class SanVLController : Controller
    {

        private ApplicationDbContext _context;

        public SanVLController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? pCount, int? pPage)
        {
            int vCount = (pCount == null ? 4 : (int)pCount);

            int vPage = (pPage == null ? 1 : (int)pPage);


            var sanvl = _context.SanViecLam.ToList();
            ViewBag.SanVL = sanvl;

            for (int i = 0; i < sanvl.Count; i++)
            {
                int res = DateTime.Compare((DateTime)sanvl[i].NgayKetThuc, DateTime.Now);
                int res1 = DateTime.Compare((DateTime)sanvl[i].NgayBatDau, DateTime.Now);
                if (res>0 & res1<0)
                {
                    var sannow = sanvl[i];
                    ViewBag.SanNow = sannow;


                    var sanvlld = _context.SVLNguoiLaoDong.Where(p => p.SanViecLamId == sannow.Id).ToList();
                    ViewBag.SanVLLD = sanvlld;

                    var nglddistinct = sanvlld.Distinct().ToList();
                    ViewBag.NLDdis = nglddistinct;

                    var songld = sanvlld.Select(p => p.NguoiLaoDongId).Distinct().ToList();
                    ViewBag.SoNLD = songld;

                    var sanvldn = _context.SVLDoanhNghiep.Where(p => p.SanViecLamId == sannow.Id).Include(e => e.DoanhNghiep).Include(e => e.HoSoTuyenDung).ToList();
                    ViewBag.SanVLDN = sanvldn;

                    var sodoanhnghiep = sanvldn.Select(p => p.DoanhNghiepId).Distinct().ToList();
                    ViewBag.SoDoanhNghiep = sodoanhnghiep;

                    ViewBag.SanVLDNCount = sodoanhnghiep.Count;
                    
                    if (sanvlld.Count != 0)
                    {
                       
                        var nguoildcount = sanvlld.Select(p => p.NguoiLaoDongId).Distinct().ToList();
                        ViewBag.SanVLLDCount = nguoildcount.Count();
                    }
                    else
                    {
                        ViewBag.SanVLLDCount = sanvlld.Count;
                    }

                    ViewBag.Tuyendungcount = sanvldn.Count;
                }
               
            }

            var sansdr = _context.SanViecLam.Where(p => p.NgayBatDau > DateTime.Now).ToList();
            ViewBag.SanSDR = sansdr;

            var sanvldnfull = _context.SVLDoanhNghiep.ToList();
            ViewBag.SanVLDNFull = sanvldnfull;

            var sanvlnldfull = _context.SVLNguoiLaoDong.ToList();
            ViewBag.SanVLNLDFull = sanvlnldfull;

            var sanddr = _context.SanViecLam.Where(p => p.NgayKetThuc < DateTime.Now).OrderByDescending(x=>x.Id).ToList();
            ViewBag.Sanddr = sanddr;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var hosotd = _context.HoSoTuyenDung.ToList();
            ViewBag.HSTD = hosotd;

            var vitri = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCV = vitri;

            var mucluong = _context.DMMucLuong.ToList();
            ViewBag.MucLuong = mucluong;

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.GioiTinh = gioitinh;

            var sanvlskip = _context.SanViecLam.ToList().Skip((vPage - 1) * vCount).Take(vCount);
            ViewBag.SanVLSkip = sanvlskip;

            var nguoild = _context.NguoiLaoDong.ToList();
            ViewBag.NguoiLD = nguoild;

            var timviec = _context.HoSoTimViec.ToList();
            ViewBag.TimViec = timviec;

            var huyen = _context.DMHuyen.ToList();
            ViewBag.Huyen = huyen;

            var tinh = _context.DMTinh.ToList();
            ViewBag.Tinh = tinh;

            var xa = _context.DMXa.ToList();
            ViewBag.Xa = xa;

            ViewBag.pageCurrent = vPage;
            if (vPage < 1) vPage = 1;
            int indexFirst = (int)(vCount * (vPage - 1));
            int indexLast = (int)(vCount * vPage);
            ViewBag.Skip = pCount;
            ViewBag.pPage = vPage;

            if (indexLast > sanvl.Count) indexLast = sanvl.Count;
            ViewBag.PageCount = (int)(sanvl.Count / (vCount + 1)) + 1;

            return View();
        }

        public ActionResult SVLDetail(Guid? UID)
        {

            var sanvl = _context.SanViecLam.Where(p => p.UID == UID).FirstOrDefault();
            ViewBag.SanVL = sanvl;

            var sanvldn = _context.SVLDoanhNghiep.Where(p => p.SanViecLamId == sanvl.Id).Include(e => e.DoanhNghiep).Include(e => e.HoSoTuyenDung).ToList();
            ViewBag.SanVLDN = sanvldn;

            var sodoanhnghiep = sanvldn.Select(p => p.DoanhNghiepId).Distinct().ToList();
            ViewBag.SoDoanhNghiep = sodoanhnghiep;
            
            var sanvlld = _context.SVLNguoiLaoDong.Where(p => p.SanViecLamId == sanvl.Id).ToList();
            ViewBag.SanVLLD = sanvlld;

            var gioitinh = _context.DMGioiTinh.ToList();
            ViewBag.GioiTinh = gioitinh;

            var songld = sanvlld.Select(p => p.NguoiLaoDongId).Distinct().ToList();
            ViewBag.SNLD = songld;

            var doanhnghiep = _context.DoanhNghiep.ToList();
            ViewBag.DoanhNghiep = doanhnghiep;

            var timviec = _context.HoSoTimViec.ToList();
            ViewBag.TimViec = timviec;

            var nguoild = _context.NguoiLaoDong.ToList();
            ViewBag.NguoiLD = nguoild;

            var hosotd = _context.HoSoTuyenDung.ToList();
            ViewBag.HSTD = hosotd;

            var vitri = _context.DMViTriCongViec.ToList();
            ViewBag.ViTriCV = vitri;

            var mucluong = _context.DMMucLuong.ToList();
            ViewBag.MucLuong = mucluong;

            var huyen = _context.DMHuyen.ToList();
            ViewBag.Huyen = huyen;

            var tinh = _context.DMTinh.ToList();
            ViewBag.Tinh = tinh;

            var xa = _context.DMXa.ToList();
            ViewBag.Xa = xa;

            ViewBag.SoDoanhNhiep = sodoanhnghiep.Count();
            ViewBag.SoNLD = songld.Count();
            ViewBag.SoHSTD = sanvldn.Count();

            return View();
        }

        [HttpPost]
        public ActionResult JoinNLD(int? sanid, int? ngldid)
        {
            
            var hstd = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == ngldid).Select(e => e.Id).FirstOrDefault();
            try
            {
                var result = _context.SVLNguoiLaoDong.SingleOrDefault(e => e.SanViecLamId == sanid
                && e.NguoiLaoDongId == ngldid);
                if (result != null)
                {
                    result.NgayCapNhat = DateTime.Now;
                    _context.Update(result);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    SVLNguoiLaoDong svlngld = new SVLNguoiLaoDong() {
                        SanViecLamId = sanid,
                        NguoiLaoDongId = ngldid,
                        //HoSoTuyenDungId = hstd,
                        NgayTao = DateTime.Now
                    };
                    //svlngld.NgayTao = DateTime.Now;
                    _context.Add(svlngld);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult JoinDN(int? sanid, int? doanhngid)
        {
            var hstd = _context.HoSoTuyenDung.Where(p => p.DoanhNghiepId == doanhngid).Select(e => e.Id).FirstOrDefault();
            try
            {
                var result = _context.SVLDoanhNghiep.SingleOrDefault(e => e.SanViecLamId == sanid
                && e.DoanhNghiepId == doanhngid);
                if (result == null)
                {
                    SVLDoanhNghiep svlndn = new SVLDoanhNghiep()
                    {
                        SanViecLamId = sanid,
                        DoanhNghiepId = doanhngid,
                        HoSoTuyenDungId = hstd,
                        NgayTao = DateTime.Now
                    };
                    //svlngld.NgayTao = DateTime.Now;
                    _context.Add(svlndn);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Đã tham gia";
                    return RedirectToAction("Index");
                }
              
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public ActionResult NopHoSo(int? sanid, int? ngldid, int? hstdid, int? dnid)
        {
            try
            {
                var result = _context.SVLNguoiLaoDong.SingleOrDefault(e => e.SanViecLamId == sanid
                && e.NguoiLaoDongId == ngldid && e.HoSoTuyenDungId == null );
                
                if (result != null)
                {
                    if (result.HoSoTuyenDungId == null)
                    {

                        result.HoSoTuyenDungId = hstdid;
                        result.NgayCapNhat = DateTime.Now;
                        result.DoanhNghiepId = dnid;
                        _context.Update(result);
                        _context.SaveChanges();
                    }
                    else
                    {
                        SVLNguoiLaoDong svlngld = new SVLNguoiLaoDong()
                        {
                            SanViecLamId = sanid,
                            NguoiLaoDongId = ngldid,
                            HoSoTuyenDungId = hstdid,
                            DoanhNghiepId = dnid,
                            NgayTao = DateTime.Now
                        };
                        _context.Update(svlngld);
                        _context.SaveChanges();
                    }
                   
                    return RedirectToAction("Index");
                }
                else
                {
                    SVLNguoiLaoDong svlngld = new SVLNguoiLaoDong()
                    {
                        SanViecLamId = sanid,
                        NguoiLaoDongId = ngldid,
                        HoSoTuyenDungId = hstdid,
                        DoanhNghiepId = dnid,
                        NgayTao = DateTime.Now
                    };
                    //svlngld.NgayTao = DateTime.Now;
                    _context.Add(svlngld);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
               
                return RedirectToAction("Index");
            } 
        }
    }
}
