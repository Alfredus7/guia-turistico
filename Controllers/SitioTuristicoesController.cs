using guia_turistico.Data;
using guia_turistico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace guia_turistico.Controllers
{
    public class SitioTuristicoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitioTuristicoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SitioTuristicoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SitiosTuristicos.Include(s => s.Tipo);
            return View(await applicationDbContext.ToListAsync());
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
                .Include(s => s.Imagenes) // ¡Importante incluir las imágenes!
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sitioTuristico == null)
            {
                return NotFound();
            }

            return View(sitioTuristico);
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
        public async Task<IActionResult> Create(SitioTuristico sitio)
        {
            if (ModelState.IsValid)
            {
                // Asegurar que las coordenadas se lean con punto decimal
                sitio.Latitud = Convert.ToDouble(sitio.Latitud.ToString(), CultureInfo.InvariantCulture);
                sitio.Longitud = Convert.ToDouble(sitio.Longitud.ToString(), CultureInfo.InvariantCulture);

                _context.Add(sitio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "Nombre", sitio.TipoId);
            return View(sitio);
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

        public IActionResult Mapa()
        {
            var sitios = _context.SitiosTuristicos.ToList();
            return View(sitios);
        }



        // POST: SitioTuristicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,DescripcionIngles,DescripcionPortugues,Direccion,Latitud,Longitud,TipoId")] SitioTuristico sitioTuristico)
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
    }
}
