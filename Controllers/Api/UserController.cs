using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using coreWeb.Models;
using coreWeb.Models.Entities;

using coreWeb.Services;
using Microsoft.EntityFrameworkCore;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _userService = new UserService(context);
            _context = context;
        }

        [HttpGet]
        //[DbAuthorize]
        public IActionResult List()
        {
            //var user = new UserClaim(HttpContext);
            //var obj = _userService.GetListUser();

            var obj = _context.User
                      .Select(x => new { x.Id, x.UserName, x.FullName, x.Role.RoleName, x.Unit.UnitName, x.Phone, x.RoleId, x.UnitId, x.isActive })
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
        public IActionResult Add([FromBody] User obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.User.Add(obj);
                _context.SaveChanges();
                return Ok(obj.Id);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        //[DbAuthorize]
        public IActionResult Update([FromBody] User obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                var _user = _context.User.Where(x => x.Id == obj.Id).FirstOrDefault();
                obj.Password = _user.Password;
                _context.Entry(_user).State = EntityState.Detached;
                _context.User.Update(obj);
                _context.SaveChanges();
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        // [DbAuthorize]
        public IActionResult UpdateProfile([FromBody] User obj)
        {
            var user = new UserClaim(HttpContext);
            try
            {
                var _user = _context.User.Where(x => x.Id == user.UserId).FirstOrDefault();
                _user.FullName = obj.FullName;
                _user.Phone = obj.Phone;
                //_context.Entry(_user).State = EntityState.Detached;
                _context.User.Update(_user);
                _context.SaveChanges();

                return Ok(1);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        // [DbAuthorize]
        public IActionResult ChangePass([FromBody] ChangePassModel model)
        {
            var user = new UserClaim(HttpContext);

            int kq = _userService.ChangePass(user.UserId, model.PasswordOld, model.PasswordNew);
            if (kq == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        //[DbAuthorize]
        public IActionResult ResetPass([FromBody] User obj)
        {
            //var user = new UserClaim(HttpContext);

            int kq = _userService.ResetPass(obj.Id);
            if (kq == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpPost]
        //[DbAuthorize]
        public IActionResult Delete([FromBody] User obj)
        {
            //var user = new UserClaim(HttpContext);
            try
            {
                _context.User.Remove(obj);
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
        public IActionResult Info()
        {
            var user = new UserClaim(HttpContext);
            var objUser = (from p in _context.User
                           where p.Id == user.UserId
                           select new
                           {
                               p.UserName,
                               p.RoleId
                           }).FirstOrDefault();
            if (objUser != null)
            {
                return Ok(objUser);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        //[DbAuthorize]
        public IActionResult GetInfo([FromBody] User obj)
        {
            //var user = new UserClaim(HttpContext);
            var objUser = (from p in _context.User
                           where p.UserName == obj.UserName
                           select new
                           {
                               p.RoleId,
                               p.UserName,
                               p.FullName,
                               p.Phone
                           }).FirstOrDefault();
            if (objUser != null)
            {
                return Ok(objUser);
            }
            else
            {
                return NoContent();
            }
        }

        public class ChangePassModel
        {
            // public string UserName { get; set; }
            public string PasswordOld { get; set; }
            public string PasswordNew { get; set; }
        }
    }
}
