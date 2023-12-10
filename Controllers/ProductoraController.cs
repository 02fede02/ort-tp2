using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class ProductoraController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public ProductoraController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Productora
        public async Task<IActionResult> Index()
        {
              return _context.Productora != null ? 
                          View(await _context.Productora.ToListAsync()) :
                          Problem("Entity set 'RecitalDatabaseContext.Productora'  is null.");
        }

        // GET: Productora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productora == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        // GET: Productora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,razonSocial,presupuesto")] Productora productora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productora);
        }

        // GET: Productora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productora == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora.FindAsync(id);
            if (productora == null)
            {
                return NotFound();
            }
            return View(productora);
        }

        // POST: Productora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,razonSocial,presupuesto")] Productora productora)
        {
            if (id != productora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoraExists(productora.Id))
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
            return View(productora);
        }

        // GET: Productora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productora == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        // POST: Productora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productora == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.Productora'  is null.");
            }
            var productora = await _context.Productora.FindAsync(id);
            if (productora != null)
            {
                _context.Productora.Remove(productora);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoraExists(int id)
        {
          return (_context.Productora?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
