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
    public class BandaController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public BandaController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Banda
        public async Task<IActionResult> Index()
        {
              return _context.Banda != null ? 
                          View(await _context.Banda.ToListAsync()) :
                          Problem("Entity set 'RecitalDatabaseContext.Banda'  is null.");
        }

        // GET: Banda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Banda == null)
            {
                return NotFound();
            }

            var banda = await _context.Banda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }

            return View(banda);
        }

        // GET: Banda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,cantidadMusicos,genero,telefonoContacto")] Banda banda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(banda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banda);
        }

        // GET: Banda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Banda == null)
            {
                return NotFound();
            }

            var banda = await _context.Banda.FindAsync(id);
            if (banda == null)
            {
                return NotFound();
            }
            return View(banda);
        }

        // POST: Banda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,cantidadMusicos,genero,telefonoContacto")] Banda banda)
        {
            if (id != banda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandaExists(banda.Id))
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
            return View(banda);
        }

        // GET: Banda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Banda == null)
            {
                return NotFound();
            }

            var banda = await _context.Banda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }

            return View(banda);
        }

        // POST: Banda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Banda == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.Banda'  is null.");
            }
            var banda = await _context.Banda.FindAsync(id);
            if (banda != null)
            {
                _context.Banda.Remove(banda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandaExists(int id)
        {
          return (_context.Banda?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
