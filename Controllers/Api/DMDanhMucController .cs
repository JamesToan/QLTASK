using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using coreWeb.Models;

using coreWeb.Models.Entities;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DMDanhMucController : ControllerBase
    {
        private ApplicationDbContext _context;


        public DMDanhMucController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult YeuCau()
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var DMNhanSu    = _context.NhanSu.ToList();
                    var DMTinhTrang = _context.Status.ToList();
                    var DMTrangThai = _context.States.ToList();
                    var DMDonVi     = _context.DonViYeuCau.ToList();
                    var DMDichVu    = _context.DichVu.Where(dm => dm.IsActive == true).Include(e=>e.DonVi).OrderByDescending(e =>e.Id).ToList();
                    var DMJira      = _context.Jira.Where(dm => dm.IsActive == true).ToList(); 
                    if (DMNhanSu != null && DMTinhTrang != null && DMTrangThai != null && DMDichVu != null && DMJira != null && DMDonVi != null)
                    {
                        var result = new
                        {
                            DMNhanSu = DMNhanSu,
                            DMTinhTrang = DMTinhTrang,
                            DMTrangThai = DMTrangThai,
                            DMDichVu = DMDichVu,
                            DMJira = DMJira,
                            DMDonVi = DMDonVi,
                        };
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet]
        //public IActionResult SVL()
        //{
        //    try
        //    {
        //        var user = new UserClaim(HttpContext);
        //        if (user.RoleId == 1 || user.RoleId == 2)
        //        {
        //            var DMTinh = _context.DMTinh.Where(dm => dm.HieuLuc == true).Include(tinh => tinh.Huyen).ToList();
        //            var DMHuyen = _context.DMHuyen.Where(dm => dm.HieuLuc == true && dm.TinhId == "80").ToList();
        //            var DMKhuCumCN = _context.DMKhuCumCN.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMGioiTinh = _context.DMGioiTinh.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMLoaiHinh = _context.DMLoaiHinh.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMNganhKinhTe = _context.DMNganhKinhTe.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMNganhNghe = _context.DMNganhNghe.Where(dm => dm.HieuLuc == true).ToList(); var DMMucLuong = _context.DMMucLuong.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMNgoaiNgu = _context.DMNgoaiNgu.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMTinHoc = _context.DMTinHoc.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMThoiGianLamViec = _context.DMThoiGianLamViec.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMVanHoa = _context.DMVanHoa.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMVanBang = _context.DMVanBang.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMViTriCongViec = _context.DMViTriCongViec.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMDaoTaoNghe = _context.DaoTaoNghe.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMThiTruong = _context.DMThiTruong.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMTinhTrang = _context.DMTinhTrang.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMTinhTrangDangKy = _context.DMTinhTrangDangKy.Where(dm => dm.HieuLuc == true).ToList();
        //            var DMLoaiSan = _context.DMLoaiSan.Where(dm => dm.HieuLuc == true).ToList();
        //            if (DMTinh != null && DMGioiTinh != null && DMLoaiHinh != null && DMNganhNghe != null && DMNgoaiNgu != null && DMTinHoc != null && DMThoiGianLamViec != null && DMVanBang != null && DMViTriCongViec != null)
        //            {
        //                var result = new
        //                {
        //                    DMTinh = DMTinh,
        //                    DMHuyen = DMHuyen,
        //                    DMKhuCumCN = DMKhuCumCN,
        //                    DMGioiTinh = DMGioiTinh,
        //                    DMLoaiHinh = DMLoaiHinh,
        //                    DMNganhNghe = DMNganhNghe,
        //                    DMNganhKinhTe = DMNganhKinhTe,
        //                    DMMucLuong = DMMucLuong,
        //                    DMNgoaiNgu = DMNgoaiNgu,
        //                    DMTinHoc = DMTinHoc,
        //                    DMThoiGianLamViec = DMThoiGianLamViec,
        //                    DMVanHoa = DMVanHoa,
        //                    DMVanBang = DMVanBang,
        //                    DMViTriCongViec = DMViTriCongViec,
        //                    DMThiTruong = DMThiTruong,
        //                    DMDaoTaoNghe = DMDaoTaoNghe,
        //                    DMTinhTrangDangKy = DMTinhTrangDangKy,
        //                    DMTinhTrang = DMTinhTrang,
        //                    DMLoaiSan = DMLoaiSan
        //                };
        //                return Ok(result);
        //            }
        //            else
        //            {
        //                return NoContent();
        //            }
        //        }
        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
    }


}
