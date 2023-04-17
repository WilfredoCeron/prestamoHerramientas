using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prestamoHerramientas.Models;

namespace prestamoHerramientas.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly PrestamosContext _context;

        public PrestamosController(PrestamosContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index(string buscar)
        {
            var prestamosContext = from Prestamo in _context.Prestamos select Prestamo;

            prestamosContext = _context.Prestamos.Include(p => p.IdMarcaNavigation).Include(p => p.IdModeloNavigation);

            if (!String.IsNullOrEmpty(buscar))
            {
                
                var marca = _context.Marcas.Where(m => m.NombreMarca.Equals(buscar)).FirstOrDefault();
                prestamosContext = prestamosContext.Where(x => x.IdMarca==marca.IdMarca);
                if( marca== null)
                {
                    var modelo = _context.Modelos.Where(m => m.Serie.Equals(buscar)).FirstOrDefault();
                    prestamosContext = prestamosContext.Where(x => x.IdModelo == modelo.IdModelo);
                    if(modelo == null)
                    {
                        prestamosContext = _context.Prestamos.Include(p => p.IdMarcaNavigation).Include(p => p.IdModeloNavigation);
                    }
                }  
            }
            
            return View(await prestamosContext.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdModeloNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca");
            ViewData["IdEquipo"] = new SelectList(_context.Equipos, "IdEquipo", "NombreEquipo");
            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrestamo,PreatadoA,IdMarca,IdModelo,FechaIncio,FechaFin,Estado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca", prestamo.IdMarca);
            ViewData["IdEquipo"] = new SelectList(_context.Modelos, "IdEquipo", "NombreEquipo", prestamo.IdModelo);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca", prestamo.IdMarca);
            ViewData["IdEquipo"] = new SelectList(_context.Modelos, "IdEquipo", "NombreEquipo", prestamo.IdModelo);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrestamo,PreatadoA,IdMarca,IdModelo,FechaIncio,FechaFin,Estado")] Prestamo prestamo)
        {
            if (id != prestamo.IdPrestamo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.IdPrestamo))
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
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "IdMarca", prestamo.IdMarca);
            ViewData["IdModelo"] = new SelectList(_context.Modelos, "IdModelo", "IdModelo", prestamo.IdModelo);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdMarcaNavigation)
                .Include(p => p.IdModeloNavigation)
                .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prestamos == null)
            {
                return Problem("Entity set 'PrestamosContext.Prestamos'  is null.");
            }
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
          return (_context.Prestamos?.Any(e => e.IdPrestamo == id)).GetValueOrDefault();
        }
    }
}
