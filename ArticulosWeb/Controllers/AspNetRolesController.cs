﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticulosWeb.Models;

namespace ArticulosWeb.Controllers
{
    public class AspNetRolesController : Controller
    {
        private readonly InventarioDBWContext _context;

        public AspNetRolesController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: AspNetRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetRoles.ToListAsync());
        }

        // GET: AspNetRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp,Description,CreatedDate")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.AspNetRoles.FindAsync(id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }
            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp,Description,CreatedDate")] AspNetRoles aspNetRoles)
        {
            if (id != aspNetRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetRolesExists(aspNetRoles.Id))
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
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetRoles = await _context.AspNetRoles.FindAsync(id);
            _context.AspNetRoles.Remove(aspNetRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }
    }
}
