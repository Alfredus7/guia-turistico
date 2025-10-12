using guia_turistico.Data;
using guia_turistico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace guia_turistico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Cargar tipos
            var tipos = _context.Tipos.ToList();

            // Calcular top 3 sitios mejor puntuados con su imagen principal
            var topSitios = _context.SitiosTuristicos
                .Include(s => s.Imagenes) // incluye relación con ImagenSitio
                .Select(s => new
                {
                    Sitio = s,
                    ImagenUrl = s.Imagenes.FirstOrDefault().Url ?? "/images/default-sitio.jpg",
                    Promedio = _context.Comentarios
                        .Where(c => c.SitioTuristicoId == s.Id)
                        .Average(c => (double?)c.Puntuacion) ?? 0
                })
                .OrderByDescending(x => x.Promedio)
                .Take(3)
                .ToList();

            ViewBag.TopSitios = topSitios;
            return View(tipos);
        }

        public IActionResult Mapa()
        {
            var sitios = _context.SitiosTuristicos.ToList();
            return View(sitios);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

