using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yeniIl.Models;

namespace yeniIl.Areas.Manage.Controllers
{
        [Area("manage")]
        public class PortfolioController : Controller
        {
            private readonly DataContext _context;
            private readonly IWebHostEnvironment _env;

            public PortfolioController(DataContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }
            public IActionResult Index()
            {
                return View(_context.portfolios.ToList());
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Portfolio portfolio)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (portfolio.FileImage == null)
                {
                    ModelState.AddModelError("FileImage", "FileImage is Empty!!");
                    return View();
                }
                if (portfolio.FileImage.ContentType != "image/jpeg" && portfolio.FileImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("FileImage", "ContentType only Jpeg|Png");
                    return View();
                }
                if (portfolio.FileImage.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile max size is 2MB");
                    return View();
                }

                portfolio.Image = FileManager.Save(_env.WebRootPath, "uploads/portfolios", portfolio.FileImage);
                _context.portfolios.Add(portfolio);
                _context.SaveChanges();
                return RedirectToAction("index");
            }

            public IActionResult Edit(int id)
            {
                Portfolio portfolio = _context.portfolios.FirstOrDefault(x => x.Id == id);
                if (portfolio == null)
                {
                    return NotFound();
                }
                return View(portfolio);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Portfolio portfolio)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                Portfolio existPortfolio = _context.portfolios.FirstOrDefault(x => x.Id == portfolio.Id);
                if (existPortfolio == null)
                {
                    return NotFound();
                }
                if (portfolio.FileImage != null)
                {
                    if (portfolio.FileImage.ContentType != "image/jpeg" && portfolio.FileImage.ContentType != "image/png")
                    {
                        ModelState.AddModelError("FileImage", "ContentType is only jpeg|png");
                        return View();
                    }
                    if (portfolio.FileImage.Length > 2097152)
                    {
                        ModelState.AddModelError("FileImage", "ImageFile max size is 2MB");
                        return View();
                    }

                    FileManager.Delete(_env.WebRootPath, "uploads/portfolios", existPortfolio.Image);
                    existPortfolio.Image = FileManager.Save(_env.WebRootPath, "uploads/portfolios", portfolio.FileImage);
                }
                existPortfolio.Title = portfolio.Title;
                existPortfolio.Text = portfolio.Text;
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            public IActionResult Delete(int id)
            {
                Portfolio portfolio = _context.portfolios.FirstOrDefault(x => x.Id == id);
                if (portfolio == null)
                {
                    return NotFound();
                }
                FileManager.Delete(_env.WebRootPath, "uploads/portfolios", portfolio.Image);
                _context.Remove(portfolio);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
