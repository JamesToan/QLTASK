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
    public class DangKyHocNgheController : ControllerBase
    {
        private ApplicationDbContext _context;


        public DangKyHocNgheController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.DangKyHocNghe
                .Include(e => e.NguoiLaoDong)
                .Include(e => e.DaoTaoNghe)
                .Include(e => e.TinhTrang)
                .OrderByDescending(e => e.NgayDangKy)
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
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] DangKyHocNghe model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
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
        public IActionResult Update([FromBody] DangKyHocNghe model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var result = _context.DangKyHocNghe.SingleOrDefault(e => e.Id == model.Id);
                if (user != null && result != null) //update
                {
                    result.DaoTaoNgheId = model.DaoTaoNgheId;
                    result.HoTen = model.HoTen;
                    result.Email = model.Email;
                    result.SoDienThoai = model.SoDienThoai;
                    result.NgayDangKy = model.NgayDangKy;
                    result.TinhTrangId = model.TinhTrangId;
                    _context.Update(result);
                    _context.SaveChanges();
                    return Ok(result.Id);
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
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _context.DangKyHocNghe.SingleOrDefault(e => e.Id == id);
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
    }
}
