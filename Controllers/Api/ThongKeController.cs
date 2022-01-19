using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using coreWeb.Models;
using System;

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
                var sum = _context.YeuCau.Count();
                var sumChuaHT = _context.YeuCau.Where(p => p.States.StateName == "Chưa hoàn thành").Count();
                var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành").Count();
                var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý").Count();
                var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý").Count();

                var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now).Count();
                var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now).Count();


                //var sumDoanhNghiep = _context.DoanhNghiep.Count(e => e.XacThuc == true);
                //var sumNguoiLaoDong = _context.NguoiLaoDong.Count(e => e.XacThuc == true);
                //var sumSanViecLam = _context.SanViecLam.Count();
                //var sumHoSoTuyenDung = _context.HoSoTuyenDung.Count(e => e.XacThuc == true);
                //var sumHoSoTimViec = _context.HoSoTimViec.Count(e => e.XacThuc == true);
                //var sumSVLDoanhNghiep = _context.SVLDoanhNghiep.Count();
                //var sumSVLNguoiLaoDong = _context.SVLNguoiLaoDong.Count();

                //var chart1 = _context.YeuCau
                
                //.GroupBy(hs => hs.Id)
                //.Select(hs => new { name = hs.FirstOrDefault().ThoiHan < DateTime.Now ? "Trễ hạn" : "Trong hạn", y = hs.Count() });
                var chart = _context.YeuCau.Where(e => e.StateId !=5)
                .Include(hs => hs.States)

                .GroupBy(hs => hs.StateId)
                .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                var result = new
                {
                    Tong = sum,
                    ChuaHT = sumChuaHT,
                    DaHT = sumDaHT,
                    DangXL = sumDangXL,
                    ChuaXL = sumChuaXL,
                    TrongHan = sumTrongHan,
                    TreHan = sumTreHan,
                    Chart = chart.ToList(),
                    //Chart1 = chart1.ToList()
                };
                return Ok(result);
            }
            catch (System.Exception)
            {
                return NoContent();
            }
        }
    }


}
