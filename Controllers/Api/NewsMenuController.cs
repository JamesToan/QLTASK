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
    public class NewsMenuController : ControllerBase
    {
        private ApplicationDbContext _context;


        public NewsMenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Select(int? ChuyenMucId)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                var result = _context.NewsMenu
                .Where(i => ChuyenMucId == null || i.ChuyenMucId == ChuyenMucId)
                .Include(e => e.ChuyenMuc)
                .OrderBy(e => e.ThuTu)
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
        public IActionResult Add([FromBody] NewsMenu model)
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
        public IActionResult Update([FromBody] NewsMenu model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsMenu.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.ChuyenMucId = model.ChuyenMucId;
                        result.TieuDe = model.TieuDe;
                        result.Icon = model.Icon;
                        result.Link = model.Link;
                        result.ThuTu = model.ThuTu;
                        result.HieuLuc = model.HieuLuc;

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
        public IActionResult Active([FromBody] NewsMenu model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.NewsMenu.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        if (result.HieuLuc == null || result.HieuLuc == false)
                        {
                            result.HieuLuc = true;
                        }
                        else
                        {
                            result.HieuLuc = false;
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
                var result = _context.NewsMenu.SingleOrDefault(e => e.Id == id);
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
