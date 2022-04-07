using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NhanSuController : ControllerBase
    {
        private ApplicationDbContext _context;

        public NhanSuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.NhanSu.SingleOrDefault();
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
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1)
            {
                var result = _context.NhanSu.ToList();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            else if (user.RoleId == 2 || user.RoleId == 3)
            {
                var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                if (userinfo.UnitId == 1)
                {
                    var result = _context.NhanSu.Where(p => p.UnitId == 1).ToList();

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
                    var result = _context.NhanSu.Where(p => p.UnitId != 1 && p.UnitId == userinfo.UnitId).ToList();

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
        public IActionResult Add([FromBody] NhanSu model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                if (user != null)
                {
                    model.NguoiTaoId = user.UserId;
                    model.NgayTao = DateTime.Now;
                    
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(model.Id);
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
        public IActionResult Update([FromBody] NhanSu model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.NhanSu.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenNhanSu = model.TenNhanSu;
                        result.NgayCapNhat = DateTime.Now;
                        result.UnitId = model.UnitId;
                        result.DichVuId = model.DichVuId;
                        result.UserId = model.UserId;
                        result.AdminDichVuId = model.AdminDichVuId;
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
                var result = _context.NhanSu.SingleOrDefault(e => e.Id == id);
                if (result != null) //update
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                    return Ok(1);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult getCurrentNS()
        {
            var user = new UserClaim(HttpContext);
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            NhanSuViewModel objUser = new NhanSuViewModel();
            if (user.RoleId ==1)
            {
                if (nhansu != null)
                {
                    if (nhansu.DichVuId != null)
                    {
                        objUser = new NhanSuViewModel { DichVuId = 0, UserId = user.UserId, NhanSuId = nhansu.Id, TenNhanSu = nhansu.TenNhanSu, AdminDichVuId = 0 };
                    }
                }
                
            }
            else
            {
                if (nhansu != null)
                {
                    if (nhansu.DichVuId != null )
                    {
                        if (nhansu.AdminDichVuId != null) // Lấy thông tin nhân sự quản lý dịch vụ
                        {
                            objUser = _context.NhanSu.Where(p => p.UserId == user.UserId).Select(p => new NhanSuViewModel { DichVuId = (int)p.DichVuId, UserId = user.UserId, NhanSuId = nhansu.Id, TenNhanSu = nhansu.TenNhanSu, AdminDichVuId = (int)nhansu.AdminDichVuId }).Single();

                        }
                        else
                        {
                            objUser = _context.NhanSu.Where(p => p.UserId == user.UserId).Select(p => new NhanSuViewModel { DichVuId = (int)p.DichVuId, UserId = user.UserId, NhanSuId = nhansu.Id, TenNhanSu = nhansu.TenNhanSu, AdminDichVuId = 0 }).Single();

                        }
                    }
                    else 
                    {
                        objUser = _context.NhanSu.Where(p => p.UserId == user.UserId).Select(p => new NhanSuViewModel { DichVuId = 0, UserId = user.UserId, NhanSuId = nhansu.Id, TenNhanSu = nhansu.TenNhanSu, AdminDichVuId = 0 }).Single();

                    }
                }
               
            }
            
           
            
            if (objUser != null)
            {
                return Ok(objUser);
            }
            else
            {
                return NoContent();
            }
        }

        public IActionResult selectNSbyDVId(int DichVuId)
        {
            var objNhanSu = _context.NhanSu.Where(p => p.DichVuId == DichVuId).ToList();
            return Ok(objNhanSu);
        }

        public class NhanSuViewModel
        {
            public int DichVuId { get; set; }
            public int UserId { get; set; }
            public int NhanSuId { get; set; }
            public string TenNhanSu { get; set; }
            public int AdminDichVuId { get; set; }
        }

    }
}
