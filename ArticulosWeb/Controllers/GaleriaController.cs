using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticulosWeb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ArticulosWeb.Controllers
{
    public class GaleriaController : Controller
    {
        private readonly InventarioDBWContext _context;

        public GaleriaController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: Galeria
        public async Task<IActionResult> Index()
        {
            var inventarioDBWContext = _context.Galeria.Include(g => g.RepuestoIdRepuestoNavigation);
            return View(await inventarioDBWContext.ToListAsync());
        }

        // GET: Galeria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeria = await _context.Galeria
                .Include(g => g.RepuestoIdRepuestoNavigation)
                .FirstOrDefaultAsync(m => m.GaleriaId == id);

            string imageBase64Data = Convert.ToBase64String(galeria.Foto);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            ViewBag.ImageData = imageDataURL;
            //
            var a = galeria.Foto;
            if (galeria == null)
            {
                return NotFound();
            }

            return View(galeria);
        }

        // GET: Galeria/Create
        public IActionResult Create()
        {
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "Nombre");
            return View();
        }

        // POST: Galeria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GaleriaId,NombreFoto,DescripcionFoto,RepuestoIdRepuesto")] Galeria galeria, List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                //ViewData["dato1"] = galeria.NombreFoto;//kick
                foreach(var item in file)
                {
                    if(item.Length>0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            galeria.Foto = stream.ToArray();
                            //stream.Close();  
                            int aux = 1;
                            var bb = _context.Galeria
                                   .OrderByDescending(p => p.GaleriaId)
                                   .FirstOrDefault();
                            if (bb == null)
                            {
                                galeria.GaleriaId = aux;
                            }
                            else
                            {
                                aux = bb.GaleriaId;
                                galeria.GaleriaId = aux + 1;
                            }
                            _context.Add(galeria);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId", galeria.RepuestoIdRepuesto);
            return View(galeria);
        }

        // GET: Galeria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeria = await _context.Galeria.FindAsync(id);
            if (galeria == null)
            {
                return NotFound();
            }
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "Nombre", galeria.RepuestoIdRepuesto);
            return View(galeria);
        }

        // POST: Galeria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GaleriaId,NombreFoto,DescripcionFoto,RepuestoIdRepuesto")] Galeria galeria, List<IFormFile> file)
        {
            if (id != galeria.GaleriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //
                    foreach (var item in file)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                galeria.Foto = stream.ToArray();
                                //stream.Close();
                            }
                        }
                    }
                    //
                    _context.Update(galeria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriaExists(galeria.GaleriaId))
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
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId", galeria.RepuestoIdRepuesto);
            return View(galeria);
        }

        // GET: Galeria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeria = await _context.Galeria
                .Include(g => g.RepuestoIdRepuestoNavigation)
                .FirstOrDefaultAsync(m => m.GaleriaId == id);

            if (galeria == null)
            {
                return NotFound();
            }

            return View(galeria);
        }

        // POST: Galeria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galeria = await _context.Galeria.FindAsync(id);
            _context.Galeria.Remove(galeria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriaExists(int id)
        {
            return _context.Galeria.Any(e => e.GaleriaId == id);
        }
    }
}
