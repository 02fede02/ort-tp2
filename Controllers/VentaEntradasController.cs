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
    public class VentaEntradasController : Controller
    {
        private readonly RecitalDatabaseContext _context;

        public VentaEntradasController(RecitalDatabaseContext context)
        {
            _context = context;
        }

        // GET: VentaEntradas
        public async Task<IActionResult> Index()
        {
            var recitalDatabaseContext = _context.VentaEntradas.Include(v => v.Recital).Include(v => v.Usuario);
            return View(await recitalDatabaseContext.ToListAsync());
        }

        // GET: VentaEntradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VentaEntradas == null)
            {
                return NotFound();
            }

            var ventaEntradas = await _context.VentaEntradas
                .Include(v => v.Recital)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventaEntradas == null)
            {
                return NotFound();
            }

            return View(ventaEntradas);
        }

        // GET: VentaEntradas/Create
        public IActionResult Create()
        {
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido");
            return View();
        }


        // POST: VentaEntradas/Create
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create([Bind("Id,CantidadEntradas,RecitalId,UsuarioId,PrecioTotal")] VentaEntradas ventaEntradas)
          {
              if (ModelState.IsValid)
              {
                  var recital = await _context.Recital.FindAsync(ventaEntradas.RecitalId);

                  if (recital != null)
                  {
                      var establecimiento = await _context.Establecimiento.FindAsync(recital.EstablecimientoId);

                      // Verificar si hay suficientes entradas disponibles
                      if (establecimiento != null && recital.EntradasVendidas + ventaEntradas.CantidadEntradas <= establecimiento.capacidad)
                      {
                          // Actualizar las entradas vendidas y la capacidad
                          recital.EntradasVendidas += ventaEntradas.CantidadEntradas;

                          if (recital.EntradasVendidas == establecimiento.capacidad)
                          {
                              recital.EstaAgotado = true;
                          }

                          _context.Update(recital);

                          // Calcular el PrecioTotal antes de agregar al contexto
                          ventaEntradas.PrecioTotal = ventaEntradas.CantidadEntradas * recital.PrecioEntrada;

                          // Agregar la venta de entradas al contexto
                          _context.Add(ventaEntradas);

                          await _context.SaveChangesAsync();

                          return RedirectToAction(nameof(Index));
                      }
                      else
                      {
                          ModelState.AddModelError("CantidadEntradas", "No hay suficientes entradas disponibles.");
                      }
                  }
              }                
              ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "Nombre", ventaEntradas.RecitalId);
              ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", ventaEntradas.UsuarioId);

              return View(ventaEntradas);
          }
       

        // GET: VentaEntradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VentaEntradas == null)
            {
                return NotFound();
            }

            var ventaEntradas = await _context.VentaEntradas.FindAsync(id);
            if (ventaEntradas == null)
            {
                return NotFound();
            }
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "Nombre", ventaEntradas.RecitalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", ventaEntradas.UsuarioId);
            return View(ventaEntradas);
        }

        // POST: VentaEntradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CantidadEntradas,RecitalId,UsuarioId,PrecioTotal")] VentaEntradas ventaEntradas)
        {
            if (id != ventaEntradas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventaEntradas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaEntradasExists(ventaEntradas.Id))
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
            ViewData["RecitalId"] = new SelectList(_context.Recital, "Id", "Nombre", ventaEntradas.RecitalId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Apellido", ventaEntradas.UsuarioId);
            return View(ventaEntradas);
        }

        // GET: VentaEntradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VentaEntradas == null)
            {
                return NotFound();
            }

            var ventaEntradas = await _context.VentaEntradas
                .Include(v => v.Recital)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventaEntradas == null)
            {
                return NotFound();
            }

            return View(ventaEntradas);
        }

        // POST: VentaEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VentaEntradas == null)
            {
                return Problem("Entity set 'RecitalDatabaseContext.VentaEntradas'  is null.");
            }
            var ventaEntradas = await _context.VentaEntradas.FindAsync(id);
            if (ventaEntradas != null)
            {
                var recital = await _context.Recital.FindAsync(ventaEntradas.RecitalId);

                if (recital != null)
                {
                    
                    recital.EntradasVendidas -= ventaEntradas.CantidadEntradas;

                    
                    recital.EstaAgotado = false;

                    _context.Update(recital);
                }
                _context.VentaEntradas.Remove(ventaEntradas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaEntradasExists(int id)
        {
          return (_context.VentaEntradas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
