﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prestamoHerramientas.Models;

namespace prestamoHerramientas.Controllers
{
    public class EquiposController : Controller
    {
        private readonly PrestamosContext _context;

        public EquiposController(PrestamosContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index(string buscar)
        {
            var prestamosContext = from Equipo in _context.Equipos select Equipo;

            prestamosContext = _context.Equipos.Include(e => e.IdMarcaNavigation);

            if (!String.IsNullOrEmpty(buscar))
            {
                prestamosContext = prestamosContext.Where(x => x.NombreEquipo!.Contains(buscar));
            }
            return View(await prestamosContext.ToListAsync());
        }


        public IActionResult crearExcel()
        {
            var Equipos = _context.Equipos;
            if (Equipos != null)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Equipos");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = "IdEquipo";
                    worksheet.Cell(currentRow, 2).Value = "NombreEquipo";
                    worksheet.Cell(currentRow, 3).Value = "IdMarca";
                    worksheet.Cell(currentRow, 4).Value = "NumeroSerie";
                    worksheet.Cell(currentRow, 5).Value = "Descripcion";

                    foreach (var item in Equipos)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.IdEquipo;
                        worksheet.Cell(currentRow, 2).Value = item.NombreEquipo;
                        worksheet.Cell(currentRow, 3).Value = item.IdMarca;
                        worksheet.Cell(currentRow, 4).Value = item.NumeroSerie;
                        worksheet.Cell(currentRow, 5).Value = item.Descripcion;
                    }

                    using (var strem = new MemoryStream())
                    {
                        workbook.SaveAs(strem);
                        var content = strem.ToArray();
                        return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Equipos.xlsx"
                                );
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.IdEquipo == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEquipo,NombreEquipo,IdMarca,NumeroSerie,Descripcion,Estado")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca", equipo.IdMarca);
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca", equipo.IdMarca);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEquipo,NombreEquipo,IdMarca,NumeroSerie,Descripcion,Estado")] Equipo equipo)
        {
            if (id != equipo.IdEquipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.IdEquipo))
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
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "NombreMarca", equipo.IdMarca);
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.IdEquipo == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equipos == null)
            {
                return Problem("Entity set 'PrestamosContext.Equipos'  is null.");
            }
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            return (_context.Equipos?.Any(e => e.IdEquipo == id)).GetValueOrDefault();
        }
    }
}
