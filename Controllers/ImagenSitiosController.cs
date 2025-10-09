using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using guia_turistico.Data;
using guia_turistico.Models;

namespace guia_turistico.Controllers
{
    public class ImagenSitiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagenSitiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImagenSitios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImagenesSitio.Include(i => i.SitioTuristico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImagenSitios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagenSitio = await _context.ImagenesSitio
                .Include(i => i.SitioTuristico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagenSitio == null)
            {
                return NotFound();
            }

            return View(imagenSitio);
        }

        // GET: ImagenSitios/Create
        public IActionResult Create()
        {
            ViewData["SitioTuristicoId"] = new SelectList(_context.SitiosTuristicos, "Id", "Nombre");
            return View();
        }

        // POST: ImagenSitios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ImagenSitios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<string> Urls, int SitioTuristicoId)
        {
            if (Urls != null && Urls.Any(url => !string.IsNullOrWhiteSpace(url)))
            {
                // Filtrar URLs vacías
                var urlsValidas = Urls.Where(url => !string.IsNullOrWhiteSpace(url)).ToList();

                foreach (var url in urlsValidas)
                {
                    var imagenSitio = new ImagenSitio
                    {
                        Url = url.Trim(),
                        SitioTuristicoId = SitioTuristicoId
                    };
                    _context.Add(imagenSitio);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SitioTuristicoId"] = new SelectList(_context.SitiosTuristicos, "Id", "Nombre", SitioTuristicoId);
            ModelState.AddModelError("", "Debe ingresar al menos una URL válida.");
            return View();
        }

        // GET: ImagenSitios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagenSitio = await _context.ImagenesSitio.FindAsync(id);
            if (imagenSitio == null)
            {
                return NotFound();
            }
            ViewData["SitioTuristicoId"] = new SelectList(_context.SitiosTuristicos, "Id", "Nombre", imagenSitio.SitioTuristicoId);
            return View(imagenSitio);
        }

        // POST: ImagenSitios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,SitioTuristicoId")] ImagenSitio imagenSitio)
        {
            if (id != imagenSitio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagenSitio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagenSitioExists(imagenSitio.Id))
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
            ViewData["SitioTuristicoId"] = new SelectList(_context.SitiosTuristicos, "Id", "Nombre", imagenSitio.SitioTuristicoId);
            return View(imagenSitio);
        }

        // GET: ImagenSitios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagenSitio = await _context.ImagenesSitio
                .Include(i => i.SitioTuristico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagenSitio == null)
            {
                return NotFound();
            }

            return View(imagenSitio);
        }

        // POST: ImagenSitios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagenSitio = await _context.ImagenesSitio.FindAsync(id);
            if (imagenSitio != null)
            {
                _context.ImagenesSitio.Remove(imagenSitio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagenSitioExists(int id)
        {
            return _context.ImagenesSitio.Any(e => e.Id == id);
        }
    }
}
