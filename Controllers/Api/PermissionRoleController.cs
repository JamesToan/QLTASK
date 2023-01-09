using System;
using System.Dynamic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using coreWeb.Models;

using coreWeb.Models.Entities;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionRoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PermissionRoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[DbAuthorize]
        public IActionResult List()
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();

            var obj = _context.PermissionRole.Include(p => p.Permission)
                      
                      .ToList();

            if (obj != null)
            {
                return Ok(obj);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        //[DbAuthorize]
        public IActionResult ListActive(int RoleId)
        {
            var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();
            var usernit = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
            if (usernit.UnitId == 1 || usernit.UnitId ==2 )
            {
                var obj = _context.PermissionRole.Include(p => p.Permission)
                      .Where(x => x.RoleId == RoleId && x.IsActive)
                      .ToList();

                if (obj != null)
                {
                    return Ok(obj);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                var obj = _context.PermissionRole.Include(p => p.Permission)
                      .Where(x => x.RoleId == RoleId && x.IsActive && x.PermissionId != 76 && x.PermissionId != 78 && x.PermissionId != 31)
                      .ToList();

                if (obj != null)
                {
                    return Ok(obj);
                }
                else
                {
                    return NoContent();
                }
            }
            
        }
        [HttpPost]
        //[DbAuthorize]
        public IActionResult Add([FromBody] PermissionRole obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.PermissionRole.Add(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch
            {
                return Ok(0);
            }
        }
        [HttpPost]
        //[DbAuthorize]
        public IActionResult Update([FromBody] PermissionRole obj)
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
        //[DbAuthorize]
        public IActionResult Copy([FromBody] dynamic obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                int roleFrom = (int)obj.roleFrom;
                int roleTo = (int)obj.roleTo;
                //delete old per
                var list = _context.PermissionRole.Include(p => p.Permission)
                      .Where(x => x.RoleId == roleTo)
                      .ToList();
                foreach (var per in list)
                {
                    _context.PermissionRole.Remove(per);
                }
                //add new per
                list = _context.PermissionRole.Include(p => p.Permission)
                      .Where(x => x.RoleId == roleFrom)
                      .ToList();
                foreach (var per in list)
                {
                    var newPer = new PermissionRole();
                    newPer.RoleId = roleTo;
                    newPer.PermissionId = per.PermissionId;
                    newPer.IsActive = per.IsActive;
                    _context.PermissionRole.Add(newPer);
                }
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        //[DbAuthorize]
        public IActionResult Delete([FromBody] PermissionRole obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.PermissionRole.Remove(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch
            {
                return Ok(0);
            }
        }

    }


}
