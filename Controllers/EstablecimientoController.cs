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
    public class EstablecimientoController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public EstablecimientoController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Establecimiento
        public async Task<IActionResult> Index()
        {
              return _context.Establecimiento != null ? 
                          View(await _context.Establecimiento.ToListAsync()) :
                          Problem("Entity set 'RecitalDatabaseContext.Establecimiento'  is null.");
        }

        // GET: Establecimiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Establecimiento == null)
            {
                return NotFound();
            }

            var establecimiento = await _context.Establecimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (establecimiento == null)
            {
                return NotFound();
            }

            return View(establecimiento);
        }

        // GET: Establecimiento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Establecimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,capacidad,tipo,mailContacto")] Establecimiento establecimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(establecimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(establecimiento);
        }

        // GET: Establecimiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Establecimiento == null)
            {
                return NotFound();
            }

            var establecimiento = await _context.Establecimiento.FindAsync(id);
            if (establecimiento == null)
            {
                return NotFound();
            }
            return View(establecimiento);
        }

        // POST: Establecimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,capacidad,tipo,mailContacto")] Establecimiento establecimiento)
        {
            if (id != establecimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(establecimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstablecimientoExists(establecimiento.Id))
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
            return View(establecimiento);
        }

        // GET: Establecimiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Establecimiento == null)
            {
                return NotFound();
            }

            var establecimiento = await _context.Establecimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (establecimiento == null)
            {
                return NotFound();
            }

            return View(establecimiento);
        }

        // POST: Establecimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Establecimiento == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.Establecimiento'  is null.");
            }
            var establecimiento = await _context.Establecimiento.FindAsync(id);
            if (establecimiento != null)
            {
                _context.Establecimiento.Remove(establecimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstablecimientoExists(int id)
        {
          return (_context.Establecimiento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
