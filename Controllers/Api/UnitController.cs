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
    public class UnitController : ControllerBase
    {
        private ApplicationDbContext _context;


        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _context.Unit;

            var result = _context.Unit
                .Include(x => x.children)
                .Where(u => u.IsActive == true)
                .ToList();
            //return Ok(result);

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
        public IActionResult ListAll()
        {
            //var user = new UserClaim(HttpContext);
            var result = _context.Unit
                .Select(x => new { id = x.Id, label = x.UnitName, children = x.children.Select(h => new { id = h.Id, label = h.UnitName }), parentId = x.ParentId })
                .Include(x => x.children)
                .Where(u => u.parentId == null)
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
        [HttpPost]
        public IActionResult Add([FromBody] Unit obj)
        {
            try
            {
                _context.Unit.Add(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] Unit obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete([FromBody] Unit obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.Remove(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }


}
