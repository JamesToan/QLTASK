using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using coreWeb.Models;
using System;
using coreWeb.Models.Entities;

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
                var user = new UserClaim(HttpContext);
                if (user.RoleId ==1)
                {
                    if (DichVuId != null)
                    {
                        var sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId).Count();
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo" && p.DichVuId == DichVuId).Count();
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
                            MoiTao = sumMoiTao,
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
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo").Count();
                        var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành").Count();
                        var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý").Count();
                        var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý").Count();

                        var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now).Count();
                        var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now).Count();


                        var chart = _context.YeuCau.Where(e => e.StateId != 5)
                        .Include(hs => hs.States)

                        .GroupBy(hs => hs.StateId)
                        .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                        var result = new
                        {
                            Tong = sum,
                            MoiTao = sumMoiTao,
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
                else if (user.RoleId == 2)
                {
                    var userInfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                    if (DichVuId != null)
                    {
                        var sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                        var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                        var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                        var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();

                        var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                        var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();


                        var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId)
                        .Include(hs => hs.States)

                        .GroupBy(hs => hs.StateId)
                        .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                        var result = new
                        {
                            Tong = sum,
                            MoiTao = sumMoiTao,
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
                        var sum = _context.YeuCau.Where(p => p.UnitId == userInfo.UnitId).Count();
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo" && p.UnitId == userInfo.UnitId).Count();
                        var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.UnitId == userInfo.UnitId).Count();
                        var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.UnitId == userInfo.UnitId).Count();
                        var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && p.UnitId == userInfo.UnitId).Count();

                        var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now && p.UnitId == userInfo.UnitId).Count();
                        var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now && p.UnitId == userInfo.UnitId).Count();


                        var chart = _context.YeuCau.Where(e => e.StateId != 5)
                        .Include(hs => hs.States)

                        .GroupBy(hs => hs.StateId)
                        .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                        var result = new
                        {
                            Tong = sum,
                            MoiTao = sumMoiTao,
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
                else
                {
                    var userInfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                    if (DichVuId != null)
                    {
                        var sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                        var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                        var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                        var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();

                        var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                        var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();


                        var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId)
                        .Include(hs => hs.States)

                        .GroupBy(hs => hs.StateId)
                        .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                        var result = new
                        {
                            Tong = sum,
                            MoiTao = sumMoiTao,
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
                        var sum = _context.YeuCau.Where(p => p.NguoiTaoId == userInfo.Id).Count();
                        var sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Mới tạo" && p.NguoiTaoId == userInfo.Id).Count();
                        var sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.NguoiTaoId == userInfo.Id).Count();
                        var sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.NguoiTaoId == userInfo.Id).Count();
                        var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && p.NguoiTaoId == userInfo.Id).Count();

                        var sumTrongHan = _context.YeuCau.Where(p => p.ThoiHan >= DateTime.Now && p.NguoiTaoId == userInfo.Id).Count();
                        var sumTreHan = _context.YeuCau.Where(p => p.ThoiHan < DateTime.Now && p.NguoiTaoId == userInfo.Id).Count();


                        var chart = _context.YeuCau.Where(e => e.StateId != 5)
                        .Include(hs => hs.States)

                        .GroupBy(hs => hs.StateId)
                        .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                        var result = new
                        {
                            Tong = sum,
                            MoiTao = sumMoiTao,
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
                
                
            }
            catch (System.Exception)
            {
                return NoContent();
            }
        }
    }


}
