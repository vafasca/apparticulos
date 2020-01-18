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
    public class AppInventarioController : Controller
    {
        private readonly InventarioDBWContext _context;

        public AppInventarioController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: AppInventario
        public async Task<IActionResult> Index()
        {
            var inventarioDBWContext = _context.AppInventario.Include(a => a.AnuncioIdAnuncioNavigation);
            return View(await inventarioDBWContext.ToListAsync());
        }

        // GET: AppInventario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appInventario = await _context.AppInventario
                .Include(a => a.AnuncioIdAnuncioNavigation)
                .FirstOrDefaultAsync(m => m.InventarioId == id);
            if (appInventario == null)
            {
                return NotFound();
            }

            return View(appInventario);
        }

        // GET: AppInventario/Create
        public IActionResult Create()
        {
            ViewData["AnuncioIdAnuncio"] = new SelectList(_context.Anuncio, "AnuncioId", "AnuncioId");
            return View();
        }

        // POST: AppInventario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventarioId,Gestion,AnuncioIdAnuncio")] AppInventario appInventario)
        {
            if (ModelState.IsValid)
            {
                string a = "";
                int aux = 1;
                var bb = _context.Auto
                       .OrderByDescending(p => p.AutoId)
                       .FirstOrDefault();
                if (bb == null)
                {
                    appInventario.InventarioId = aux;
                }
                else
                {
                    aux = bb.AutoId;
                    appInventario.InventarioId = aux + 1;
                }
                _context.Add(appInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnuncioIdAnuncio"] = new SelectList(_context.Anuncio, "AnuncioId", "AnuncioId", appInventario.AnuncioIdAnuncio);
            return View(appInventario);
        }

        // GET: AppInventario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appInventario = await _context.AppInventario.FindAsync(id);
            if (appInventario == null)
            {
                return NotFound();
            }
            ViewData["AnuncioIdAnuncio"] = new SelectList(_context.Anuncio, "AnuncioId", "AnuncioId", appInventario.AnuncioIdAnuncio);
            return View(appInventario);
        }

        // POST: AppInventario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventarioId,Gestion,AnuncioIdAnuncio")] AppInventario appInventario)
        {
            if (id != appInventario.InventarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppInventarioExists(appInventario.InventarioId))
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
            ViewData["AnuncioIdAnuncio"] = new SelectList(_context.Anuncio, "AnuncioId", "AnuncioId", appInventario.AnuncioIdAnuncio);
            return View(appInventario);
        }

        // GET: AppInventario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appInventario = await _context.AppInventario
                .Include(a => a.AnuncioIdAnuncioNavigation)
                .FirstOrDefaultAsync(m => m.InventarioId == id);
            if (appInventario == null)
            {
                return NotFound();
            }

            return View(appInventario);
        }

        // POST: AppInventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appInventario = await _context.AppInventario.FindAsync(id);
            _context.AppInventario.Remove(appInventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppInventarioExists(int id)
        {
            return _context.AppInventario.Any(e => e.InventarioId == id);
        }
    }
}
