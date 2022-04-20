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
                int sum = 0;
                int sumMoiTao = 0;
                int sumDaHT = 0;
                int sumDangXL = 0;
                int sumTrongHan = 0;
                int sumTreHan = 0;
                int sumTrehanHT = 0;
                int sumDunghanHT = 0;
                int sumTronghanXL = 0;
                int sumTrehanXL = 0;
                int sumTronghanCTN = 0;
                int sumTrehanCTN = 0;
                if (user.RoleId == 1)
                {
                    if (DichVuId != null)
                    {
                        sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId).Count();
                        sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId).Count();
                        sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId).Count();
                        sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId).Count();


                        sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                        sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();

                        sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                        sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();

                        sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                        sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();


                        sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                        sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();


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

                            DungHanHT = sumDunghanHT,
                            TreHanHT = sumTrehanHT,
                            TrongHanXL = sumTronghanXL,
                            TreHanXL = sumTrehanXL,
                            TrongHanCTN = sumTronghanCTN,
                            TreHanCTN = sumTrehanCTN,

                            TrongHan = sumTrongHan,
                            TreHan = sumTreHan,
                            Chart = chart.ToList(),
                            //Chart1 = chart1.ToList()
                        };
                        return Ok(result);
                    }
                    else
                    {
                        sum = _context.YeuCau.Count();
                        sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận").Count();
                        sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành").Count();
                        sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý").Count();

                        sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                        sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();

                        sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                        sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();

                        sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                        sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();


                        sumTrongHan = _context.YeuCau.Where(p => p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null).Count();
                        sumTreHan = _context.YeuCau.Where(p => p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null).Count();


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


                            DungHanHT = sumDunghanHT,
                            TreHanHT = sumTrehanHT,
                            TrongHanXL = sumTronghanXL,
                            TreHanXL = sumTrehanXL,
                            TrongHanCTN = sumTronghanCTN,
                            TreHanCTN = sumTrehanCTN,

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
                    if (userInfo.UnitId == 1)
                    {
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();

                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId).Count();


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

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            sum = _context.YeuCau.Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận").Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành").Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý").Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null)).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();

                            sumTrongHan = _context.YeuCau.Where(p => p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null)).Count();


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

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                    }
                    else if (userInfo.UnitId == 2)
                    {
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                           
                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId && e.UnitId != 1)
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            sum = _context.YeuCau.Where(p => p.UnitId != 1).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.UnitId != 1).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.UnitId != 1).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.UnitId != 1).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();

                            //var sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan && p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.UnitId != 1)
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

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
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();

                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan && p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId == userInfo.UnitId).Count();


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

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            sum = _context.YeuCau.Where(p => p.UnitId == userInfo.UnitId).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.UnitId == userInfo.UnitId).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.UnitId == userInfo.UnitId).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.UnitId == userInfo.UnitId).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId == userInfo.UnitId).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId == userInfo.UnitId).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId == userInfo.UnitId).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId == userInfo.UnitId).Count();

                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy >= p.ThoiHan && p.NgayXuLy == null) && p.UnitId == userInfo.UnitId).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId == userInfo.UnitId).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.UnitId == userInfo.UnitId)
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                    }

                }
                else // Roleid  =  3 
                {
                    var userInfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                    var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
                    
                    if (userInfo.UnitId == 1) // TTCNTT
                    {
                        
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.NhanSuId == nhansu.Id).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();

                            //sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                            //sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();


                            var chart = _context.YeuCau.Where(e => (e.StateId != 5 && e.DichVuId == DichVuId) && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == userInfo.Id))
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,
                                //ChuaXL = sumChuaXL,
                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,
                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            
                            if (nhansu.AdminDichVuId != null)
                            {
                                sum = _context.YeuCau.Where(p => p.NhanSuId == nhansu.Id ||  p.DichVuId == nhansu.AdminDichVuId).Count();
                                sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                //var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();
                                sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                //sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                //sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                            }
                            else
                            {
                                sum = _context.YeuCau.Where(p => p.NhanSuId == nhansu.Id).Count();
                                sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                //var sumChuaXL = _context.YeuCau.Where(p => p.States.StateName == "Chưa xử lý" && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id)).Count();

                                sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                                //sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();
                                //sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && ((p.NhanSuId == nhansu.Id || p.NguoiTaoId == userInfo.Id) || p.DichVuId == nhansu.AdminDichVuId)).Count();

                            }

                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && ((e.NhanSuId == nhansu.Id || e.NguoiTaoId == userInfo.Id) || e.DichVuId == nhansu.AdminDichVuId))
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,
                                //ChuaXL = sumChuaXL,
                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,
                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                    }
                    else if (userInfo.UnitId == 2)
                    {
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();


                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.UnitId != 1).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId && e.UnitId != 1)
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            sum = _context.YeuCau.Where(p => p.UnitId != 1).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.UnitId != 1).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.UnitId != 1).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.UnitId != 1).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();


                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.UnitId != 1).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.UnitId != 1).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.UnitId != 1)
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

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
                        if (DichVuId != null)
                        {
                            sum = _context.YeuCau.Where(p => p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();


                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.DichVuId == DichVuId && p.NguoiTaoId == userInfo.Id).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && e.DichVuId == DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == userInfo.Id))
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                        else
                        {
                            sum = _context.YeuCau.Where(p => p.NguoiTaoId == userInfo.Id).Count();
                            sumMoiTao = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && p.NguoiTaoId == userInfo.Id).Count();
                            sumDaHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && p.NguoiTaoId == userInfo.Id).Count();
                            sumDangXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && p.NguoiTaoId == userInfo.Id).Count();

                            sumDunghanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanHT = _context.YeuCau.Where(p => p.States.StateName == "Đã hoàn thành" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.NguoiTaoId == userInfo.Id).Count();

                            sumTronghanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanXL = _context.YeuCau.Where(p => p.States.StateName == "Đang xử lý" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.NguoiTaoId == userInfo.Id).Count();

                            sumTronghanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.NguoiTaoId == userInfo.Id).Count();
                            sumTrehanCTN = _context.YeuCau.Where(p => p.States.StateName == "Chưa tiếp nhận" && (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.NguoiTaoId == userInfo.Id).Count();


                            sumTrongHan = _context.YeuCau.Where(p => (p.NgayXuLy <= p.ThoiHan || p.NgayXuLy == null) && p.NguoiTaoId == userInfo.Id).Count();
                            sumTreHan = _context.YeuCau.Where(p => (p.NgayXuLy > p.ThoiHan && p.NgayXuLy != null) && p.NguoiTaoId == userInfo.Id).Count();


                            var chart = _context.YeuCau.Where(e => e.StateId != 5 && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == userInfo.Id))
                            .Include(hs => hs.States)

                            .GroupBy(hs => hs.StateId)
                            .Select(hs => new { name = hs.FirstOrDefault().States != null ? hs.FirstOrDefault().States.StateName : "Chưa xác định", y = hs.Count() });
                            var result = new
                            {
                                Tong = sum,
                                MoiTao = sumMoiTao,
                                DaHT = sumDaHT,
                                DangXL = sumDangXL,

                                DungHanHT = sumDunghanHT,
                                TreHanHT = sumTrehanHT,
                                TrongHanXL = sumTronghanXL,
                                TreHanXL = sumTrehanXL,
                                TrongHanCTN = sumTronghanCTN,
                                TreHanCTN = sumTrehanCTN,

                                TrongHan = sumTrongHan,
                                TreHan = sumTreHan,
                                Chart = chart.ToList(),
                                //Chart1 = chart1.ToList()
                            };
                            return Ok(result);
                        }
                    }

                }


            }
            catch (System.Exception)
            {
                return NoContent();
            }
        }

        [HttpGet]
        public IActionResult ThongKeCaNhan(SearchParameter search)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
                var dsnhansu = _context.NhanSu.Where(p => p.UnitId == search.UnitId).ToList();
                var yeucau = _context.YeuCau.Where(p => p.UnitId == search.UnitId).ToList();
                for (int i = 0; i < dsnhansu.Count; i++)
                {
                   
                }

                return Ok();
            }
            catch (Exception)
            {

                return NoContent();
            }
        }

        public class SearchParameter
        {
            public int? UnitId { get; set; }
            public DateTime? time1 { get; set; }
            public DateTime? time2 { get; set; }
        }
    }


}
