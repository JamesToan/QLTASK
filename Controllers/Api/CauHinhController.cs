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
    public class CauHinhController : ControllerBase
    {
        private ApplicationDbContext _context;


        public CauHinhController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.CauHinh.ToList();
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
        public IActionResult Update([FromBody] CauHinh model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.CauHinh.SingleOrDefault(e => e.Id == model.Id);
                    if (user != null && result != null) //update
                    {
                        result.MaCauHinh = model.MaCauHinh;
                        result.TenCauHinh = model.TenCauHinh;
                        result.GiaTri = model.GiaTri;
                        result.GhiChu = model.GhiChu;
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
    }
}
