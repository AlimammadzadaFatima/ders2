using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yeniIl.Models;

namespace yeniIl.Areas.Manage.Controllers
{
        [Area("manage")]
        public class SettingController : Controller
        {
            private readonly DataContext _context;

            public SettingController(DataContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                return View(_context.settings.ToList());
            }

            public IActionResult Edit(int id)
            {
                Setting setting = _context.settings.FirstOrDefault(x => x.Id == id);
                return View(setting);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Setting setting)
            {
                var existSetting = _context.settings.FirstOrDefault(x => x.Id == setting.Id);
                if (existSetting == null)
                {
                    return NotFound();
                }
                

                existSetting.Value = setting.Value;
                _context.SaveChanges();
                return RedirectToAction("index");
            }

        }
    }

