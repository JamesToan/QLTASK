using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using coreWeb.Models;

using coreWeb.Models.Entities;

using coreWeb.Services;


namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _PermisService;
        private readonly ApplicationDbContext _context;

        public PermissionController(ApplicationDbContext context)
        {
            _PermisService = new PermissionService(context);
            _context = context;
        }

        [HttpGet]
        //[DbAuthorize]
        public IActionResult List()
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();

            var obj = _context.Permission
                      .Select(x => new { x.Id, x.Route, x.Path, x.IsActive })
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
        public IActionResult ListActive()
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();

            var obj = _context.Permission
                      .Select(x => new { x.Id, x.Route, x.Path, x.IsActive })
                      .Where(x => x.IsActive)
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
        public IActionResult ListNotPer(int RoleId)
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();
            var idList = _context.PermissionRole.Where(x => x.RoleId == RoleId).Select(x => x.Permission.Id).ToList();
            var obj = _context.Permission
                      .Select(x => new { x.Id, x.Route, x.Path, x.IsActive })
                      .Where(x => x.IsActive && !idList.Contains(x.Id))
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
        [HttpPost]
        //[DbAuthorize]
        public IActionResult Add([FromBody] Permission obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.Permission.Add(obj);
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
        public IActionResult Update([FromBody] Permission obj)
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
        public IActionResult Active([FromBody] Permission obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                var listPer = _context.PermissionRole
                      .Where(x => x.PermissionId == obj.Id)
                      .ToList();
                foreach (var per in listPer)
                {
                    per.IsActive = obj.IsActive;
                    _context.Update(per);
                }
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
        public IActionResult Delete([FromBody] Permission obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.Permission.Remove(obj);
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
