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
    public class PedidoController : Controller
    {
        private readonly InventarioDBWContext _context;

        public PedidoController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var inventarioDBWContext = _context.Pedido.Include(p => p.ClienteIdClienteNavigation).Include(p => p.InventarioIdAppInventarioNavigation).Include(p => p.RepuestoIdRepuestoNavigation);
            return View(await inventarioDBWContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.ClienteIdClienteNavigation)
                .Include(p => p.InventarioIdAppInventarioNavigation)
                .Include(p => p.RepuestoIdRepuestoNavigation)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            ViewData["ClienteIdCliente"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId");
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId");
            return View();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,NombrePedido,Cantidad,FechaReserva,RepuestoIdRepuesto,ClienteIdCliente,InventarioIdAppInventario")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                string a = "";
                int aux = 1;
                var bb = _context.Pedido
                       .OrderByDescending(p => p.PedidoId)
                       .FirstOrDefault();
                if (bb == null)
                {
                    pedido.PedidoId = aux;
                }
                else
                {
                    aux = bb.PedidoId;
                    pedido.PedidoId = aux + 1;
                }
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteIdCliente"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", pedido.ClienteIdCliente);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", pedido.InventarioIdAppInventario);
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId", pedido.RepuestoIdRepuesto);
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteIdCliente"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", pedido.ClienteIdCliente);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", pedido.InventarioIdAppInventario);
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId", pedido.RepuestoIdRepuesto);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,NombrePedido,Cantidad,FechaReserva,RepuestoIdRepuesto,ClienteIdCliente,InventarioIdAppInventario")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
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
            ViewData["ClienteIdCliente"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", pedido.ClienteIdCliente);
            ViewData["InventarioIdAppInventario"] = new SelectList(_context.AppInventario, "InventarioId", "InventarioId", pedido.InventarioIdAppInventario);
            ViewData["RepuestoIdRepuesto"] = new SelectList(_context.Repuesto, "RepuestoId", "RepuestoId", pedido.RepuestoIdRepuesto);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.ClienteIdClienteNavigation)
                .Include(p => p.InventarioIdAppInventarioNavigation)
                .Include(p => p.RepuestoIdRepuestoNavigation)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.PedidoId == id);
        }
    }
}
