using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticulosWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ArticulosWeb.Areas.Identity.Pages.Account;

namespace ArticulosWeb.Controllers
{
    public class AspNetUserRolesController : Controller
    {
        private readonly InventarioDBWContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public AspNetUserRolesController(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, InventarioDBWContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        // GET: AspNetUserRoles
        public async Task<IActionResult> Index()
        {
            var inventarioDBWContext = _context.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);
            return View(await inventarioDBWContext.ToListAsync());
        }

        // GET: AspNetUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: AspNetUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        public async Task<IActionResult> Check(AspNetUserRoles aspNetUserRoles, string button)
        {
            var hh = "~/";
            //####################
            var usr = User.Identity.Name;
            var progu = from gesty in _context.AspNetUsers
                        where gesty.UserName == usr
                        select gesty.Id;
            string convertp = progu.FirstOrDefault();
            //####################
            var contratar = from gesty in _context.AspNetRoles
                            where gesty.Name == button
                            select gesty.Id;
            string IdContratar = contratar.FirstOrDefault();

            aspNetUserRoles.UserId = convertp;
            aspNetUserRoles.RoleId = IdContratar;
            _context.Add(aspNetUserRoles);
            await _context.SaveChangesAsync();
            return Redirect("~/");
        }

        // GET: AspNetUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (id != aspNetUserRoles.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserRolesExists(aspNetUserRoles.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);
            _context.AspNetUserRoles.Remove(aspNetUserRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserRolesExists(string id)
        {
            return _context.AspNetUserRoles.Any(e => e.UserId == id);
        }
    }
}
