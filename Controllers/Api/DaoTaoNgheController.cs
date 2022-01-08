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
    public class DaoTaoNgheController : ControllerBase
    {
        private ApplicationDbContext _context;


        public DaoTaoNgheController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.DaoTaoNghe
                .Include(e => e.LoaiHinhDaoTao)
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
        public IActionResult Add([FromBody] DaoTaoNghe model)
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
        public IActionResult Update([FromBody] DaoTaoNghe model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var result = _context.DaoTaoNghe.SingleOrDefault(e => e.Id == model.Id);
                if (user != null && result != null) //update
                {
                    result.TenKhoaHoc = model.TenKhoaHoc;
                    result.ThoiGianDaoTao = model.ThoiGianDaoTao;
                    result.HocPhi = model.HocPhi;
                    result.LoaiHinhDaoTaoId = model.LoaiHinhDaoTaoId;
                    result.MoTaKhoaHoc = model.MoTaKhoaHoc;
                    result.YeuCauKhoaHoc = model.YeuCauKhoaHoc;
                    model.ThuTu = model.ThuTu;
                    model.HieuLuc = model.HieuLuc;
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
                var result = _context.DaoTaoNghe.SingleOrDefault(e => e.Id == id);
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
