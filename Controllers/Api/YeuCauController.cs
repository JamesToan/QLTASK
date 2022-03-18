using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YeuCauController : Controller
    {
        private ApplicationDbContext _context;

        public YeuCauController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.YeuCau
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.Unit)
                .Include(e => e.DonViYeuCau)
                //.Include(e => e.Status)
                .Include(e => e.User).FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        public IActionResult Select(int? StateId, int? DichVuId)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            
            if (user.RoleId == 1)
            {
                if (DichVuId ==1 && StateId != 5)
                {
                    var result = _context.YeuCau.Where(e => e.StateId == StateId )
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.User)
                .Include(e => e.Unit)
                .Include(e => e.DonViYeuCau)
                .OrderByDescending(e => e.ThoiHan)
                .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (StateId ==5 && DichVuId !=1)
                {
                    var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId )
                                   .Include(e => e.DichVu)
                                   .Include(e => e.NhanSu)
                                   .Include(e => e.States)
                                   .Include(e => e.User)
                                   .Include(e => e.Unit)
                                   .Include(e => e.DonViYeuCau)
                                   .OrderByDescending(e => e.ThoiHan)
                                   .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (StateId != 5 && DichVuId != 1)
                {
                    var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId)
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .Include(e => e.Unit)
                                  .Include(e => e.DonViYeuCau)
                                  .OrderByDescending(e => e.ThoiHan)
                                  .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (StateId == 6)
                {
                    var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == 6)
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .Include(e => e.Unit)
                                  .Include(e => e.DonViYeuCau)
                                  .OrderByDescending(e => e.ThoiHan)
                                  .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    var result = _context.YeuCau.Where(e => StateId == null
                || e.DichVuId == null || e.DichVuId == DichVuId || e.StateId == StateId && e.StateId != 6)
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.User)
                .Include(e => e.Unit)
                .Include(e => e.DonViYeuCau)
                .OrderByDescending(e => e.ThoiHan)
                .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                
            }
            else if (user.RoleId == 2)
            {

                QuanLyDichVu quanlydv = new QuanLyDichVu();
                if (nhansu != null)
                {
                    quanlydv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id && p.DichVuId == nhansu.DichVuId).FirstOrDefault();
                }
                if (userinfo.UnitId == 2)
                {
                    if (DichVuId == 1 && StateId != 5 && StateId != 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == StateId && e.UnitId != 1)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.ThoiHan)
                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 5 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.UnitId != 1)
                                       .Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderByDescending(e => e.ThoiHan)
                                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId && e.UnitId != 1)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.ThoiHan)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 6)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == 6 && e.UnitId != 1)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.ThoiHan)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        var result = _context.YeuCau.Where(e => StateId == null && e.UnitId != 1
                    || e.DichVuId == null && e.UnitId == userinfo.UnitId || e.DichVuId == DichVuId && e.UnitId != 1 || e.StateId == StateId && e.StateId != 6 && e.UnitId != 1)
                    .Include(e => e.DichVu)
                    .Include(e => e.NhanSu)
                    .Include(e => e.States)
                    .Include(e => e.User)
                    .Include(e => e.Unit)
                    .Include(e => e.DonViYeuCau)
                    .OrderByDescending(e => e.ThoiHan)
                    .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                }
                else if (userinfo.UnitId == 1) {

                    if (DichVuId == 1 && StateId != 5 && StateId != 6)
                    {
                        List<YeuCau> result = new List<YeuCau>();


                        result = _context.YeuCau.Where(e => e.StateId == StateId)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.Id)
                       .ToList();


                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 5 && DichVuId != 1)
                    {
                        List<YeuCau> result = new List<YeuCau>();

                        result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == DichVuId)
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .Include(e => e.Unit)
                                  .Include(e => e.DonViYeuCau)
                                  .OrderByDescending(e => e.Id)
                                  .ToList();


                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                    {
                        List<YeuCau> result = new List<YeuCau>();

                        result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == DichVuId)
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .Include(e => e.Unit)
                                  .Include(e => e.DonViYeuCau)
                                  .OrderByDescending(e => e.Id)
                                  .ToList();


                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 6 )
                    {
                        List<YeuCau> result = new List<YeuCau>();

                        result = _context.YeuCau.Where(e => e.StateId == 6 )
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .Include(e => e.Unit)
                                  .Include(e => e.DonViYeuCau)
                                  .OrderByDescending(e => e.Id)
                                  .ToList();


                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        var result = _context.YeuCau.Where(e => (StateId == null 
                     || e.StateId == StateId) && e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                    .Include(e => e.DichVu)
                    .Include(e => e.NhanSu)
                    .Include(e => e.States)
                    .Include(e => e.User)
                    .Include(e => e.Unit)
                    .Include(e => e.DonViYeuCau)
                    .OrderByDescending(e => e.Id)
                    .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }

                }
                else
                {
                    if (DichVuId == 1 && StateId != 5 && StateId != 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == StateId && e.UnitId == userinfo.UnitId)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.Id)
                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 5 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.UnitId == userinfo.UnitId)
                                       .Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderByDescending(e => e.Id)
                                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId && e.UnitId == userinfo.UnitId)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.Id)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == 6 && e.UnitId == userinfo.UnitId)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.Id)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        var result = _context.YeuCau.Where(e => StateId == null && e.UnitId == userinfo.UnitId
                    || e.DichVuId == null && e.UnitId == userinfo.UnitId || e.DichVuId == DichVuId && e.UnitId == userinfo.UnitId || e.StateId == StateId && e.StateId != 6 && e.UnitId == userinfo.UnitId)
                    .Include(e => e.DichVu)
                    .Include(e => e.NhanSu)
                    .Include(e => e.States)
                    .Include(e => e.User)
                    .Include(e => e.Unit)
                    .Include(e => e.DonViYeuCau)
                    .OrderByDescending(e => e.Id)
                    .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                }
            

                

            }
            else if(user.RoleId == 3)
            {
                QuanLyDichVu quanlydv = new QuanLyDichVu();
                if (nhansu != null)
                {
                    quanlydv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id && p.DichVuId == nhansu.DichVuId).FirstOrDefault();
                }
                if (userinfo.UnitId == 2)
                {
                    if (DichVuId == 1 && StateId != 5 && StateId != 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == StateId && e.UnitId != 1)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.ThoiHan)
                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 5 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.UnitId != 1 && e.StateId == StateId)
                                       .Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderByDescending(e => e.ThoiHan)
                                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId && e.UnitId != 1)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.ThoiHan)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 6)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == 6 && e.UnitId != 1)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.ThoiHan)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        var result = _context.YeuCau.Where(e => StateId == null && e.UnitId != 1
                    || e.DichVuId == null && e.UnitId == userinfo.UnitId || e.DichVuId == DichVuId && e.UnitId != 1 || e.StateId == StateId && e.StateId != 6 && e.UnitId != 1)
                    .Include(e => e.DichVu)
                    .Include(e => e.NhanSu)
                    .Include(e => e.States)
                    .Include(e => e.User)
                    .Include(e => e.Unit)
                    .Include(e => e.DonViYeuCau)
                    .OrderByDescending(e => e.ThoiHan)
                    .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                }
                else if (userinfo.UnitId == 1)
                {
                    if (quanlydv != null)
                    {
                        if (DichVuId == 1 && StateId != 5 && StateId != 6)
                        {
                            List<YeuCau> result = new List<YeuCau>();
                            if (StateId == 10)
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == quanlydv.DichVuId)
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit)
                                .Include(e => e.DonViYeuCau)
                                .OrderByDescending(e => e.Id)
                                .ToList();
                            }
                            else
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit)
                               .Include(e => e.DonViYeuCau)
                               .OrderByDescending(e => e.Id)
                               .ToList();
                            }


                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId == 5 && DichVuId != 1)
                        {
                            var result = _context.YeuCau.Where(e => e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                                           .Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit)
                                           .Include(e => e.DonViYeuCau)
                                           .OrderByDescending(e => e.Id)
                                           .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                        {
                            List<YeuCau> result = new List<YeuCau>();
                            if (StateId == 10)
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == quanlydv.DichVuId && e.NguoiTaoId == user.UserId)
                                         .Include(e => e.DichVu)
                                         .Include(e => e.NhanSu)
                                         .Include(e => e.States)
                                         .Include(e => e.User)
                                         .Include(e => e.Unit)
                                         .Include(e => e.DonViYeuCau)
                                         .OrderByDescending(e => e.Id)
                                         .ToList();
                            }
                            else
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId && e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                                         .Include(e => e.DichVu)
                                         .Include(e => e.NhanSu)
                                         .Include(e => e.States)
                                         .Include(e => e.User)
                                         .Include(e => e.Unit)
                                         .Include(e => e.DonViYeuCau)
                                         .OrderByDescending(e => e.Id)
                                         .ToList();
                            }

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId == 6)
                        {
                            var result = _context.YeuCau.Where(e => e.StateId == 6 && e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                                          .Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit)
                                          .Include(e => e.DonViYeuCau)
                                          .OrderByDescending(e => e.Id)
                                          .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else
                        {
                            var result = _context.YeuCau.Where(e => (StateId == null
                         || e.StateId == StateId) && e.DichVuId == quanlydv.DichVuId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                        .Include(e => e.DichVu)
                        .Include(e => e.NhanSu)
                        .Include(e => e.States)
                        .Include(e => e.User)
                        .Include(e => e.Unit)
                        .Include(e => e.DonViYeuCau)
                        .OrderByDescending(e => e.Id)
                        .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                    }
                    else
                    {
                        if (DichVuId == 1 && StateId != 5 && StateId != 6)
                        {
                            List<YeuCau> result = new List<YeuCau>();
                            if (StateId == 10)
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId && e.NguoiTaoId == user.UserId)
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit)
                                .Include(e => e.DonViYeuCau)
                                .OrderByDescending(e => e.Id)
                                .ToList();
                            }
                            else
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId  && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit)
                               .Include(e => e.DonViYeuCau)
                               .OrderByDescending(e => e.Id)
                               .ToList();
                            }


                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId == 5 && DichVuId != 1)
                        {
                            var result = _context.YeuCau.Where(e => e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId)
                                           .Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit)
                                           .Include(e => e.DonViYeuCau)
                                           .OrderByDescending(e => e.Id)
                                           .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                        {
                            List<YeuCau> result = new List<YeuCau>();
                            if (StateId == 10)
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId  && e.NguoiTaoId == user.UserId)
                                         .Include(e => e.DichVu)
                                         .Include(e => e.NhanSu)
                                         .Include(e => e.States)
                                         .Include(e => e.User)
                                         .Include(e => e.Unit)
                                         .Include(e => e.DonViYeuCau)
                                         .OrderByDescending(e => e.Id)
                                         .ToList();
                            }
                            else
                            {
                                result = _context.YeuCau.Where(e => e.StateId == StateId  && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                                         .Include(e => e.DichVu)
                                         .Include(e => e.NhanSu)
                                         .Include(e => e.States)
                                         .Include(e => e.User)
                                         .Include(e => e.Unit)
                                         .Include(e => e.DonViYeuCau)
                                         .OrderByDescending(e => e.Id)
                                         .ToList();
                            }

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else if (StateId == 6)
                        {
                            var result = _context.YeuCau.Where(e => e.StateId == 6  && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                                          .Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit)
                                          .Include(e => e.DonViYeuCau)
                                          .OrderByDescending(e => e.Id)
                                          .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                        else
                        {
                            var result = _context.YeuCau.Where(e => (StateId == null
                         || e.StateId == StateId)  && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                        .Include(e => e.DichVu)
                        .Include(e => e.NhanSu)
                        .Include(e => e.States)
                        .Include(e => e.User)
                        .Include(e => e.Unit)
                        .Include(e => e.DonViYeuCau)
                        .OrderByDescending(e => e.Id)
                        .ToList();
                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                return NoContent();
                            }
                        }
                    }
                    
                }
                else
                {
                    if (DichVuId == 1 && StateId != 5 && StateId != 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == StateId && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.Id)
                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 5 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                       .Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderByDescending(e => e.Id)
                                       .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId != 5 && StateId != 6 && DichVuId != 1)
                    {
                        var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.Id)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else if (StateId == 6)
                    {
                        var result = _context.YeuCau.Where(e => e.StateId == 6 && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                      .Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit)
                                      .Include(e => e.DonViYeuCau)
                                      .OrderByDescending(e => e.Id)
                                      .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        var result = _context.YeuCau.Where(e => (StateId == null 
                    || e.DichVuId == null  || e.DichVuId == DichVuId  || e.StateId == StateId) && e.StateId != 6 && e.UnitId == userinfo.UnitId && (e.NhanSuId == nhansu.Id || e.NguoiTaoId == nhansu.UserId))
                    .Include(e => e.DichVu)
                    .Include(e => e.NhanSu)
                    .Include(e => e.States)
                    .Include(e => e.User)
                    .Include(e => e.Unit)
                    .Include(e => e.DonViYeuCau)
                    .OrderByDescending(e => e.Id)
                    .ToList();
                        if (result != null)
                        {
                            return Ok(result);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Selectall(int? StateId, int? DichVuId)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            
            if (user.RoleId == 1 )
            {
                List<YeuCau> result = new List<YeuCau>();
                if (StateId != null && StateId != 5)    
                {
                    result = _context.YeuCau.Where(e => e.StateId == StateId)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.Id)
                       .ToList();
                }
                else
                {
                    result = _context.YeuCau.Where(e => e.StateId != 6)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit)
                       .Include(e => e.DonViYeuCau)
                       .OrderByDescending(e => e.Id)
                       .ToList();
                }
                    
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                

            }
            else if (user.RoleId == 3 || user.RoleId == 2)
            {
               
                if (userinfo.UnitId == 1)
                {
                    
                    List<YeuCau> result = new List<YeuCau>();
                    if (user.RoleId == 2)
                    {
                        result = _context.YeuCau.Where(e => e.StateId != 6 )
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit)
                               .Include(e => e.DonViYeuCau)
                               .OrderByDescending(e => e.Id)
                               .ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(e => e.StateId != 6 && (e.NhanSuId == nhansu.Id|| e.NguoiTaoId == user.UserId))
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit)
                               .Include(e => e.DonViYeuCau)
                               .OrderByDescending(e => e.Id)
                               .ToList();
                    }

                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }


                }
                else if (userinfo.UnitId == 2)
                {
                    var result = _context.YeuCau.Where(e => e.StateId != 6 && e.UnitId != 1)
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit)
                                .Include(e => e.DonViYeuCau)
                                .OrderByDescending(e => e.Id)
                                .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                
                else
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (user.RoleId == 2)
                    {
                        if (StateId != null && StateId != 5)
                        {
                            result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId != 6 && e.UnitId == userinfo.UnitId && e.StateId == 10 )
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit)
                                .Include(e => e.DonViYeuCau)
                                .OrderByDescending(e => e.Id)
                                .ToList();
                        }
                        else
                        {
                            result = _context.YeuCau.Where(e => e.StateId != 6 && e.UnitId == userinfo.UnitId)
                                 .Include(e => e.DichVu)
                                 .Include(e => e.NhanSu)
                                 .Include(e => e.States)
                                 .Include(e => e.User)
                                 .Include(e => e.Unit)
                                 .Include(e => e.DonViYeuCau)
                                 .OrderByDescending(e => e.Id)
                                 .ToList();
                        }
                    }
                    else
                    {
                        if (StateId != null && StateId != 5)
                        {
                            result = _context.YeuCau.Where(e => (e.DichVuId == null || e.DichVuId == DichVuId) && e.StateId != 6 && e.UnitId == userinfo.UnitId && e.StateId == 10 && e.NguoiTaoId == userinfo.Id)
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit)
                                .Include(e => e.DonViYeuCau)
                                .OrderByDescending(e => e.Id)
                                .ToList();
                        }
                        else
                        {
                            result = _context.YeuCau.Where(e => e.StateId != 6 && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                 .Include(e => e.DichVu)
                                 .Include(e => e.NhanSu)
                                 .Include(e => e.States)
                                 .Include(e => e.User)
                                 .Include(e => e.Unit)
                                 .Include(e => e.DonViYeuCau)
                                 .OrderByDescending(e => e.Id)
                                 .ToList();
                        }
                    }
                    
                    
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }



            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                var nhansu = _context.NhanSu.Where(p => p.UserId == userinfo.Id).FirstOrDefault();
                if (user != null)
                {
                    DateTime thoihan = (DateTime) model.ThoiHan;
                    DateTime time = thoihan.AddHours(23).AddMinutes(59).AddSeconds(59);
                    if (userinfo.UnitId != 1)
                    {
                        model.NguoiTaoId = user.UserId;
                        model.NgayTao = DateTime.Now;
                        model.UnitId = userinfo.UnitId;
                        model.MaSoThue = model.MaSoThue;
                        model.ThoiHan = time;
                        model.StateId = 10;
                        _context.Add(model);
                        _context.SaveChanges();

                        var idyeucau = _context.YeuCau.Select(x => x.Id).LastOrDefault();
                        if (model.JiraDaGui != null)
                        {
                            Jira jr = new Jira();
                            jr.LinkJira = model.JiraDaGui;
                            jr.YeuCauId = idyeucau;
                            _context.Jira.Add(jr);
                            _context.SaveChanges();
                        }


                        return Ok(model.Id);
                    }
                    else
                    {
                        model.NguoiTaoId = user.UserId;
                        model.NgayTao = DateTime.Now;
                        model.UnitId = userinfo.UnitId;
                        model.StateId = 10;
                        model.ThoiHan = time;
                        model.MaSoThue = model.MaSoThue;
                        _context.Add(model);
                        _context.SaveChanges();

                        var idyeucau = _context.YeuCau.Select(x => x.Id).LastOrDefault();
                        if (model.JiraDaGui != null)
                        {
                            Jira jr = new Jira();
                            jr.LinkJira = model.JiraDaGui;
                            jr.YeuCauId = idyeucau;
                            _context.Jira.Add(jr);
                            _context.SaveChanges();
                        }


                        return Ok(model.Id);
                    }
                    
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    DateTime thoihan = (DateTime)model.ThoiHan;
                    DateTime time = thoihan.AddHours(23).AddMinutes(59).AddSeconds(59);
                    if (result != null) //update
                    {
                        result.TenYeuCau = model.TenYeuCau;
                        result.NoiDung = model.NoiDung;
                        result.ThoiHan = time;
                        result.JiraDaGui = model.JiraDaGui;
                        result.NguoiGiamSatId = model.NguoiGiamSatId;
                        result.ThoiHanMongMuon = model.ThoiHanMongMuon;
                        result.FileUpload = model.FileUpload;
                        result.NgayYeuCau = model.NgayYeuCau;
                        result.StateId = model.StateId;
                        result.NhanSuId = model.NhanSuId;
                        result.DichVuId = model.DichVuId;
                        result.DonViYeuCauId = model.DonViYeuCauId;
                       
                        result.MaSoThue = model.MaSoThue;
                        result.NgayCapNhat = DateTime.Now;
                        result.NoiDungXuLy = model.NoiDungXuLy;
                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if(user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                    DateTime thoihan = (DateTime)model.ThoiHan;
                    DateTime time = thoihan.AddHours(23).AddMinutes(59).AddSeconds(59);
                    if (result != null ) //update
                    {
                        if (userinfo.UnitId == 1 || userinfo.Id == result.NguoiTaoId)
                        {
                            result.TenYeuCau = model.TenYeuCau;
                            result.NoiDung = model.NoiDung;
                            result.ThoiHan = time;
                            result.JiraDaGui = model.JiraDaGui;
                            result.NhanSuId = model.NhanSuId;
                            result.NguoiGiamSatId = model.NguoiGiamSatId;
                            result.ThoiHanMongMuon = model.ThoiHanMongMuon;
                            result.FileUpload = model.FileUpload;
                            result.NgayYeuCau = model.NgayYeuCau;
                            result.NoiDungXuLy = model.NoiDungXuLy;
                            result.DichVuId = model.DichVuId;
                            result.DonViYeuCauId = model.DonViYeuCauId;
                            result.MaSoThue = model.MaSoThue;
                            result.NgayCapNhat = DateTime.Now;
                            result.StateId = model.StateId;
                        }
                        
                        
                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Completed([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.NoiDungXuLy = model.NoiDungXuLy;

                        result.StateId = 6;
                        result.NgayXuLy = DateTime.Now;

                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Accept(int Id)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == Id);
                    if (result != null) //update
                    {
                       

                        result.StateId = 7;
                        

                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddNew ([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                var nhansu = _context.NhanSu.Where(p => p.UserId == userinfo.Id).FirstOrDefault();
                if (user != null)
                {
                    DateTime thoihan = (DateTime)model.ThoiHan;
                    DateTime time = thoihan.AddHours(23).AddMinutes(59).AddSeconds(59);
                    if (userinfo.UnitId != 1)
                    {
                        model.NguoiTaoId = user.UserId;
                        model.NgayTao = DateTime.Now;
                        model.UnitId = userinfo.UnitId;
                        model.MaSoThue = model.MaSoThue;
                        model.ThoiHan = time;
                        model.StateId = 10;
                        _context.Add(model);
                        _context.SaveChanges();

                        var idyeucau = _context.YeuCau.Select(x => x.Id).LastOrDefault();
                        if (model.JiraDaGui != null)
                        {
                            Jira jr = new Jira();
                            jr.LinkJira = model.JiraDaGui;
                            jr.YeuCauId = idyeucau;
                            _context.Jira.Add(jr);
                            _context.SaveChanges();
                        }


                        return Ok(model.Id);
                    }
                    else
                    {
                        model.NguoiTaoId = user.UserId;
                        model.NgayTao = DateTime.Now;
                        model.UnitId = userinfo.UnitId;
                        model.ThoiHan = time;
                        model.StateId = 10;
                        model.MaSoThue = model.MaSoThue;
                        _context.Add(model);
                        _context.SaveChanges();

                        var idyeucau = _context.YeuCau.Select(x => x.Id).LastOrDefault();
                        if (model.JiraDaGui != null)
                        {
                            Jira jr = new Jira();
                            jr.LinkJira = model.JiraDaGui;
                            jr.YeuCauId = idyeucau;
                            _context.Jira.Add(jr);
                            _context.SaveChanges();
                        }


                        return Ok(model.Id);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Forward(int Id)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == Id);
                    if (result != null) //update
                    {


                        result.StateId = 7;


                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Denies(int Id)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == Id);
                    if (result != null) //update
                    {


                        result.StateId = 10;


                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 )
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == id);
                    var jira = _context.Jira.Where(p => p.LinkJira == result.JiraDaGui && p.YeuCauId == result.Id).FirstOrDefault();
                    var comment = _context.Comment.Where(p => p.YeuCauId == result.Id).ToList();
                    if (result != null) //update
                    {
                        if (jira != null)
                        {
                            _context.Remove(jira);
                        }
                        if (comment != null)
                        {
                            foreach (var item in comment)
                            {
                                _context.Remove(item);
                            }
                           
                        }
                        _context.Remove(result);
                        _context.SaveChanges();


                        return Ok(1);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if(user.RoleId == 3 || user.RoleId == 2)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == id);
                    if (result.NguoiTaoId == user.UserId && result.StateId == 10)
                    {
                        var jira = _context.Jira.Where(p => p.LinkJira == result.JiraDaGui && p.YeuCauId == result.Id).FirstOrDefault();
                        var comment = _context.Comment.Where(p => p.YeuCauId == result.Id).ToList();
                        if (result != null) //update
                        {
                            if (jira != null)
                            {
                                _context.Remove(jira);
                            }
                            if (comment != null)
                            {
                                foreach (var item in comment)
                                {
                                    _context.Remove(item);
                                }

                            }
                            _context.Remove(result);
                            _context.SaveChanges();


                            return Ok(1);
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    else
                    {
                        return NoContent();
                    }

                }
                else
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == id);
                    var jira = _context.Jira.Where(p => p.LinkJira == result.JiraDaGui).FirstOrDefault();
                    var comment = _context.Comment.Where(p => p.YeuCauId == result.Id).ToList();
                    if (result != null) //update
                    {
                        if (jira != null)
                        {
                            _context.Remove(jira);
                        }
                        if (comment != null)
                        {
                            foreach (var item in comment)
                            {
                                _context.Remove(item);
                            }

                        }
                        _context.Remove(result);
                        _context.SaveChanges();


                        return Ok(1);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult getYCUnitId(int id)
        {
            var user = new UserClaim(HttpContext);
            var objUser = _context.YeuCau.Where(p => p.Id == id).FirstOrDefault();
            if (objUser != null)
            {
                return Ok(objUser);
            }
            else
            {
                return NoContent();
            }
        }



        [HttpGet]
        public async Task<string> GetTrangThai(string majira)
        {

            //var client = new RestClient("https://cntt.vnpt.vn/rest/api/2/issue/IT360-224546");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Basic dG9hbmxtLmxhbjpKYW1lczAxMjM0NUA=");
            //request.AddHeader("Cookie", "BIGipServerPool_ttcntt.vnpt.vn_443=!g4vwiZq0EwOTof/necoQOJnflXX/zfOLbohW5wA9AzCdBwH+UQGxahaf+3dEzm92u+6rauG9ygANZ1E=; JSESSIONID=3A028B54660430287B45BA7B61233E4A; atlassian.xsrf.token=BNR2-A9FG-4XCX-SDI4_c3398ff1e416998278db04f70f8845df05a5899a_lin");
            //IRestResponse response = client.Execute(request);

            //Console.WriteLine(split);
           
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            object userInfos = new { Username = userinfo.JiraAcount, Password = userinfo.JiraPass };
            var byteArray = Encoding.ASCII.GetBytes($"{userinfo.JiraAcount}:{userinfo.JiraPass}");
            string encodeString = "Basic " + Convert.ToBase64String(byteArray);
            var client = new System.Net.Http.HttpClient();
            var jsonObj = JsonConvert.SerializeObject(userInfos);
            var connectionUrl = "https://cntt.vnpt.vn/rest/api/2/issue/" + majira;
            StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(connectionUrl),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", encodeString);
            //request.Headers.Add("Cookie", "BIGipServerPool_ttcntt.vnpt.vn_443=!g4vwiZq0EwOTof/necoQOJnflXX/zfOLbohW5wA9AzCdBwH+UQGxahaf+3dEzm92u+6rauG9ygANZ1E=; JSESSIONID=3A028B54660430287B45BA7B61233E4A; atlassian.xsrf.token=BNR2-A9FG-4XCX-SDI4_c3398ff1e416998278db04f70f8845df05a5899a_lin");
            try
            {
                var response = await client.SendAsync(request);

                var dataResult = response.Content.ReadAsStringAsync().Result;
                JObject joResponse = JObject.Parse(dataResult);

                Console.WriteLine(dataResult);
                return dataResult;
            }
            catch (Exception ex)
            {

                return (null);
            }

           

        }

        [HttpPost]
        public async Task<string> sendTeleAsync(int id)
        {

            //var client = new RestClient("https://api.telegram.org/bot1781635918:AAGST0yL3eyKLPEHsp8rT0VRo7E3fNPUmRo/sendMessage?parse_mode=HTML&chat_id=-517927180&text=" + text);
            //var request = new RestRequest(Method.GET);
            //request.AddOrUpdateParameter("application/json", ParameterType.RequestBody);
            //var response = client.Execute(request);
            //return response.Content;

            var yeucau = _context.YeuCau.Where(p => p.Id == id).FirstOrDefault();
            var userinfo = _context.User.Where(p => p.Id == yeucau.NguoiTaoId).FirstOrDefault();
            var state = _context.States.Where(p => p.Id == yeucau.StateId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.Id == yeucau.NhanSuId).FirstOrDefault();
            var text = "";
            DateTime thoihan = (DateTime)yeucau.ThoiHan;
            //if (yeucau.StateId == 9)
            //{

            //    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("yyyy-MM-dd") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n - Tình trạng: <strong>Đã chuyển tới tiếp nhận</strong>";

            //}
            //else if(yeucau.StateId == 10)
            //{
            //    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("yyyy-MM-dd") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong> \n - Tình trạng: <strong>Bị từ chối </strong>";


            //}

            text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("yyyy-MM-dd") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n";


            var client = new System.Net.Http.HttpClient();
            var connectionUrl = "https://api.telegram.org/bot5219391619:AAHl8WwY_8A4WDAzdWljY-xQA-XIcdEWaY0/sendMessage?parse_mode=HTML&chat_id=-687082532&text=" + text;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(connectionUrl),
                Method = HttpMethod.Get
            };
            var response = await client.SendAsync(request);
            var dataResult = response.Content.ReadAsStringAsync().Result;
            JObject joResponse = JObject.Parse(dataResult);
            return dataResult;
        }


        public class YCViewModel
        {
            public int UnitId { get; set; }

        }
    }
}
