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
    public class CommentController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.Comment.SingleOrDefault();
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
        public IActionResult Select(int ycId)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
            {
                var result = _context.Comment.Where(p => p.YeuCauId == ycId).Include(e => e.User).Include(e => e.YeuCau).ToList();
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
        public IActionResult Add(string comments , int ycId)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var result = _context.Comment.Where(p => p.Comments == comments).FirstOrDefault();
                
                if (user != null && result == null)
                {
                    result = new Comment();
                    result.UserId = user.UserId;
                    result.Comments = comments;
                    result.YeuCauId = ycId;
                    _context.Add(result);
                    _context.SaveChanges();
                    return Ok(result.Id);
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
        public IActionResult Update(string comments, int id)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1)
                {
                    //var result = _context.Comment.SingleOrDefault(e => e.Id == model.Id && e.UserId == user.UserId);
                    var result = _context.Comment.Where(p =>p.Id == id).FirstOrDefault();
                    if (result != null) //update
                    {
                        result.Comments = comments;
                        
                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (user.RoleId == 2 || user.RoleId == 3)
                {
                    //var result = _context.Comment.SingleOrDefault(e => e.Id == model.Id && e.UserId == user.UserId);
                    var result = _context.Comment.Where(p => p.Id == id).FirstOrDefault();
                    
                    if (result != null && user.UserId == result.UserId) //update
                    {
                        result.Comments = comments;

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
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1)
                {
                    var result = _context.Comment.SingleOrDefault(e => e.Id == id);
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
                else if (user.RoleId == 2 || user.RoleId == 3) {
                    var result = _context.Comment.SingleOrDefault(e => e.Id == id);
                    if (result != null && result.UserId == user.UserId) //update
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
