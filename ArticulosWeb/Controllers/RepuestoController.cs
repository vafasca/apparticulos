using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticulosWeb.Models;

namespace ArticulosWeb.Controllers
{
    public class RepuestoController : Controller
    {
        private readonly InventarioDBWContext _context;

        public RepuestoController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: Repuesto
        public async Task<IActionResult> Index()
        {
            var inventarioDBWContext = _context.Repuesto.Include(r => r.AutoIdAutoNavigation).Include(r => r.CategoriaIdCategoriaNavigation).Include(r => r.InventarioIdAppInventarioNavigation);
            //
                //        var repuesto = await _context.Repuesto
                //.Include(r => r.AutoIdAutoNavigation)
                //.Include(r => r.CategoriaIdCategoriaNavigation)
                //.Include(r => r.InventarioIdAppInventarioNavigation)
                //.FirstOrDefaultAsync(m => m.RepuestoId == id);
            //
            //ViewBag.Nom = 
            return View(await inventarioDBWContext.ToListAsync());
        }

        // GET: Repuesto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .Include(r => r.AutoIdAutoNavigation)
                .Include(r => r.CategoriaIdCategoriaNavigation)
                .Include(r => r.InventarioIdAppInventarioNavigation)
                .FirstOrDefaultAsync(m => m.RepuestoId == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // GET: Repuesto/Create
        public IActionResult Create()
        {
            ViewData["AutoIdAuto"] = new SelectList(_context.Auto, "AutoId", "Modelo", "", "Marca");
            ViewData["CategoriaIdCategoria"] = new SelectList(_context.Categoria, "CategoriaId", "NombreCategoria");
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId");
            return View();
        }

        // POST: Repuesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepuestoId,Nombre,Precio,Descripcion,Disponible,AutoIdAuto,CategoriaIdCategoria,InventarioIdAppInventario")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                string a = "";
                int aux = 1;
                var bb = _context.Repuesto
                       .OrderByDescending(p => p.RepuestoId)
                       .FirstOrDefault();
                if (bb == null)
                {
                    repuesto.RepuestoId = aux;
                }
                else
                {
                    aux = bb.RepuestoId;
                    repuesto.RepuestoId = aux + 1;
                }
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoIdAuto"] = new SelectList(_context.Auto, "AutoId", "AutoId", repuesto.AutoIdAuto);
            ViewData["CategoriaIdCategoria"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", repuesto.CategoriaIdCategoria);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", repuesto.InventarioIdAppInventario);
            return View(repuesto);
        }

        // GET: Repuesto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            ViewData["AutoIdAuto"] = new SelectList(_context.Auto, "AutoId", "Modelo", repuesto.AutoIdAuto);
            ViewData["CategoriaIdCategoria"] = new SelectList(_context.Categoria, "CategoriaId", "NombreCategoria", repuesto.CategoriaIdCategoria);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", repuesto.InventarioIdAppInventario);
            return View(repuesto);
        }

        // POST: Repuesto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepuestoId,Nombre,Precio,Descripcion,Disponible,AutoIdAuto,CategoriaIdCategoria,InventarioIdAppInventario")] Repuesto repuesto)
        {
            if (id != repuesto.RepuestoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoExists(repuesto.RepuestoId))
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
            ViewData["AutoIdAuto"] = new SelectList(_context.Auto, "AutoId", "AutoId", repuesto.AutoIdAuto);
            ViewData["CategoriaIdCategoria"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", repuesto.CategoriaIdCategoria);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", repuesto.InventarioIdAppInventario);
            return View(repuesto);
        }

        // GET: Repuesto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .Include(r => r.AutoIdAutoNavigation)
                .Include(r => r.CategoriaIdCategoriaNavigation)
                .Include(r => r.InventarioIdAppInventarioNavigation)
                .FirstOrDefaultAsync(m => m.RepuestoId == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // POST: Repuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repuesto = await _context.Repuesto.FindAsync(id);
            _context.Repuesto.Remove(repuesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuesto.Any(e => e.RepuestoId == id);
        }
    }
}
