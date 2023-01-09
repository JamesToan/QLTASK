using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoaiYeuCauNewController : ControllerBase
    {

        private ApplicationDbContext _context;

        public LoaiYeuCauNewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Getlistbyid(int id)
        {
            var user = new UserClaim(HttpContext);
            var result = _context.LoaiYeuCauNew.Include(x => x.DichVu).Where(x => x.DichVuId == id).ToList();
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
            if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
            {
                var result = _context.LoaiYeuCauNew.Include(x => x.DichVu).ToList();
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
        public IActionResult Add([FromBody] LoaiYeuCauNew model)
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
        public IActionResult Update([FromBody] LoaiYeuCauNew model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.LoaiYeuCauNew.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenLYC = model.TenLYC;
                        
                        result.DichVuId = model.DichVuId;
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
                var result = _context.LoaiYeuCauNew.SingleOrDefault(e => e.Id == id);
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
