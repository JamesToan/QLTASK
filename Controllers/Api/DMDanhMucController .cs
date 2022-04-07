using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using coreWeb.Models;

using coreWeb.Models.Entities;
using System.Collections.Generic;

namespace coreWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DMDanhMucController : ControllerBase
    {
        private ApplicationDbContext _context;


        public DMDanhMucController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult YeuCau()
        {
            try
            {
                var user = new UserClaim(HttpContext);
                var userinfo = _context.User.Where(p => p.Id == user.UserId).FirstOrDefault();
                List<NhanSu> DMNhanSu;
                List<User> DMUser;
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {


                    DMNhanSu = _context.NhanSu.Include(e => e.Unit).ToList();

                    if (user.RoleId == 1)
                    {
                        DMUser = _context.User.ToList();
                    }
                    else
                    {
                        DMUser = _context.User.Where(p => p.UnitId == userinfo.UnitId).ToList();
                    }
                    var DMQLDV = _context.QuanLyDichVu.ToList();
                    var DMTinhTrang = _context.Status.ToList();
                    var DMDiaBan    = _context.Unit.ToList(); 
                    var DMTrangThai = _context.States.OrderByDescending(e=>e.Id).ToList();
                    var DMDonVi     = _context.DonViYeuCau.ToList();
                    var DMDichVu    = _context.DichVu.Where(dm => dm.IsActive == true).Include(e=>e.DonVi).OrderBy(e =>e.Id).ToList();
                    var DMJira      = _context.Jira.Where(dm => dm.IsActive == true).ToList();
                    var DMLYC       = _context.LoaiYeuCau.ToList();
                    if (DMNhanSu != null && DMTinhTrang != null && DMTrangThai != null && DMDichVu != null && DMJira != null && DMDonVi != null && DMUser != null && DMQLDV != null && DMLYC != null && DMDiaBan != null)
                    {
                        var result = new
                        {
                            DMNhanSu = DMNhanSu,
                            DMTinhTrang = DMTinhTrang,
                            DMTrangThai = DMTrangThai,
                            DMDichVu = DMDichVu,
                            DMJira = DMJira,
                            DMDonVi = DMDonVi,
                            DMUser  = DMUser,
                            DMQLDV = DMQLDV,
                            DMLYC  = DMLYC,
                            DMDiaBan = DMDiaBan,
                        };
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        
    }


}
