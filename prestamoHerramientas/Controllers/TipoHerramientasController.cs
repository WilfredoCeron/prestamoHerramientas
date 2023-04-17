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
    public class TipoHerramientasController : Controller
    {
        private readonly PrestamosContext _context;

        public TipoHerramientasController(PrestamosContext context)
        {
            _context = context;
        }

        // GET: TipoHerramientas
        public async Task<IActionResult> Index()
        {
              return _context.TipoHerramientas != null ? 
                          View(await _context.TipoHerramientas.ToListAsync()) :
                          Problem("Entity set 'PrestamosContext.TipoHerramientas'  is null.");
        }

        // GET: TipoHerramientas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoHerramientas == null)
            {
                return NotFound();
            }

            var tipoHerramienta = await _context.TipoHerramientas
                .FirstOrDefaultAsync(m => m.IdTipoHerramienta == id);
            if (tipoHerramienta == null)
            {
                return NotFound();
            }

            return View(tipoHerramienta);
        }

        // GET: TipoHerramientas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoHerramientas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoHerramienta,TipoHerramienta1")] TipoHerramienta tipoHerramienta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoHerramienta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoHerramienta);
        }

        // GET: TipoHerramientas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoHerramientas == null)
            {
                return NotFound();
            }

            var tipoHerramienta = await _context.TipoHerramientas.FindAsync(id);
            if (tipoHerramienta == null)
            {
                return NotFound();
            }
            return View(tipoHerramienta);
        }

        // POST: TipoHerramientas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoHerramienta,TipoHerramienta1")] TipoHerramienta tipoHerramienta)
        {
            if (id != tipoHerramienta.IdTipoHerramienta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoHerramienta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoHerramientaExists(tipoHerramienta.IdTipoHerramienta))
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
            return View(tipoHerramienta);
        }

        // GET: TipoHerramientas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoHerramientas == null)
            {
                return NotFound();
            }

            var tipoHerramienta = await _context.TipoHerramientas
                .FirstOrDefaultAsync(m => m.IdTipoHerramienta == id);
            if (tipoHerramienta == null)
            {
                return NotFound();
            }

            return View(tipoHerramienta);
        }

        // POST: TipoHerramientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoHerramientas == null)
            {
                return Problem("Entity set 'PrestamosContext.TipoHerramientas'  is null.");
            }
            var tipoHerramienta = await _context.TipoHerramientas.FindAsync(id);
            if (tipoHerramienta != null)
            {
                _context.TipoHerramientas.Remove(tipoHerramienta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoHerramientaExists(int id)
        {
          return (_context.TipoHerramientas?.Any(e => e.IdTipoHerramienta == id)).GetValueOrDefault();
        }
    }
}
