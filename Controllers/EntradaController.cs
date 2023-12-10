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
    public class EntradaController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public EntradaController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Entrada
        public async Task<IActionResult> Index()
        {
            var recitalDatabaseContext = _context.Entrada.Include(e => e.Establecimiento).Include(e => e.Recital).Include(e => e.Usuario);
            return View(await recitalDatabaseContext.ToListAsync());
        }

        // GET: Entrada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entrada
                .Include(e => e.Establecimiento)
                .Include(e => e.Recital)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entrada/Create
        public IActionResult Create()
        {
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre");
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido");
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Precio,Cantidad,EstablecimientoId,RecitalId,UsuarioId")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", entrada.EstablecimientoId);
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "nombre", entrada.RecitalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", entrada.UsuarioId);
            return View(entrada);
        }

        // GET: Entrada/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entrada.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", entrada.EstablecimientoId);
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "nombre", entrada.RecitalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", entrada.UsuarioId);
            return View(entrada);
        }

        // POST: Entrada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,Cantidad,EstablecimientoId,RecitalId,UsuarioId")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.Id))
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
            ViewData["EstablecimientoId"] = new SelectList(_context.Establecimiento, "Id", "nombre", entrada.EstablecimientoId);
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "nombre", entrada.RecitalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", entrada.UsuarioId);
            return View(entrada);
        }

        // GET: Entrada/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entrada
                .Include(e => e.Establecimiento)
                .Include(e => e.Recital)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entrada == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.Entrada'  is null.");
            }
            var entrada = await _context.Entrada.FindAsync(id);
            if (entrada != null)
            {
                _context.Entrada.Remove(entrada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaExists(int id)
        {
          return (_context.Entrada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
