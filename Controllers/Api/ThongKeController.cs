using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using coreWeb.Models;


namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private ApplicationDbContext _context;


        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                //var sum = _context.HoSoTuyenDung.Sum(e => e.SoLuong);
                //var sumDoanhNghiep = _context.DoanhNghiep.Count(e => e.XacThuc == true);
                //var sumNguoiLaoDong = _context.NguoiLaoDong.Count(e => e.XacThuc == true);
                //var sumSanViecLam = _context.SanViecLam.Count();
                //var sumHoSoTuyenDung = _context.HoSoTuyenDung.Count(e => e.XacThuc == true);
                //var sumHoSoTimViec = _context.HoSoTimViec.Count(e => e.XacThuc == true);
                //var sumSVLDoanhNghiep = _context.SVLDoanhNghiep.Count();
                //var sumSVLNguoiLaoDong = _context.SVLNguoiLaoDong.Count();

                //var chart = _context.HoSoTuyenDung
                //.Include(hs => hs.NoiLamViecTinh)
                //.Include(hs => hs.NoiLamViecHuyen)
                //.GroupBy(hs => hs.NoiLamViecHuyen)
                //.Select(hs => new { name = hs.FirstOrDefault().NoiLamViecHuyen != null ? hs.FirstOrDefault().NoiLamViecHuyen.TenHuyen : "Ngoài tỉnh", y = hs.Sum(e => e.SoLuong) });
                //var result = new
                //{
                //    Tong = sum,
                //    DoanhNghiep = sumDoanhNghiep,
                //    NguoiLaoDong = sumNguoiLaoDong,
                //    SanViecLam = sumSanViecLam,
                //    HoSoTuyenDung = sumHoSoTuyenDung,
                //    HoSoTimViec = sumHoSoTimViec,
                //    SVLDoanhNghiep = sumSVLDoanhNghiep,
                //    SVLNguoiLaoDong = sumSVLNguoiLaoDong,
                //    Chart = chart.ToList()
                //};
                return Ok();
            }
            catch (System.Exception)
            {
                return NoContent();
            }
        }
    }


}
