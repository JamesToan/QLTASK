using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuanLyDichVuController : ControllerBase
    {
        private ApplicationDbContext _context;

        public QuanLyDichVuController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.QuanLyDichVu.SingleOrDefault();
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
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
            {
                if (userinfo.UnitId == 1)
                {
                    var result = _context.QuanLyDichVu.
                    Include(e => e.DichVu).
                    Include(e => e.NhanSu).
                    Include(e => e.Unit)
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
                    return NoContent();
                }

            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] QuanLyDichVu model)
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
        public IActionResult Update([FromBody] QuanLyDichVu model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.QuanLyDichVu.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.DichVuId = model.DichVuId;
                        result.NhanSuId = model.NhanSuId;
                        result.UnitId = model.UnitId;
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
                var result = _context.QuanLyDichVu.SingleOrDefault(e => e.Id == id);
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

        [HttpGet]
        public IActionResult getdichVuNhanSu()
        {
            var user = new UserClaim(HttpContext);
            var nhansu = _context.NhanSu.Where(p => p.UserId == user.UserId).FirstOrDefault();
            List<QLDVViewModel> quanLy = new List<QLDVViewModel>();
            if (nhansu != null)
            {
                quanLy = _context.QuanLyDichVu.Where(p => p.NhanSuId == nhansu.Id).Select(e => new QLDVViewModel { DichVuId = (int)e.DichVuId, NhanSuId = nhansu.Id}).Distinct().ToList();

            }
            if (quanLy != null)
            {
                return Ok(quanLy);
            }
            else
            {
                return NoContent();
            }
        }

        public IActionResult getdsnhansudv()
        {
            var user = new UserClaim(HttpContext);
            var quanly = _context.QuanLyDichVu.Select(e => new QLDVViewModel { DichVuId = (int)e.DichVuId, NhanSuId = (int)e.NhanSuId}).Distinct().ToList();

            return Ok(quanly);
        }

        public class QLDVViewModel
        {
            public int DichVuId { get; set; }
            public int NhanSuId { get; set; }
           
        }

    }
}
