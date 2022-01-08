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
    public class HoSoTimViecController : ControllerBase
    {
        private ApplicationDbContext _context;


        public HoSoTimViecController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var nguoi = _context.NguoiLaoDong.Where(e => e.UserId == user.UserId).SingleOrDefault();
            if (nguoi != null)
            {
                var result = _context.HoSoTimViec.Where(e => e.NguoiLaoDongId == nguoi.Id).SingleOrDefault();
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
                var result = _context.HoSoTimViec
                .Include(e => e.NguoiLaoDong).ThenInclude(e => e.GioiTinh)
                .Include(e => e.NguoiLaoDong).ThenInclude(e => e.TinhTrang)
                .Include(e => e.NganhNghe)
                .Include(e => e.MucLuong)
                .Include(e => e.NoiLamViecTinh)
                .Include(e => e.NoiLamViecHuyen)
                .Include(e => e.VanHoa)
                .Include(e => e.NgoaiNgu)
                .Include(e => e.TinHoc)
                .Include(e => e.VanBang)
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
        public IActionResult Add([FromBody] HoSoTimViec model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var ndl = _context.NguoiLaoDong.Where(e => e.Id == model.NguoiLaoDongId).FirstOrDefault();
                if (ndl != null)
                {
                    model.UID = Guid.NewGuid();
                    model.NguoiLaoDongId = ndl.Id;
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
        public IActionResult Update([FromBody] HoSoTimViec model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var nguoi = _context.NguoiLaoDong.Where(e => e.UserId == user.UserId).SingleOrDefault();
                if (nguoi != null)
                {
                    var result = _context.HoSoTimViec.SingleOrDefault(e => e.NguoiLaoDongId == nguoi.Id);
                    if (result != null) //update
                    {
                        result.ViTriCongViecId = model.ViTriCongViecId;
                        result.TenCongViec = model.TenCongViec;
                        result.NganhNgheId = model.NganhNgheId;
                        result.LoaiHinhId = model.LoaiHinhId;
                        result.MucLuongTu = model.MucLuongTu;
                        result.MucLuongDen = model.MucLuongDen;
                        result.NoiLamViec = model.NoiLamViec;
                        result.NoiLamViecTinhId = model.NoiLamViecTinhId;
                        result.ThoiGianLamViecId = model.ThoiGianLamViecId;
                        result.VanBangId = model.VanBangId;
                        result.ChuyenNganh = model.ChuyenNganh;
                        result.KinhNghiem = model.KinhNghiem;
                        result.NgoaiNguId = model.NgoaiNguId;
                        result.TinHocId = model.TinHocId;
                        result.KyNang = model.KyNang;
                        result.UuDiem = model.UuDiem;
                        result.KhuyetDiem = model.KhuyetDiem;
                        result.NgayCapNhat = DateTime.Now;

                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        model.UID = Guid.NewGuid();
                        model.NguoiLaoDongId = nguoi.Id;
                        model.NgayTao = DateTime.Now;
                        _context.Add(model);
                        _context.SaveChanges();
                        return Ok(1);
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
        public IActionResult Active([FromBody] HoSoTimViec model)
        {
            try
            {
                var result = _context.HoSoTimViec.SingleOrDefault(e => e.Id == model.Id);
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
                var result = _context.HoSoTimViec.SingleOrDefault(e => e.Id == id);
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
