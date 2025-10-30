using guia_turistico.Data;
using guia_turistico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace guia_turistico.Controllers
{
    public class SitioTuristicoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SitioTuristicoesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SitioTuristicoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SitiosTuristicos.Include(s => s.Tipo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SitioTuristicoes/Create
        public IActionResult Create()
        {
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "Nombre");
            return View();
        }

        // POST: SitioTuristicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,NombreIngles,NombrePortugues,Descripcion,DescripcionIngles,DescripcionPortugues,Direccion,Latitud,Longitud,TipoId")] SitioTuristico sitioTuristico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sitioTuristico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "Nombre", sitioTuristico.TipoId);
            return View(sitioTuristico);
        }
       

        // GET: SitioTuristicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitioTuristico = await _context.SitiosTuristicos.FindAsync(id);
            if (sitioTuristico == null)
            {
                return NotFound();
            }
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "Nombre", sitioTuristico.TipoId);
            return View(sitioTuristico);
        }

        // POST: SitioTuristicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,NombreIngles,NombrePortugues,Descripcion,DescripcionIngles,DescripcionPortugues,Direccion,Latitud,Longitud,TipoId")] SitioTuristico sitioTuristico)
        {
            if (id != sitioTuristico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitioTuristico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SitioTuristicoExists(sitioTuristico.Id))
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
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "Nombre", sitioTuristico.TipoId);
            return View(sitioTuristico);
        }

        // GET: SitioTuristicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitioTuristico = await _context.SitiosTuristicos
                .Include(s => s.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitioTuristico == null)
            {
                return NotFound();
            }

            return View(sitioTuristico);
        }

        // POST: SitioTuristicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sitioTuristico = await _context.SitiosTuristicos.FindAsync(id);
            if (sitioTuristico != null)
            {
                _context.SitiosTuristicos.Remove(sitioTuristico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SitioTuristicoExists(int id)
        {
            return _context.SitiosTuristicos.Any(e => e.Id == id);
        }

        // ---------------------------
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarComentario(int sitioId, int puntuacion, string texto)
        {
            if (puntuacion < 1 || puntuacion > 5 || string.IsNullOrWhiteSpace(texto))
            {
                TempData["Error"] = "Debe completar todos los campos y seleccionar una puntuación válida.";
                return RedirectToAction("Details", new { id = sitioId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Buscar si el usuario ya comentó este sitio
            var comentarioExistente = await _context.Comentarios
                .FirstOrDefaultAsync(c => c.SitioTuristicoId == sitioId && c.UsuarioId == user.Id);

            if (comentarioExistente != null)
            {
                // 🛠 Actualiza comentario existente
                comentarioExistente.Texto = texto;
                comentarioExistente.Puntuacion = puntuacion;
                comentarioExistente.Fecha = DateTime.Now;
                _context.Comentarios.Update(comentarioExistente);
                TempData["Mensaje"] = "Tu comentario ha sido actualizado.";
            }
            else
            {
                // 🆕 Crea nuevo comentario
                var comentario = new Comentario
                {
                    SitioTuristicoId = sitioId,
                    Texto = texto,
                    Puntuacion = puntuacion,
                    UsuarioId = user.Id,
                    Fecha = DateTime.Now
                };
                _context.Comentarios.Add(comentario);
                TempData["Mensaje"] = "Tu comentario ha sido agregado.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = sitioId });
        }
        public async Task<IActionResult> PorTipo(int tipoId)
        {
            var tipo = await _context.Tipos.FirstOrDefaultAsync(t => t.TipoId == tipoId);

            if (tipo == null)
                return NotFound();

            var sitios = await _context.SitiosTuristicos
                .Where(s => s.TipoId == tipoId)
                .Include(s => s.Tipo)
                .Include(s => s.Imagenes)
                .ToListAsync();

            ViewBag.TipoNombre = tipo.Nombre;
            ViewBag.TipoNombreIngles = tipo.NombreIngles;
            ViewBag.TipoNombrePortugues = tipo.NombrePortugues;

            return View(sitios);
        }
        // GET: SitioTuristicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitioTuristico = await _context.SitiosTuristicos
                .Include(s => s.Tipo)
                .Include(s => s.Imagenes)
                .Include(s => s.Comentarios)
                    .ThenInclude(c => c.Usuario) // 👈 Carga el usuario del comentario
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sitioTuristico == null)
            {
                return NotFound();
            }

            // 💡 Recalcular puntuación promedio (por seguridad)
            if (sitioTuristico.Comentarios.Any())
            {
                sitioTuristico.Comentarios = sitioTuristico.Comentarios
                    .OrderByDescending(c => c.Fecha)
                    .ToList();
            }

            return View(sitioTuristico);
        }
    }
}
