using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using coreWeb.Models;

using coreWeb.Models.Entities;


namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ApplicationDbContext _context;


        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            //var user = new UserClaim(HttpContext);
            var obj = _context.Role.ToList();
            if (obj != null)
            {
                return Ok(obj);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Add([FromBody] Role obj)
        {
            try
            {
                _context.Role.Add(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] Role obj)
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
        public IActionResult Delete([FromBody] Role obj)
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
