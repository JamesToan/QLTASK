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
        public IActionResult Dashboard(int? DichVuId)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                if (DichVuId != null)
                {
                    var sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId).Count();
                    var sumChuaHT = _context.YeuCau.Where(p => p.States.StateName == "Chưa hoàn thành" && p.DichVuId == DichVuId).Count();
                    var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId).Count();
                    var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId).Count();
                    var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && p.DichVuId == DichVuId).Count();

                    var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now && p.DichVuId == DichVuId).Count();
                    var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now && p.DichVuId == DichVuId).Count();


                    var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId)
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
                else
                {
                    var sum = _context.YeuCau.Count();
                    var sumChuaHT = _context.YeuCau.Where(p => p.States.StateName == "Chưa hoàn thành" ).Count();
                    var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" ).Count();
                    var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý").Count();
                    var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý").Count();

                    var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now ).Count();
                    var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now).Count();


                    var chart = _context.YeuCau.Where(e => e.StateId != 5)
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
                
            }
            catch (System.Exception)
            {
                return NoContent();
            }
        }
    }


}
