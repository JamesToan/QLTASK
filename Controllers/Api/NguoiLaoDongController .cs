using System;

using System.Linq;

using Microsoft.AspNetCore.Mvc;

using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NguoiLaoDongController : ControllerBase
    {
        private ApplicationDbContext _context;


        public NguoiLaoDongController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.NguoiLaoDong.Where(e => e.UserId == user.UserId)
            // .Include(e => e.GioiTinh)
            // .Include(e => e.DiaChiTinh)
            // .Include(e => e.DiaChiHuyen)
            .SingleOrDefault();
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
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.NguoiLaoDong
                // .Where(e => e.UserId == user.UserId)
                .Include(e => e.GioiTinh)
                .Include(e => e.DiaChiTinh)
                .Include(e => e.DiaChiHuyen)
                .Include(e => e.TinhTrang)
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
        public IActionResult Update([FromBody] NguoiLaoDong model)
        {
            try
            {
                // var user = new UserClaim(HttpContext);
                var result = new NguoiLaoDong();
                if (model.Id > 0)
                {
                    result = _context.NguoiLaoDong.SingleOrDefault(e => e.UserId == model.UserId);
                }
                if (result != null) //update
                {
                    result.HoTen = model.HoTen;
                    result.SoDienThoai = model.SoDienThoai;
                    result.GioiTinhId = model.GioiTinhId;
                    result.NgaySinh = model.NgaySinh;
                    result.SoDinhDanh = model.SoDinhDanh;
                    result.NgayCap = model.NgayCap;
                    result.NoiCap = model.NoiCap;
                    result.MaKhachHang = model.MaKhachHang;
                    result.Email = model.Email;
                    result.SoDienThoai = model.SoDienThoai;
                    result.DiaChi = model.DiaChi;
                    result.DiaChiTinhId = model.DiaChiTinhId;
                    result.DiaChiHuyenId = model.DiaChiHuyenId;

                    _context.Update(result);
                    _context.SaveChanges();
                    return Ok(result.Id);
                }
                else
                {
                    var config = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "XacNhanNguoiLaoDong");
                    model.XacThuc = config != null && config.GiaTri == "1";
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(1);
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
                var result = _context.NguoiLaoDong.SingleOrDefault(e => e.Id == id);
                if (result != null)
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
