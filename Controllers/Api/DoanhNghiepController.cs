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
    public class DoanhNghiepPageController : ControllerBase
    {
        private ApplicationDbContext _context;


        public DoanhNghiepPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.DoanhNghiep.Where(e => e.UserId == user.UserId)
            // .Include(e => e.NganhNghe)
            // .Include(e => e.LoaiHinh)
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
                var result = _context.DoanhNghiep
                // .Where(e => e.UserId == user.UserId)
                .Include(e => e.NganhKinhTe)
                .Include(e => e.LoaiHinh)
                .Include(e => e.KhuCumCN)
                .Include(e => e.DiaChiTinh)
                .Include(e => e.DiaChiHuyen)
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
        public IActionResult Update([FromBody] DoanhNghiep model)
        {
            try
            {
                // var user = new UserClaim(HttpContext);
                var result = new DoanhNghiep();
                if (model.Id > 0)
                {
                    result = _context.DoanhNghiep.SingleOrDefault(e => e.Id == model.Id);
                }
                if (result != null) //update
                {
                    result.TenDoanhNghiep = model.TenDoanhNghiep;
                    result.MaSoThue = model.MaSoThue;
                    result.NganhKinhTeId = model.NganhKinhTeId;
                    result.LoaiHinhId = model.LoaiHinhId;
                    result.TongSoLaoDong = model.TongSoLaoDong;
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
                    var config = _context.CauHinh.SingleOrDefault(e => e.MaCauHinh == "XacNhanDoanhNghiep");
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
        public IActionResult Active([FromBody] DoanhNghiep model)
        {
            try
            {
                var result = _context.DoanhNghiep.SingleOrDefault(e => e.Id == model.Id);
                if (result != null) //update
                {
                    result.XacThuc = model.XacThuc;
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
                var result = _context.DoanhNghiep.SingleOrDefault(e => e.Id == id);
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
