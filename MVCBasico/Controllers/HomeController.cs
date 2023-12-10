using Microsoft.AspNetCore.Mvc;
using MVCBasico.Models;
using System.Diagnostics;
using System.Collections.Generic;
using MVCBasico.Context;
using Microsoft.EntityFrameworkCore;

namespace MVCBasico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecitalDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, RecitalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtener recitales reales de la base de datos con propiedades relacionadas cargadas
            var recitales = _context.Recital
                .Include(r => r.Banda)
                .Include(r => r.Establecimiento)
                .ToList();

            // Pasa la lista de recitales a la vista
            return View(recitales);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
