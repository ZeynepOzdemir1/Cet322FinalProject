using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cet322EticaretUygulaması.Data;
using Cet322EticaretUygulaması.Models;

namespace Cet322EticaretUygulaması.Controllers
{
    public class PlantImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlantImage
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlantImages.ToListAsync());
        }

        // GET: PlantImage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantImageModel = await _context.PlantImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantImageModel == null)
            {
                return NotFound();
            }

            return View(plantImageModel);
        }

        // GET: PlantImage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlantImage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,FileName,IsDefaultImage")] PlantImageModel plantImageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantImageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plantImageModel);
        }

        // GET: PlantImage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantImageModel = await _context.PlantImages.FindAsync(id);
            if (plantImageModel == null)
            {
                return NotFound();
            }
            return View(plantImageModel);
        }

        // POST: PlantImage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,FileName,IsDefaultImage")] PlantImageModel plantImageModel)
        {
            if (id != plantImageModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantImageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantImageModelExists(plantImageModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plantImageModel);
        }

        // GET: PlantImage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantImageModel = await _context.PlantImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantImageModel == null)
            {
                return NotFound();
            }

            return View(plantImageModel);
        }

        // POST: PlantImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantImageModel = await _context.PlantImages.FindAsync(id);
            _context.PlantImages.Remove(plantImageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantImageModelExists(int id)
        {
            return _context.PlantImages.Any(e => e.Id == id);
        }
    }
}
