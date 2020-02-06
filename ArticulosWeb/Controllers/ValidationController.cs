using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArticulosWeb.Models;

namespace ArticulosWeb.Controllers
{
    public class ValidationController : Controller
    {
        
        Cliente cliente = new Cliente();
        private readonly InventarioDBWContext _context;
        
        public ValidationController(InventarioDBWContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            //Roles del Sistema
            ViewBag.admin = "Administrador";
            ViewBag.trabajar = "trabajar";
            ViewBag.contratar = "contratar";
            ViewBag.finanzas = "finanzas";
            ViewBag.vip = "vip";
            return View();
        }

        public ActionResult Verificar(string usuario, string pass)
        {
            
            //
            var bb = _context.Cliente
            .OrderByDescending(p => p.ClienteId)
            .FirstOrDefault();
            
            //
            string var1 = usuario;
            string var2 = pass;
            //var query = from Cliente in _context.Cliente
            //            where Cliente.Nombre == usuario
            //            select Cliente.Apellido;
            //string respq = query.FirstOrDefault();
            if (bb.Nombre!=usuario)
            {
                TempData["ErrorDate"] = "Usuario No encontrado";
                return Redirect("/Validation/Login/");
            }
            else
            {
                if (bb.Apellido==pass)
                {
                    if(bb.Celular==1)
                    {
                        ViewBag.rev = "Administrador";
                        return Redirect("/Home/Privacy/");
                    }
                    if (bb.Celular == 2)
                    {
                        TempData["rol"] = "trabajar";
                        return Redirect("/Home/Privacy/");
                    }
                    if (bb.Celular == 3)
                    {
                        TempData["rol"] = "contratar";
                        return Redirect("/Home/Privacy/");
                    }
                    if (bb.Celular == 4)
                    {
                        TempData["rol"] = "finanzas";
                        return Redirect("/Home/Privacy/");
                    }
                    if (bb.Celular == 5)
                    {
                        TempData["rol"] = "vip";
                        return Redirect("/Home/Privacy/");
                    }
                }
                TempData["ErrorDate"] = "Usuario no encontrado";
                return Redirect("/Validation/Login/");
            }
        }
    }
}