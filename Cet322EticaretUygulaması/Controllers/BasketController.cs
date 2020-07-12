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
    public class BasketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BasketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Basket
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Baskets.Include(b => b.Customer).Include(b => b.Plant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Basket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketResultModel = await _context.Baskets
                .Include(b => b.Customer)
                .Include(b => b.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketResultModel == null)
            {
                return NotFound();
            }

            return View(basketResultModel);
        }

        // GET: Basket/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id");
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description");
            return View();
        }

        // POST: Basket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,PlantId,CustomerName,PlantName,PlantPrice,Quantity")] BasketResultModel basketResultModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basketResultModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", basketResultModel.CustomerId);
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", basketResultModel.PlantId);
            return View(basketResultModel);
        }

        // GET: Basket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketResultModel = await _context.Baskets.FindAsync(id);
            if (basketResultModel == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", basketResultModel.CustomerId);
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", basketResultModel.PlantId);
            return View(basketResultModel);
        }

        // POST: Basket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,PlantId,CustomerName,PlantName,PlantPrice,Quantity")] BasketResultModel basketResultModel)
        {
            if (id != basketResultModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basketResultModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketResultModelExists(basketResultModel.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", basketResultModel.CustomerId);
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", basketResultModel.PlantId);
            return View(basketResultModel);
        }

        // GET: Basket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketResultModel = await _context.Baskets
                .Include(b => b.Customer)
                .Include(b => b.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketResultModel == null)
            {
                return NotFound();
            }

            return View(basketResultModel);
        }

        // POST: Basket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basketResultModel = await _context.Baskets.FindAsync(id);
            _context.Baskets.Remove(basketResultModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketResultModelExists(int id)
        {
            return _context.Baskets.Any(e => e.Id == id);
        }
    }
}
