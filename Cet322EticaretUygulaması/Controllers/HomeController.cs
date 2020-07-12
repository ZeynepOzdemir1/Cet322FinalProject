using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cet322EticaretUygulaması.Models;
using Cet322EticaretUygulaması.ViewModel;
using Cet322EticaretUygulaması.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cet322EticaretUygulaması.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Search()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel searchModel)
        {
            var title = searchModel.SearchText;
            var plants = _context.Plants.Where(p=>true).AsQueryable();

            if (!String.IsNullOrWhiteSpace(searchModel.SearchText))
            {
                if(searchModel.SearchInDescription)
                {
                plants = plants.Where(p => p.PlantName.Contains(searchModel.SearchText) || p.Description.Contains(searchModel.SearchText));
                }
                else
                {
                    plants = plants.Where(p => p.PlantName.Contains(searchModel.SearchText));
                }

            }
            if (searchModel.CategoryId.HasValue)
            {
                plants = plants.Where(p => p.CategoryId == searchModel.CategoryId.Value);
            }
            var result = await plants.ToListAsync();
            searchModel.Results=await plants.ToListAsync();
            return View(searchModel);
        }





    }
}
