using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Components
{
    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {

        private ApplicationDbContext _context;

        public FooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var truycap = _context.CauHinh.Where(p => p.Id == 20).Select(p => p.GiaTri).FirstOrDefault();

            var tuyendungc = _context.HoSoTuyenDung.Where(p => p.XacThuc == true).ToList();
            ViewBag.TuyenDung = tuyendungc;

            var timviecc = _context.HoSoTimViec.Where(p => p.XacThuc == true).ToList();
            ViewBag.TimViecc = timviecc;

            var thanhvienc = _context.User.Where(p => p.RoleId == 3 || p.RoleId == 4).ToList();
            ViewBag.ThanhVienCountc = thanhvienc.Count;

            var footer = _context.NewsBanTin.Where(p => p.ChuyenMucId == 36 && p.TieuDe == "Footer Text").FirstOrDefault();
            var footer1 = _context.NewsBanTin.Where(p => p.ChuyenMucId == 36 && p.TieuDe == "Footer Link").FirstOrDefault();

            var footerstring = footer.NoiDung;
            var footerstring1 = footer1.NoiDung;
            var timvieccount = timviecc.Count;
            var tuyendungcount = tuyendungc.Count;
            var thanhviencount = thanhvienc.Count;
            List<string> categories = new List<string>() {
               truycap , timvieccount.ToString(), tuyendungcount.ToString(), thanhviencount.ToString(), footerstring, footerstring1
            };
            return View("Index", categories);
        }
    }
}
