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
                List<NhanSu> DMNhanSuCNTT;
               
                
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {


                    DMNhanSu = _context.NhanSu.Where(p => p.isActive == true).ToList();
                    DMNhanSuCNTT = _context.NhanSu.Where(p => p.isActive == true && p.Id != 7 & p.UnitId == 1).ToList();
                   
                    var DMQLDV = _context.QuanLyDichVu.ToList();
                    var DMTinhTrang = _context.Status.ToList();
                    var DMDiaBan = _context.Unit.ToList();
                    var DMTrangThai = _context.States.OrderByDescending(e => e.Id).ToList();
                    var DMDonVi = _context.DonViYeuCau.ToList();
                    var DMDichVu = _context.DichVu.Where(dm => dm.IsActive == true).Include(e => e.DonVi).OrderBy(e => e.Id).ToList();
                    var DMJira = _context.Jira.Where(dm => dm.IsActive == true).ToList();
                    var DMLYC = _context.LoaiYeuCau.ToList();
                    
                    if (DMNhanSu != null && DMTinhTrang != null && DMTrangThai != null && DMDichVu != null && DMJira != null && DMDonVi != null  && DMQLDV != null && DMLYC != null && DMDiaBan != null && DMNhanSuCNTT != null)
                    {
                        var result = new
                        {
                            DMNhanSu = DMNhanSu,
                            DMTinhTrang = DMTinhTrang,
                            DMTrangThai = DMTrangThai,
                            DMDichVu = DMDichVu,
                            DMJira = DMJira,
                            DMDonVi = DMDonVi,
                            DMQLDV = DMQLDV,
                            DMLYC = DMLYC,
                            DMDiaBan = DMDiaBan,
                            DMNhanSuCNTT = DMNhanSuCNTT
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
        public class UserViewModel
        {

            public int UserId { get; set; }

            public string FullName { get; set; }

        }
        public class NhanSuViewModel
        {
            public int NSID { get; set; }
            public string TenNhanSu { get; set; }
            public int UnitId { get; set; }
            public int UserId { get; set; }
            public int DichVuId { get; set; }
            public int AdminDichVuId { get; set; }
        }
    }

}
