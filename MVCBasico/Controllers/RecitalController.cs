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
    public class RecitalController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public RecitalController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Recital
        public async Task<IActionResult> Index()
        {
            var recitalDatabaseContext = _context.Recital.Include(r => r.Banda).Include(r => r.Establecimiento);
            return View(await recitalDatabaseContext.ToListAsync());
        }

        // GET: Recital/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recital == null)
            {
                return NotFound();
            }

            var recital = await _context.Recital
                .Include(r => r.Banda)
                .Include(r => r.Establecimiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recital == null)
            {
                return NotFound();
            }

            return View(recital);
        }

        // GET: Recital/Create
        public IActionResult Create()
        {
            ViewData["BandaId"] = new SelectList(_context.Banda, "Id", "Nombre");
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre");
            return View();
        }

        // POST: Recital/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Fecha,EntradasVendidas,EstaAgotado,PrecioEntrada,BandaId,EstablecimientoId")] Recital recital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandaId"] = new SelectList(_context.Banda, "Id", "Nombre", recital.BandaId);
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", recital.EstablecimientoId);
            return View(recital);
        }

        // GET: Recital/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recital == null)
            {
                return NotFound();
            }

            var recital = await _context.Recital.FindAsync(id);
            if (recital == null)
            {
                return NotFound();
            }
            ViewData["BandaId"] = new SelectList(_context.Banda, "Id", "Nombre", recital.BandaId);
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", recital.EstablecimientoId);
            return View(recital);
        }

        // POST: Recital/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Fecha,EntradasVendidas,EstaAgotado,PrecioEntrada,BandaId,EstablecimientoId")] Recital recital)
        {
            if (id != recital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecitalExists(recital.Id))
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
            ViewData["BandaId"] = new SelectList(_context.Banda, "Id", "Nombre", recital.BandaId);
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", recital.EstablecimientoId);
            return View(recital);
        }

        // GET: Recital/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recital == null)
            {
                return NotFound();
            }

            var recital = await _context.Recital
                .Include(r => r.Banda)
                .Include(r => r.Establecimiento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recital == null)
            {
                return NotFound();
            }

            return View(recital);
        }

        // POST: Recital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recital == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.Recital'  is null.");
            }
            var recital = await _context.Recital.FindAsync(id);
            if (recital != null)
            {
                _context.Recital.Remove(recital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecitalExists(int id)
        {
          return (_context.Recital?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
