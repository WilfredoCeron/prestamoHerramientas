﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prestamoHerramientas.Models;

namespace prestamoHerramientas.Controllers
{
    public class MarcasController : Controller
    {
        private readonly PrestamosContext _context;

        public MarcasController(PrestamosContext context)
        {
            _context = context;
        }

        // GET: Marcas
        public async Task<IActionResult> Index(string buscar)
        {
            var prestamosContext = from Marca in _context.Marcas select Marca;

            prestamosContext = _context.Marcas.Include(m => m.IdTipoHerramientaNavigation);

            if (!String.IsNullOrEmpty(buscar))
            {
                prestamosContext = prestamosContext.Where(x => x.NombreMarca!.Contains(buscar));
            }
            return View(await prestamosContext.ToListAsync());
        }


        public IActionResult crearExcel()
        {
            var Marcas = _context.Marcas;
            if (Marcas != null)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Marcas");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = "idMarca";
                    worksheet.Cell(currentRow, 2).Value = "nombreMarca";
                    worksheet.Cell(currentRow, 3).Value = "descripcion";
                    worksheet.Cell(currentRow, 4).Value = "tipoHerramienta";
                    worksheet.Cell(currentRow, 5).Value = "excatitud";

                    foreach (var item in Marcas)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.IdMarca;
                        worksheet.Cell(currentRow, 2).Value = item.NombreMarca;
                        worksheet.Cell(currentRow, 3).Value = item.Descripcion;
                        worksheet.Cell(currentRow, 4).Value = item.IdTipoHerramienta;
                        worksheet.Cell(currentRow, 5).Value = item.Exactitud;
                    }

                    using (var strem = new MemoryStream())
                    {
                        workbook.SaveAs(strem);
                        var content = strem.ToArray();
                        return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Marcas.xlsx"
                                );
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .Include(m => m.IdTipoHerramientaNavigation)
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoHerramienta"] = new SelectList(_context.TipoHerramientas, "IdTipoHerramienta", "TipoHerramienta1");
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMarca,NombreMarca,Descripcion,IdTipoHerramienta,Exactitud")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoHerramienta"] = new SelectList(_context.TipoHerramientas, "IdTipoHerramienta", "TipoHerramienta1", marca.IdTipoHerramienta);
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            ViewData["IdTipoHerramienta"] = new SelectList(_context.TipoHerramientas, "IdTipoHerramienta", "TipoHerramienta1", marca.IdTipoHerramienta);
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMarca,NombreMarca,Descripcion,IdTipoHerramienta,Exactitud")] Marca marca)
        {
            if (id != marca.IdMarca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.IdMarca))
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
            ViewData["IdTipoHerramienta"] = new SelectList(_context.TipoHerramientas, "IdTipoHerramienta", "TipoHerramienta1", marca.IdTipoHerramienta);
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .Include(m => m.IdTipoHerramientaNavigation)
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marcas == null)
            {
                return Problem("Entity set 'PrestamosContext.Marcas'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
            return (_context.Marcas?.Any(e => e.IdMarca == id)).GetValueOrDefault();
        }
    }
}
