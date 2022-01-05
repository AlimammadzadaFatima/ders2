using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using yeniIl.Models;
using yeniIl.ViewModels;

namespace yeniIl.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                portfolios=_context.portfolios.ToList(),
                settings=_context.settings.ToList()
            };
            return View(homeVM);
        }

       
    }
}
