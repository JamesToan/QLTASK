using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using coreWeb.Services;
using Hangfire;
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
                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
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
        public IActionResult Select(int? StateId, int? DichVuId, int? LoaiYCId)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            
            if (user.RoleId == 1)
            {
                List<YeuCau> result = new List<YeuCau>();
                if (StateId != 6 )
                {
                    result = _context.YeuCau.Where(e => ((e.StateId == StateId || StateId == 5) && e.StateId != 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId ==5) ).Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                .Include(e => e.DonViYeuCau)
                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                .ToList();
                }
                else
                {
                    result = _context.YeuCau.Where(e => (e.StateId == 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)).Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                .Include(e => e.DonViYeuCau)
                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
            else if (user.RoleId == 2) // quản lý
            {
                
                QuanLyDichVu quanlydv = new QuanLyDichVu();
                List<int?> qldv = new List<int?>();
                if (nhansu != null)
                {
                    qldv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id).Select(p => p.DichVuId).Distinct().ToList();
                    quanlydv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id && p.DichVuId == nhansu.DichVuId).FirstOrDefault();
                }
                if (userinfo.UnitId == 2) // KHTCDN
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId !=6)
                    {
                        result = _context.YeuCau.Where(e => ((e.StateId == StateId || StateId == 5) && e.StateId!=6 )&& (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && e.UnitId != 1).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                                .ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(e => (e.StateId == 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && e.UnitId != 1).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
                else if (userinfo.UnitId == 1) //  CNTT
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId !=6)
                    {
                        result = _context.YeuCau.Where(e => ((e.StateId == StateId || StateId == 5)&& e.StateId !=6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                                .ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(e => (e.StateId == 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
                else
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId != 6)
                    {
                        result = _context.YeuCau.Where(e => ((e.StateId == StateId || StateId == 5) && e.StateId !=6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && e.UnitId == userinfo.UnitId).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                                .ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(e => (e.StateId == 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && e.UnitId == userinfo.UnitId).Include(e => e.DichVu)
                                                .Include(e => e.NhanSu)
                                                .Include(e => e.States)
                                                .Include(e => e.User)
                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                .Include(e => e.DonViYeuCau)
                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
            

                

            }
            else if(user.RoleId == 3) // role nhân viên
            {
                
                QuanLyDichVu quanlydv = new QuanLyDichVu();
                List<int?> qldv = new List<int?>();
                if (nhansu != null)
                {
                    qldv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id).Select(p => p.DichVuId).Distinct().ToList();
                    quanlydv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id && p.DichVuId == nhansu.DichVuId).FirstOrDefault();
                }
                if (userinfo.UnitId == 2) // phòng KHTCDN
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId != 6)
                    {
                        result = _context.YeuCau.Where(p => (p.DichVuId == DichVuId || DichVuId == 1) && ((p.StateId == StateId || StateId == 5) && p.StateId !=6)
                                                                && (p.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && p.UnitId != 1).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan).ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(p => (p.DichVuId == DichVuId || DichVuId == 1) && (p.StateId == 6)
                                                                && (p.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5) && p.UnitId != 1).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau)
                                       .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan).ToList();
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
                else if (userinfo.UnitId == 1) // TT CNTT
                {
                    if (nhansu != null)
                    {
                        qldv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id).Select(p => p.DichVuId).Distinct().ToList();
                        quanlydv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id && p.DichVuId == nhansu.DichVuId).FirstOrDefault();
                    }
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId != 6 && StateId != 9)
                    {
                        result = _context.YeuCau.Where(p => (p.DichVuId == DichVuId || DichVuId == 1)
                                                                && ((p.StateId == StateId || StateId == 5) && p.StateId !=6 && p.StateId != 9) && (p.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)
                                                                && (p.NguoiTaoId == nhansu.UserId || p.NhanSuId == nhansu.Id || p.DichVuId == nhansu.AdminDichVuId || qldv.Contains(p.DichVuId)))
                                                                .Include(e => e.DichVu)
                                                                .Include(e => e.NhanSu)
                                                                .Include(e => e.States)
                                                                .Include(e => e.User)
                                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                                .Include(e => e.DonViYeuCau)
                                                                 .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan).ToList();
                    }
                    else if (StateId == 9)
                    {
                        result = _context.YeuCau.Where(p => (p.DichVuId == DichVuId || DichVuId == 1)
                                                               && ( StateId == 9) && (p.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)
                                                               && (p.NguoiTaoId == nhansu.UserId || p.NhanSuId == nhansu.Id || p.DichVuId == nhansu.AdminDichVuId || qldv.Contains(p.DichVuId)))
                                                               .Include(e => e.DichVu)
                                                               .Include(e => e.NhanSu)
                                                               .Include(e => e.States)
                                                               .Include(e => e.User)
                                                               .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                               .Include(e => e.DonViYeuCau)
                                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan).ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(p => (p.DichVuId == DichVuId || DichVuId == 1) && (p.StateId == 6) && (p.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)
                                                                && (p.NguoiTaoId == nhansu.UserId || p.NhanSuId == nhansu.Id || p.DichVuId == nhansu.AdminDichVuId || qldv.Contains(p.DichVuId) ))
                                                                .Include(e => e.DichVu)
                                                                .Include(e => e.NhanSu)
                                                                .Include(e => e.States)
                                                                .Include(e => e.User)
                                                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                                .Include(e => e.DonViYeuCau)
                                                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan).ToList();
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
                else // các phòng khác
                {
                    List<YeuCau> result = new List<YeuCau>();
                    if (StateId !=6)
                    {
                        result = _context.YeuCau.Where(e => ((e.StateId == StateId || StateId == 5) && e.StateId !=6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)
                                                            && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                                          .Include(e => e.DichVu)
                                                          .Include(e => e.NhanSu)
                                                          .Include(e => e.States)
                                                          .Include(e => e.User)
                                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                          .Include(e => e.DonViYeuCau)
                                                          .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                                          .ToList();
                    }
                    else
                    {
                        result = _context.YeuCau.Where(e => (e.StateId == 6) && (e.DichVuId == DichVuId || DichVuId == 1) && (e.LoaiYeuCauId == LoaiYCId || LoaiYCId == 5)
                                                              && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                                              .Include(e => e.DichVu)
                                                              .Include(e => e.NhanSu)
                                                              .Include(e => e.States)
                                                              .Include(e => e.User)
                                                              .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                                              .Include(e => e.DonViYeuCau)
                                                              .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
            
               
           
            
            if (user.RoleId == 1 ) // ---------------   Admin   -------
            {
                List<YeuCau> result = new List<YeuCau>();
                if (StateId != null && StateId != 5)    
                {
                    result = _context.YeuCau.Where(e => e.StateId == StateId)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                       .Include(e => e.DonViYeuCau)
                       .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                       .ToList();
                }
                else
                {
                    result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9)
                       .Include(e => e.DichVu)
                       .Include(e => e.NhanSu)
                       .Include(e => e.States)
                       .Include(e => e.User)
                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                       .Include(e => e.DonViYeuCau)
                       .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
            else if (user.RoleId == 3 || user.RoleId == 2) //------ QL - NV --------///
            {
                List<int?> qldv = new List<int?>();
                if (nhansu != null)
                {
                    qldv = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id).Select(p => p.DichVuId).Distinct().ToList();
                    
                }
                if (userinfo.UnitId == 1) //------   TT CNTT
                {
                    
                    List<YeuCau> result = new List<YeuCau>();
                    if (user.RoleId == 2)  //------ QL --------///
                    {
                        result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9)
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                               .Include(e => e.DonViYeuCau)
                               .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                               .ToList();
                    }
                    else //------ NV --------///
                    {
                        // select cho nhân sự cntt có admin dịch vụ id có thể xem hết yêu cầu chưa hoàn thành
                        result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9 && ((e.NhanSuId == nhansu.Id|| e.NguoiTaoId == user.UserId || e.DichVuId == nhansu.AdminDichVuId || qldv.Contains(e.DichVuId))))
                               .Include(e => e.DichVu)
                               .Include(e => e.NhanSu)
                               .Include(e => e.States)
                               .Include(e => e.User)
                               .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                               .Include(e => e.DonViYeuCau)
                               .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e=>e.ThoiHan)
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
                else if (userinfo.UnitId == 2)  // P KHTCDN  
                {
                    var result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9 && e.UnitId != 1)
                                .Include(e => e.DichVu)
                                .Include(e => e.NhanSu)
                                .Include(e => e.States)
                                .Include(e => e.User)
                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                .Include(e => e.DonViYeuCau)
                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
                
                else  // Phòng bán hàng còn lại
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
                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                .Include(e => e.DonViYeuCau)
                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                .ToList();
                        }
                        else
                        {
                            result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9 && e.UnitId == userinfo.UnitId)
                                 .Include(e => e.DichVu)
                                 .Include(e => e.NhanSu)
                                 .Include(e => e.States)
                                 .Include(e => e.User)
                                 .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                 .Include(e => e.DonViYeuCau)
                                 .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
                                .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                .Include(e => e.DonViYeuCau)
                                .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
                                .ToList();
                        }
                        else
                        {
                            result = _context.YeuCau.Where(e => e.StateId != 6 && e.StateId != 9 && e.UnitId == userinfo.UnitId && e.NguoiTaoId == userinfo.Id)
                                 .Include(e => e.DichVu)
                                 .Include(e => e.NhanSu)
                                 .Include(e => e.States)
                                 .Include(e => e.User)
                                 .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                 .Include(e => e.DonViYeuCau)
                                 .OrderBy(p => p.LoaiYeuCau.Order).ThenBy(e => e.ThoiHan)
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
                bool isXacThuc = false;
                var yeucau = _context.YeuCau.Where(p=>p.DichVuId==6).ToList();
                foreach (var item in yeucau)
                {
                    if (item.TenYeuCau == model.TenYeuCau && item.UnitId == userinfo.UnitId)
                    {
                        isXacThuc = true;
                    }
                }

                if (user != null && isXacThuc == false)
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

                        _context.YeuCau.Add(model);

                        _context.SaveChanges();
                        foreach (var item in yeucau)
                        {
                            if (item.TenYeuCau == model.TenYeuCau && item.UnitId == userinfo.UnitId)
                            {
                                isXacThuc = true;
                            }
                        }
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
                //var ngaytao = (DateTime)model.NgayTao;
                //var formatnt = ngaytao.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime myDate = DateTime.ParseExact(formatnt, "yyyy-MM-dd HH:mm:ss.fff",
                //                       System.Globalization.CultureInfo.InvariantCulture);
                //var yeucauId = _context.YeuCau.Where(p => p.NguoiTaoId == model.NguoiTaoId && p.UnitId == model.UnitId && (p.NgayTao== model.NgayTao)).FirstOrDefault();
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                  
                    if (result != null) //update
                    {
                        
                        result.TenYeuCau = model.TenYeuCau;
                        result.NoiDung = model.NoiDung;
                        result.ThoiHan = model.ThoiHan;
                        result.JiraDaGui = model.JiraDaGui;
                        result.NguoiGiamSatId = model.NguoiGiamSatId;
                        result.ThoiHanMongMuon = model.ThoiHanMongMuon;
                        result.FileUpload = model.FileUpload;
                        result.NgayYeuCau = model.NgayYeuCau;
                        result.StateId = model.StateId;
                        result.NhanSuId = model.NhanSuId;
                        result.DichVuId = model.DichVuId;
                        result.DonViYeuCauId = model.DonViYeuCauId;
                        result.FileXuLy = model.FileXuLy;
                        result.MaSoThue = model.MaSoThue;
                        if (model.NguoiTaoId == userinfo.Id)
                        {
                            result.NgayXuLy = DateTime.Now;
                            result.NgayCapNhat = DateTime.Now;
                        }
                        else if(model.NguoiTaoId != userinfo.Id)
                        {
                            result.NgayXuLy = DateTime.Now;
                        } 
                        result.NoiDungXuLy = model.NoiDungXuLy;
                        result.LoaiYeuCauId = model.LoaiYeuCauId;
                       
                        
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
                    
                    if (result != null ) //update
                    {
                        if (userinfo.UnitId == 1 || userinfo.Id == result.NguoiTaoId || userinfo.UnitId == 2)
                        {
                            result.TenYeuCau = model.TenYeuCau;
                            result.NoiDung = model.NoiDung;
                            result.ThoiHan = model.ThoiHan;
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
                            if (model.NguoiTaoId == userinfo.Id)
                            {
                                result.NgayXuLy = DateTime.Now;
                                result.NgayCapNhat = DateTime.Now;
                            }
                            else if (model.NguoiTaoId != userinfo.Id)
                            {
                                result.NgayXuLy = DateTime.Now;
                            }
                            result.StateId = model.StateId;
                            result.FileXuLy = model.FileXuLy;
                            result.LoaiYeuCauId = model.LoaiYeuCauId;
                            
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
                var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.NoiDungXuLy = model.NoiDungXuLy;

                        result.StateId = 6;
                        result.NgayXuLy = DateTime.Now;
                        result.FileXuLy = model.FileXuLy;
                        result.NhanSuId = nhansu.Id;
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


                        result.StateId = 9;


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
            
            DateTime ngaytao = (DateTime)yeucau.NgayTao;
            if (yeucau.NoiDungXuLy != null && yeucau.NoiDungXuLy != "" && yeucau.StateId == 7) // Thông báo khi nội dung xử lý khác null hoặc rỗng và đang xử lý
            {
               
                if (yeucau.NoiDungXuLy != null && yeucau.NgayXuLy != null)
                {
                    var xuly = yeucau.NoiDungXuLy.Replace("<p>", "").Replace("</p>", " ");
                    DateTime ngayxuly = (DateTime)yeucau.NgayXuLy;
                    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong>  \n- Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Ngày xử lý: <strong>" + ngayxuly.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>  \n - Nội dung xử lý: <strong>" + xuly + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>";

                }
                else
                {
                    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong>  \n- Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Trạng thái: <strong>" + state.StateName + "</strong>";

                }

            }
            else if (yeucau.NoiDungXuLy == null && yeucau.NoiDungXuLy == "" && yeucau.StateId == 7)// Thông báo khi nội dung xử lý bằng null hoặc rỗng và đang xử lý
            {
                text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong>\n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n";

            }
            else if (yeucau.StateId == 10)  // thông báo khi trạng thái là chưa tiếp nhận
            {
                text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n";

            }
            else if (yeucau.StateId == 6)  // thông báo khi trạng thái hoàn thành
            {
                if (yeucau.NoiDungXuLy != null && yeucau.NgayXuLy != null)
                {
                    var xuly1 = yeucau.NoiDungXuLy.Replace("<p>", "").Replace("</p>", " ");
                    DateTime ngayxuly = (DateTime)yeucau.NgayXuLy;
                    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Ngày xử lý: <strong>" + ngayxuly.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Trạng thái: <strong>" + state.StateName + "</strong> ";

                }
                else
                {
                    text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong>\n - Trạng thái: <strong>" + state.StateName + "</strong> ";

                }

            }
            else
            {
                text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n- Ngày tạo: <strong>" + ngaytao.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("dd/MM/yyyy HH:mm:ss") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n";

            }

            //text = "<strong>🔔 Thông báo</strong> \n - Mã yêu cầu: <strong>YC" + yeucau.Id + "</strong> \n - Yêu cầu: <strong>" + yeucau.TenYeuCau + "</strong> \n - Người tạo: <strong>" + userinfo.FullName + "</strong> \n - Người xử lý: <strong>" + nhansu.TenNhanSu + "</strong> \n - Hạn xử lý: <strong>" + thoihan.ToString("yyyy-MM-dd") + "</strong> \n - Trạng thái: <strong>" + state.StateName + "</strong>  \n";


            var client = new System.Net.Http.HttpClient();
            var connectionUrl = "https://api.telegram.org/bot5219391619:AAHl8WwY_8A4WDAzdWljY-xQA-XIcdEWaY0/sendMessage?parse_mode=HTML&chat_id=-1001221153606&text=" + text;
            //var connectionUrl = "https://api.telegram.org/bot5177700420:AAFx_iGsLrekLA2Xjw3aBspdYf0xcPsS3uA/sendMessage?parse_mode=HTML&chat_id=-728524367&text=" + text;
            //var connectionUrl = "https://api.telegram.org/bot5177700420:AAFx_iGsLrekLA2Xjw3aBspdYf0xcPsS3uA/sendContact?chat_id=-728524367&phone_number=+84856699248&first_name=aaaaa";

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(connectionUrl),
                Method = HttpMethod.Get
            };

            var response = await client.SendAsync(request);
            var dataResult = response.Content.ReadAsStringAsync().Result;

            /// ----- Gửi tin nhắn group riêng
            var user = new UserClaim(HttpContext);
            var userinfo1 = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var unit = _context.Unit.Where(p => p.Id == userinfo1.UnitId).FirstOrDefault();
            HttpResponseMessage response2;
            string dataResult2;
            HttpResponseMessage response3;
            string dataResult3;
            if (unit.ChatId != null)
            {
                var connectionUrl2 = "https://api.telegram.org/bot5219391619:AAHl8WwY_8A4WDAzdWljY-xQA-XIcdEWaY0/sendMessage?parse_mode=HTML&chat_id=" + unit.ChatId + "&text=" + text;

                var request2 = new HttpRequestMessage()
                {
                    RequestUri = new Uri(connectionUrl2),
                    Method = HttpMethod.Get
                };

                response2 = await client.SendAsync(request2);
                dataResult2 = response2.Content.ReadAsStringAsync().Result;
            }
            var unitnguoitao = _context.Unit.Where(p => p.Id == userinfo.UnitId).FirstOrDefault();

            if (unitnguoitao.ChatId != null && unit.ChatId != unitnguoitao.ChatId)
            {
                var connectionUrl3 = "https://api.telegram.org/bot5219391619:AAHl8WwY_8A4WDAzdWljY-xQA-XIcdEWaY0/sendMessage?parse_mode=HTML&chat_id=" + unitnguoitao.ChatId + "&text=" + text;

                var request3 = new HttpRequestMessage()
                {
                    RequestUri = new Uri(connectionUrl3),
                    Method = HttpMethod.Get
                };

                response3 = await client.SendAsync(request3);
                dataResult3 = response3.Content.ReadAsStringAsync().Result;
            }




            //JObject joResponse = JObject.Parse(dataResult);
            //System.Diagnostics.Debug.WriteLine(joResponse);
            //var user_id = (string)joResponse.SelectToken("result.contact.user_id");
            //var connectionUrl1 = "https://api.telegram.org/bot5177700420:AAFx_iGsLrekLA2Xjw3aBspdYf0xcPsS3uA/sendMessage?parse_mode=HTML&chat_id=" + user_id + "&text=" + text;
            //var request1 = new HttpRequestMessage()
            //{
            //    RequestUri = new Uri(connectionUrl1),
            //    Method = HttpMethod.Get
            //};
            //var response1 = await client.SendAsync(request1);
            //var dataResult1 = response1.Content.ReadAsStringAsync().Result;


            return dataResult ;
        }

        [HttpGet]
        public IActionResult GetYCbyTimeRange(DateTime time1 , DateTime time2)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            List<YeuCau> result = new List<YeuCau>();
            if (user.RoleId ==1)
            {
                result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
            }
            else if (user.RoleId == 2)
            {
                if (userinfo.UnitId != 1 && userinfo.UnitId != 2)
                {
                    result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2 && p.UnitId == userinfo.UnitId).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                else if(userinfo.UnitId == 2)
                {
                    result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2 && p.UnitId != 1).Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                      .Include(e => e.DonViYeuCau).ToList();
                }
                else
                {
                    result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                
            }
            else if (user.RoleId == 3)
            {
                result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2 && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == user.UserId || p.DichVuId == nhansu.AdminDichVuId)).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
            }
        
           

            return Ok(result);
        }

        public IActionResult GetValue(int index , DateTime time1 , DateTime time2, int DichVuID, int StateID)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            List<YeuCau> result = new List<YeuCau>();
            if (user.RoleId == 1)
            {
                if (index == 1)
                {
                    result = _context.YeuCau.Where(p => (p.NgayTao >= time1 && p.NgayTao <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                else if (index == 2)
                {
                    result = _context.YeuCau.Where(p => (p.ThoiHan >= time1 && p.ThoiHan <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                else if (index == 3)
                {
                    result = _context.YeuCau.Where(p => (p.NgayXuLy >= time1 && p.NgayXuLy <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                      .Include(e => e.DonViYeuCau).ToList();
                }

            }
            else if(user.RoleId == 2)
            {
                if (nhansu.UnitId == 1)
                {
                    if (index == 1)
                    {
                        result = _context.YeuCau.Where(p => (p.NgayTao >= time1 && p.NgayTao <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 2)
                    {
                        result = _context.YeuCau.Where(p => (p.ThoiHan >= time1 && p.ThoiHan <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 3)
                    {
                        result = _context.YeuCau.Where(p => (p.NgayXuLy >= time1 && p.NgayXuLy <= time2) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                          .Include(e => e.DonViYeuCau).ToList();
                    }
                }
                else if (nhansu.UnitId == 2)
                {
                    if (index == 1)
                    {
                        result = _context.YeuCau.Where(p => (p.NgayTao >= time1 && p.NgayTao <= time2) && p.UnitId != 1 && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 2)
                    {
                        result = _context.YeuCau.Where(p => (p.ThoiHan >= time1 && p.ThoiHan <= time2) && p.UnitId != 1 && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 3)
                    {
                        result = _context.YeuCau.Where(p => (p.NgayXuLy >= time1 && p.NgayXuLy <= time2) && p.UnitId != 1 && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                          .Include(e => e.DonViYeuCau).ToList();
                    }
                }
                else
                {
                    if (index == 1)
                    {
                        result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2 && p.UnitId == userinfo.UnitId && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 2)
                    {
                        result = _context.YeuCau.Where(p => p.ThoiHan >= time1 && p.ThoiHan <= time2 && p.UnitId == userinfo.UnitId && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                           .Include(e => e.NhanSu)
                                           .Include(e => e.States)
                                           .Include(e => e.User)
                                           .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                           .Include(e => e.DonViYeuCau).ToList();
                    }
                    else if (index == 3)
                    {
                        result = _context.YeuCau.Where(p => p.NgayXuLy >= time1 && p.NgayXuLy <= time2 && p.UnitId == userinfo.UnitId && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                          .Include(e => e.DonViYeuCau).ToList();
                    }
                }
                

            }
            else if(user.RoleId == 3)
            {
                if (index == 1)
                {
                    result = _context.YeuCau.Where(p => p.NgayTao >= time1 && p.NgayTao <= time2 && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == user.UserId || p.DichVuId == nhansu.AdminDichVuId) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                else if (index == 2)
                {
                    result = _context.YeuCau.Where(p => p.ThoiHan >= time1 && p.ThoiHan <= time2 && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == user.UserId || p.DichVuId == nhansu.AdminDichVuId) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                       .Include(e => e.NhanSu)
                                       .Include(e => e.States)
                                       .Include(e => e.User)
                                       .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                       .Include(e => e.DonViYeuCau).ToList();
                }
                else if (index == 3)
                {
                    result = _context.YeuCau.Where(p => p.NgayXuLy >= time1 && p.NgayXuLy <= time2 && (p.NhanSuId == nhansu.Id || p.NguoiTaoId == user.UserId || p.DichVuId == nhansu.AdminDichVuId) && (p.DichVuId == DichVuID || DichVuID == 1) && (p.StateId == StateID || StateID == 5)).Include(e => e.DichVu)
                                      .Include(e => e.NhanSu)
                                      .Include(e => e.States)
                                      .Include(e => e.User)
                                      .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                      .Include(e => e.DonViYeuCau).ToList();
                }

            }

            return Ok(result);
        }

        public IActionResult GetYCDiaBan(int UnitId, int? NhanSuId)
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            List<YeuCau> result = new List<YeuCau>();

            if (userinfo.UnitId != 1 && userinfo.UnitId != 2)
            {
                if (UnitId != 1 && UnitId != 2)
                {
                    
                    result = _context.YeuCau.Where(p => p.UnitId == UnitId).Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                          .Include(e => e.DonViYeuCau).ToList();
                }
               
            }
            else
            {
                result = _context.YeuCau.Where(p => p.UnitId == UnitId).Include(e => e.DichVu)
                                          .Include(e => e.NhanSu)
                                          .Include(e => e.States)
                                          .Include(e => e.User)
                                          .Include(e => e.Unit).Include(e => e.LoaiYeuCau)
                                          .Include(e => e.DonViYeuCau).ToList();
            }
            



            return Ok(result);
        }

        public ActionResult TraCuuDS(int? DonViId, int? DichVuId, string TuNgay, string DenNgay)
        {
            var objbhtn = new DanhSachThucHienYeuCauService(_context);
            List<DanhSachThucHienYeuCau> ds = new List<DanhSachThucHienYeuCau>();
            if (DonViId ==null)
            {
                ds = objbhtn.danhSach(-1, (int)DichVuId, TuNgay, DenNgay);
            }
            else
            {
               ds = objbhtn.danhSach((int)DonViId, (int)DichVuId, TuNgay, DenNgay);
            }
            

            return Ok(ds);
        }

        public ActionResult TraCuuDSNgayTao(int? DonViId, int? DichVuId, string TuNgay, string DenNgay)
        {
            var objbhtn = new ThongKeCaNhanNgayTaoService(_context);
            List<DanhSachThucHienYeuCau> ds = new List<DanhSachThucHienYeuCau>();
            if (DonViId == null)
            {
                ds = objbhtn.danhSach(-1, (int)DichVuId, TuNgay, DenNgay);
            }
            else
            {
                ds = objbhtn.danhSach((int)DonViId, (int)DichVuId, TuNgay, DenNgay);
            }


            return Ok(ds);
        }

        public ActionResult TraCuuDSUnit(int? DonViId, int? DichVuId, string TuNgay, string DenNgay)
        {
            var objbhtn = new DanhSachThucHienYeuCauDonViService(_context);
            List<DanhSachThucHienYeuCauDonVi> ds = new List<DanhSachThucHienYeuCauDonVi>();
            if (DonViId == null)
            {
                ds = objbhtn.danhSachDonVi(-1, (int)DichVuId, TuNgay, DenNgay);
            }
            else
            {
                ds = objbhtn.danhSachDonVi((int)DonViId, (int)DichVuId, TuNgay, DenNgay);
            }


            return Ok(ds);
        }

        public ActionResult TraCuuLoaiYC(int? DonViId, string TuNgay, string DenNgay)
        {
            var objbhtn = new TraCuuLoaiYeuCauService(_context);
            List<TraCuuLoaiYeuCau> ds = new List<TraCuuLoaiYeuCau>();
            if (DonViId == null)
            {
                ds = objbhtn.tracuuLoai(-1, TuNgay, DenNgay);
            }
            else
            {
                ds = objbhtn.tracuuLoai((int)DonViId, TuNgay, DenNgay);
            }


            return Ok(ds);
        }

        public async Task<ActionResult> SaveCommentJira()
        {

            DateTime date1 = new DateTime(2022, 8, 25, 0, 0, 0);
            //DateTime date2 = new DateTime(2022, 6, 30, 23, 0, 0);
            var jiraacount = _context.User.Where(p => p.Id == 1080).Select(p => new { p.Id, p.JiraAcount, p.JiraPass }).FirstOrDefault();
            var listyeucau = _context.YeuCau.Where(p => p.StateId != 9 && p.JiraDaGui != null && p.NgayTao > date1 && p.StateId != 6).ToList();
            var comment = "";

            foreach (var item in listyeucau)
            {
                if (item.JiraDaGui != null)
                {
                    var jiralink = item.JiraDaGui;
                    var split = jiralink.Split('/');
                    var majira = "";

                    for (var i = 0; i < split.Length; i++)
                    {
                        majira = split[split.Length - 1];
                    }

                    object userInfos = new { Username = jiraacount.JiraAcount, Password = jiraacount.JiraPass };
                    var byteArray = Encoding.ASCII.GetBytes($"{jiraacount.JiraAcount}:{jiraacount.JiraPass}");
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
                    var response = await client.SendAsync(request);
                    if (response.StatusCode.ToString() != "Forbidden")
                    {
                        var dataResult = response.Content.ReadAsStringAsync().Result;
                        JObject joResponse = JObject.Parse(dataResult);
                        if (joResponse["fields"] != null)
                        {
                            JObject jObjectnew = (JObject)joResponse["fields"]["comment"];
                            JArray userResponseArray = (JArray)jObjectnew["comments"];
                            JObject jObjectnew1 = (JObject)joResponse["fields"]["status"];
                            string status = jObjectnew1["name"].Value<string>();
                            if (status != "Resolved" && status != "Cancelled" && status != "Closed" && status != "Deployed")
                            {
                                foreach (JObject ja in userResponseArray.Children<JObject>())
                                {
                                    foreach (JProperty val in ja.Properties())
                                    {
                                        if (val.Name == "body")
                                        {
                                            comment += (string)val.Value;
                                        }
                                        if (val.Name == "updateAuthor")
                                        {
                                            var userName = val.Value["displayName"];
                                            comment += " - " + userName.ToString();
                                        }
                                        if (val.Name == "updated")
                                        {
                                            var time = val.Value;
                                            var date = time.ToString();
                                            DateTime enteredDate = DateTime.Parse(date);
                                            var format = enteredDate.ToString("dd/MM/yyyy");
                                            comment += " - " + (string)time.ToString() + "; ";
                                        }

                                    }

                                }

                                item.CommentJira = comment;
                                _context.Update(item);
                                _context.SaveChanges();
                                comment = "";
                            }
                            

                        }
                    }
                    

                }
            }

            return Ok();
        }


        public ActionResult callSaveCommentJira()
        {
            var JobName = "SaveComment";
            // first job
            RecurringJob.AddOrUpdate(
                JobName,
                () => SaveCommentJira(),
                "0 */12 * * *", TimeZoneInfo.Local);
            return Ok();
        }
    }
}
