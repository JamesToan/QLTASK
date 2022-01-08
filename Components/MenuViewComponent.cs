using coreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Components
{
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public MenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var menu = _context.NewsMenu.Where(p => p.ChuyenMucId == 0 && p.MenuChaId == null).ToList();
            var menucon = _context.NewsMenu.Where(p => p.ChuyenMucId == 0 && p.MenuChaId != null).ToList();
            List<List<Models.Entities.NewsMenu>> menus = new List<List<Models.Entities.NewsMenu>>
            {
                menu, menucon
            };
            return View("Index", menus);
        }

    }
}
