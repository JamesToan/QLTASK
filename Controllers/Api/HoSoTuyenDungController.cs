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
    public class HoSoTuyenDungController : ControllerBase
    {
        private ApplicationDbContext _context;


        public HoSoTuyenDungController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get(bool? conHan)
        {
            var user = new UserClaim(HttpContext);
            var dn = _context.DoanhNghiep.Where(e => e.UserId == user.UserId && e.XacThuc == true).FirstOrDefault();
            if (dn != null)
            {
                var result = _context.HoSoTuyenDung.Where(e => e.DoanhNghiepId == dn.Id && (conHan == null || conHan == false || e.HanNop >= DateTime.Today))
                .Include(e => e.DoanhNghiep)
                .Include(e => e.ViTriCongViec)
                .Include(e => e.GioiTinh)
                .Include(e => e.ThoiGianLamViec)
                .Include(e => e.MucLuong)
                .Include(e => e.NoiLamViecTinh)
                .Include(e => e.NoiLamViecHuyen)
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
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.HoSoTuyenDung
                .Include(e => e.DoanhNghiep)
                .Include(e => e.ViTriCongViec)
                .Include(e => e.GioiTinh)
                .Include(e => e.ThoiGianLamViec)
                .Include(e => e.MucLuong)
                .Include(e => e.NoiLamViecTinh)
                .Include(e => e.NoiLamViecHuyen)
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
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] HoSoTuyenDung model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var dn = _context.DoanhNghiep.Where(e => e.Id == model.DoanhNghiepId).FirstOrDefault();
                if (dn != null)
                {
                    var nghe = _context.DMNganhNghe.SingleOrDefault(e => e.Id == model.NganhNgheId);
                    if (nghe != null && string.IsNullOrEmpty(model.TenCongViec))
                    {
                        model.TenCongViec = nghe.TenNganhNghe;
                    }
                    model.UID = Guid.NewGuid();
                    model.DoanhNghiepId = dn.Id;
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
        public IActionResult Update([FromBody] HoSoTuyenDung model)
        {
            try
            {
                var result = _context.HoSoTuyenDung.SingleOrDefault(e => e.Id == model.Id);
                if (result != null) //update
                {
                    var nghe = _context.DMNganhNghe.SingleOrDefault(e => e.Id == model.NganhNgheId);
                    if (nghe != null && string.IsNullOrEmpty(model.TenCongViec))
                    {
                        result.TenCongViec = nghe.TenNganhNghe;
                    }
                    result.ViTriCongViecId = model.ViTriCongViecId;
                    result.TenCongViec = model.TenCongViec;
                    result.SoLuong = model.SoLuong;
                    result.MucLuongTu = model.MucLuongTu;
                    result.MucLuongDen = model.MucLuongDen;
                    result.NoiLamViec = model.NoiLamViec;
                    result.NoiLamViecTinhId = model.NoiLamViecTinhId;
                    result.NoiLamViecHuyenId = model.NoiLamViecHuyenId;
                    result.ThoiGianLamViecId = model.ThoiGianLamViecId;
                    result.HanNop = model.HanNop;
                    result.MoTaCongViec = model.MoTaCongViec;
                    result.YeuCauCongViec = model.YeuCauCongViec;
                    result.PhucLoi = model.PhucLoi;

                    result.NgayCapNhat = DateTime.Now;

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
        public IActionResult Active([FromBody] HoSoTuyenDung model)
        {
            try
            {
                var result = _context.HoSoTuyenDung.SingleOrDefault(e => e.Id == model.Id);
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
                var result = _context.HoSoTuyenDung.SingleOrDefault(e => e.Id == id);
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
