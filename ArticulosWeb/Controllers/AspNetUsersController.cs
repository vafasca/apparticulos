using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticulosWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace ArticulosWeb.Controllers
{
    [Authorize(Roles = "Administrador,Usuario")]
    public class AspNetUsersController : Controller
    {
        private readonly InventarioDBWContext _context;

        public AspNetUsersController(InventarioDBWContext context)
        {
            _context = context;
        }

        // GET: AspNetUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetUsers.ToListAsync());
        }

        // GET: AspNetUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,IdPer,FirstName,LastName,Street,City,Province,PostalCode,Country")] AspNetUsers aspNetUsers)
        {

            if (ModelState.IsValid)
            {
                //
                
                string password = aspNetUsers.PasswordHash;

                //// generate a 128-bit salt using a secure PRNG
                //byte[] salt = new byte[128 / 8];
                //using (var rng = RandomNumberGenerator.Create())
                //{
                //    rng.GetBytes(salt);
                //}
                //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

                //// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                //    password: password,
                //    salt: salt,
                //    prf: KeyDerivationPrf.HMACSHA1,
                //    iterationCount: 10000,
                //    numBytesRequested: 256 / 8));
                //Console.WriteLine($"Hashed: {hashed}");
                //
                byte[] salt = new byte[128 / 8];
                using (var generator = RandomNumberGenerator.Create())

                    generator.GetBytes(salt);
                string rpt2 = Convert.ToBase64String(salt);
                //
                var valueBytes = KeyDerivation.Pbkdf2(
                             password: password,
                             salt: Encoding.UTF8.GetBytes(rpt2),
                             prf: KeyDerivationPrf.HMACSHA512,
                             iterationCount: 10000,
                             numBytesRequested: 256 / 8);

                var rpt = Convert.ToBase64String(valueBytes);
                //
                aspNetUsers.PasswordHash = rpt;
                //

                _context.Add(aspNetUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,IdPer,FirstName,LastName,Street,City,Province,PostalCode,Country")] AspNetUsers aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
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
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(aspNetUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
