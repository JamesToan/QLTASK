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
    public class DichVuTheoUnitController : ControllerBase
    {
        private ApplicationDbContext _context;

        public DichVuTheoUnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.DichVuTheoUnit.SingleOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        public IActionResult SelectAll()
        {

            var listdv = _context.DichVuTheoUnit.Include(x =>x.DichVu).Include(x => x.Unit).ToList();

            return Ok(listdv);
        }

        public IActionResult Select()
        {
            var user = new UserClaim(HttpContext);
            var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();

            var listdv = _context.DichVuTheoUnit.Include(e => e.DichVu).
                    Include(e => e.Unit).Where(x => x.UnitId == userinfo.UnitId).ToList();

            return Ok(listdv);

        }

        [HttpPost]
        public IActionResult Add([FromBody] DichVuTheoUnit model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var dvunit = _context.DichVuTheoUnit.ToList();
                bool isXacThuc = false;
                foreach (var item in dvunit)
                {
                    if (item.DichVuId == model.DichVuId && item.UnitId == model.UnitId)
                    {
                        isXacThuc = true;
                    }
                }
                if (user != null && isXacThuc == false)
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
        public IActionResult Update([FromBody] DichVuTheoUnit model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.DichVuTheoUnit.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.DichVuId = model.DichVuId;
                        
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
                var result = _context.DichVuTheoUnit.SingleOrDefault(e => e.Id == id);
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
