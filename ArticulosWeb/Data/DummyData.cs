using ArticulosWeb.Data;
using ArticulosWeb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArticulosWeb.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context, InventarioDBWContext testdbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            //////////////////////   Identity AspNetUser   /////////////////////
            ///
            context.Database.EnsureCreated();

            string adminRole = "Administrador";
            string adminDesc = "Rol de Administrador, con acceso a las configuraciones del Sistema.";
            //---------------------------
            string usuarioRole = "Usuario";
            string usuarioDesc = "Rol de Usuaro, con acceeso a la funcionalidad del Sistema.";
            //---------------------------
            string trabajarRole = "Trabajar";
            string trabajarDesc = "Rol de Trabajar, con acceeso a la funcionalidad del Sistema.";
            //---------------------------
            string contratarRole = "Contratar";
            string contratarDesc = "Rol de Contratar, con acceeso a la funcionalidad del Sistema.";
            //---------------------------
            string vipRole = "VIP";
            string vipDesc = "Rol de VIP, con acceeso a la funcionalidad del Sistema.";
            //---------------------------
            string adminCredential = "admin@root.net";
            string userCredential = "user@root.net";

            string password = "P@ssword1";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(adminRole, adminDesc, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(usuarioRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(usuarioRole, usuarioDesc, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(trabajarRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(trabajarRole, trabajarDesc, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(contratarRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(contratarRole, contratarDesc, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(vipRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(vipRole, vipDesc, DateTime.Now));
            }

            if (await userManager.FindByNameAsync(adminCredential) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminCredential,
                    Email = adminCredential,
                    FirstName = "Admin",
                    LastName = "istrador",
                    Street = "Av. Sucre",
                    City = "Cochabamba",
                    Province = "Cercado",
                    PostalCode = "na",
                    Country = "Bolivia",
                    PhoneNumber = "1234567"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

            if (await userManager.FindByNameAsync(userCredential) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userCredential,
                    Email = userCredential,
                    FirstName = "Usuario",
                    LastName = "Comino",
                    Street = "Av. Sucre",
                    City = "Cochabamba",
                    Province = "Cercado",
                    PostalCode = "na",
                    Country = "Bolivia",
                    PhoneNumber = "1234567"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, usuarioRole);
                }
            }
        }
    }
}
