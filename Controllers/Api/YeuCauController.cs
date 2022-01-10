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
                .Include(e => e.Status)
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
        public IActionResult Select(bool?  thoihan)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.YeuCau
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.Status)
                .Include(e => e.User)
                .Where(e => thoihan == null
                || (thoihan == true && e.ThoiHan >= DateTime.Today)
                || (thoihan == false && e.ThoiHan < DateTime.Today))
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
        public IActionResult Add([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
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
        public IActionResult Update([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenYeuCau = model.TenYeuCau;
                        result.NoiDung = model.NoiDung;
                        result.ThoiHan = model.ThoiHan;
                        result.JiraDaGui = model.JiraDaGui;
                        result.StatusId = model.StatusId;
                        result.StateId = model.StateId;
                        result.DichVuId = model.DichVuId;
                        result.DonVi = model.DonVi;
                        result.NguoiTaoId = user.UserId;
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
                var result = _context.YeuCau.SingleOrDefault(e => e.Id == id);
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
