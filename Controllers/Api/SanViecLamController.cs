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
    public class SanViecLamController : ControllerBase
    {
        private ApplicationDbContext _context;


        public SanViecLamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string uid)
        {
            //var user = new UserClaim(HttpContext);
            var result = _context.SanViecLam.Where(e => e.UID.ToString() == uid)
            // .Where(e => e.UserId == user.UserId)
            .FirstOrDefault();
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
        public IActionResult Select(bool? dangDienRa)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.SanViecLam
                .Include(e => e.LoaiSan)
                .Include(e => e.DoanhNghiepThamGia).ThenInclude(e => e.HoSoTuyenDung)
                .Include(e => e.NguoiLaoDongThamGia)
                .Where(e => dangDienRa == null
                || (dangDienRa == true && e.NgayKetThuc >= DateTime.Today)
                || (dangDienRa == false && e.NgayKetThuc < DateTime.Today))
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
        public IActionResult Add([FromBody] SanViecLam model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
                    model.UID = Guid.NewGuid();
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
        public IActionResult Update([FromBody] SanViecLam model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.SanViecLam.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenSanViecLam = model.TenSanViecLam;
                        result.NoiToChuc = model.NoiToChuc;
                        result.NgayBatDau = model.NgayBatDau;
                        result.NgayKetThuc = model.NgayKetThuc;
                        result.NguoiTaoId = user.UserId;
                        result.NgayTao = DateTime.Now;

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
        public IActionResult Active([FromBody] SanViecLam model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.SanViecLam.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        if (result.NguoiDuyetId == null)
                        {
                            result.NguoiDuyetId = user.UserId;
                            result.NgayDuyet = DateTime.Now;
                        }
                        else
                        {
                            result.NguoiDuyetId = null;
                            result.NgayDuyet = null;
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
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _context.SanViecLam.SingleOrDefault(e => e.Id == id);
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
        [HttpGet]
        public IActionResult GetHoSoTuyenDung(int id)
        {
            //var user = new UserClaim(HttpContext);
            var hsDaThamGia = _context.SVLDoanhNghiep.Where(e => e.SanViecLamId == id)
            .Include(e => e.DoanhNghiep)
            .Include(e => e.HoSoTuyenDung)
            .ToList();
            var hsChuaThamGia = _context.HoSoTuyenDung.Where(e => !hsDaThamGia.Select(h => h.HoSoTuyenDungId).Contains(e.Id) && e.XacThuc == true && (e.HanNop == null || e.HanNop >= DateTime.Today))
            .Include(e => e.DoanhNghiep)
            .ToList();
            if (hsDaThamGia != null && hsChuaThamGia != null)
            {
                return Ok(new
                {
                    ChuaThamGia = hsChuaThamGia,
                    DaThamGia = hsDaThamGia
                });
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        public IActionResult GetHoSoUngTuyen(int id)
        {
            //var user = new UserClaim(HttpContext);
            var hsTuyenDung = _context.SVLDoanhNghiep.Where(e => e.SanViecLamId == id)
            .Include(e => e.DoanhNghiep)
            .Include(e => e.HoSoTuyenDung)
            .ToList();
            var hsDaThamGia = _context.SVLNguoiLaoDong.Where(e => e.SanViecLamId == id)
            .Include(e => e.NguoiLaoDong)
            .Include(e => e.DoanhNghiep)
            .Include(e => e.HoSoTuyenDung)
            .ToList();
            var hsChuaThamGia = _context.NguoiLaoDong.Where(e => !hsDaThamGia.Select(h => h.NguoiLaoDongId).Contains(e.Id) && e.XacThuc == true)
            //.Include(e => e.HoSoTimViecs)
            .ToList();
            if (hsDaThamGia != null && hsChuaThamGia != null)
            {
                return Ok(new
                {
                    TuyenDung = hsTuyenDung,
                    ChuaThamGia = hsChuaThamGia,
                    DaThamGia = hsDaThamGia
                });
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult joinSVLDoanhNghiep([FromBody] SVLDoanhNghiep model)
        {
            try
            {
                var result = _context.SVLDoanhNghiep.SingleOrDefault(e => e.SanViecLamId == model.SanViecLamId
                && e.DoanhNghiepId == model.DoanhNghiepId && e.HoSoTuyenDungId == model.HoSoTuyenDungId);
                if (result != null)
                {
                    result.NgayCapNhat = DateTime.Now;
                    _context.Update(result);
                    _context.SaveChanges();
                    return Ok(result.Id);
                }
                else
                {
                    model.NgayTao = DateTime.Now;
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(model.Id);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult removeSVLDoanhNghiep([FromBody] SVLDoanhNghiep model)
        {
            try
            {
                var result = _context.SVLDoanhNghiep.SingleOrDefault(e => e.Id == model.Id);
                if (result != null)
                {
                    _context.Remove(result);
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
        public IActionResult JoinSVLNguoiLaoDong([FromBody] SVLNguoiLaoDong model)
        {
            try
            {
                var result = _context.SVLNguoiLaoDong.SingleOrDefault(e => e.SanViecLamId == model.SanViecLamId
                && e.NguoiLaoDongId == model.NguoiLaoDongId && e.HoSoTuyenDungId == model.HoSoTuyenDungId);
                if (result != null)
                {
                    result.NgayCapNhat = DateTime.Now;
                    _context.Update(result);
                    _context.SaveChanges();
                    return Ok(result.Id);
                }
                else
                {
                    model.NgayTao = DateTime.Now;
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(model.Id);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult removeSVLNguoiLaoDong([FromBody] SVLNguoiLaoDong model)
        {
            try
            {
                var result = _context.SVLNguoiLaoDong.SingleOrDefault(e => e.Id == model.Id);
                if (result != null)
                {
                    _context.Remove(result);
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
    }


}
