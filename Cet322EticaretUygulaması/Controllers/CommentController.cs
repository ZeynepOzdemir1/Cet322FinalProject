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
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.Plant).Include(c => c.PlantStoreUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.Comments
                .Include(c => c.Plant)
                .Include(c => c.PlantStoreUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            return View(commentModel);
        }

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description");
            ViewData["PlantStoreUserId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Detail,CreatedDate,PlantId,Rating,PlantStoreUserId")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", commentModel.PlantId);
            ViewData["PlantStoreUserId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", commentModel.PlantStoreUserId);
            return View(commentModel);
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", commentModel.PlantId);
            ViewData["PlantStoreUserId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", commentModel.PlantStoreUserId);
            return View(commentModel);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Detail,CreatedDate,PlantId,Rating,PlantStoreUserId")] CommentModel commentModel)
        {
            if (id != commentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentModelExists(commentModel.Id))
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
            ViewData["PlantId"] = new SelectList(_context.Plants, "Id", "Description", commentModel.PlantId);
            ViewData["PlantStoreUserId"] = new SelectList(_context.Set<CustomerModel>(), "Id", "Id", commentModel.PlantStoreUserId);
            return View(commentModel);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.Comments
                .Include(c => c.Plant)
                .Include(c => c.PlantStoreUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            return View(commentModel);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentModelExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
