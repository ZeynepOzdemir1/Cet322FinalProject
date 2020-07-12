using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cet322EticaretUygulaması.Data;
using Cet322EticaretUygulaması.Models;
using Cet322EticaretUygulaması.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Cet322EticaretUygulaması.Controllers
{
    public class PlantModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public PlantModelsController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: PlantModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plants.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> UplodeImage(ImageUploadViewModel uploadModel)
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, "UserImages");
            string fileName = Guid.NewGuid().ToString() + "_" + uploadModel.ImageFile.FileName;

            string fullPath = Path.Combine(directory, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await uploadModel.ImageFile.CopyToAsync(fileStream);
            }

            PlantImageModel plantImage = new PlantImageModel();
            plantImage.ProductId = uploadModel.PlantId;
            plantImage.FileName = fileName;

            _context.PlantImages.Add(plantImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageImage), new { id = uploadModel.PlantId });



        }

        public async Task<IActionResult> ManageImage(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var plant = await _context.Plants.Include(p => p.PlantImages)
            .FirstOrDefaultAsync(p => p.Id == id);
            if (plant==null)
            {
             return NotFound();
            }
            return View(plant);
        }


        // GET: PlantModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantModel = await _context.Plants
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantModel == null)
            {
                return NotFound();
            }

            return View(plantModel);
        }

        // GET: PlantModels/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: PlantModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlantName,Description,Price,CreationDateTime,CategoryId,CreatedDate")] PlantModel plantModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", plantModel.CategoryId);
            return View(plantModel);
        }

        // GET: PlantModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantModel = await _context.Plants.FindAsync(id);
            if (plantModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", plantModel.CategoryId);
            return View(plantModel);
        }

        // POST: PlantModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlantName,Description,Price,CreationDateTime,CategoryId,CreatedDate")] PlantModel plantModel)
        {
            if (id != plantModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantModelExists(plantModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", plantModel.CategoryId);
            return View(plantModel);
        }

        // GET: PlantModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantModel = await _context.Plants
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantModel == null)
            {
                return NotFound();
            }

            return View(plantModel);
        }

        // POST: PlantModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantModel = await _context.Plants.FindAsync(id);
            _context.Plants.Remove(plantModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantModelExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
