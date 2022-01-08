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
    public class NewsBannerController : ControllerBase
    {
        private ApplicationDbContext _context;


        public NewsBannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string uid)
        {
            //var user = new UserClaim(HttpContext);
            var result = _context.NewsBanner
            .Where(e => e.UID.ToString() == uid)
            .Include(e => e.ChuyenMuc)
            .FirstOrDefault();
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
        public IActionResult Select(int? ChuyenMucId)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.NewsBanner
                .Where(i => ChuyenMucId == null || i.ChuyenMucId == ChuyenMucId)
                .Include(e => e.ChuyenMuc)
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
        public IActionResult Add([FromBody] NewsBanner model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
                    model.UID = Guid.NewGuid();
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
        public IActionResult Update([FromBody] NewsBanner model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsBanner.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.ChuyenMucId = model.ChuyenMucId;
                        result.TieuDe = model.TieuDe;
                        result.HinhAnh = model.HinhAnh;
                        result.Link = model.Link;
                        result.NguoiTaoId = user.UserId;
                        result.NgayTao = DateTime.Now;

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
        public IActionResult Active([FromBody] NewsBanner model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsBanner.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        if (result.NguoiDuyetId == null)
                        {
                            result.NguoiDuyetId = user.UserId;
                            result.NgayDuyet = DateTime.Now;
                        }
                        else
                        {
                            result.NguoiDuyetId = null;
                            result.NgayDuyet = null;
                        }
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
                var result = _context.NewsBanner.SingleOrDefault(e => e.Id == id);
                if (result != null)
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
