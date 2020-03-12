using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArticulosWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticulosWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventarioDBWContext _context;

        public HomeController(InventarioDBWContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var CantidadAnuncio = _context.Anuncio.Count();
            ViewBag.Cant = CantidadAnuncio;
            if (CantidadAnuncio%2 == 0)
            {
                ViewBag.par = true;
            }
            ViewBag.impar = true;
            var pru = _context.Anuncio;
            if (CantidadAnuncio<=2)
            {
                ViewBag.Datopo = 0;
            }
            else
            {
                ViewBag.Datopo = (CantidadAnuncio-1) / 2;
            }
            int sendPar = 0;//1
            int sendImpar = 0;
            int contador = 0;//1
            int prueba = 0;
            foreach (var el in pru)
            {
                ViewBag.Prueba = prueba;
                if (contador % 2 != 0)
                {
                    ViewBag.esImpar = true;
                    string imageBase64Data = Convert.ToBase64String(el.FotoAnuncio);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);


                    ViewData["TituloImpar" + sendImpar] = el.Titulo;
                    ViewData["DescripcionImpar" + sendImpar] = el.Descripcion;
                    ViewData["anuncioFotoImpar" + sendImpar] = imageDataURL;
                    ViewBag.send = sendImpar;
                    sendImpar++;
                    prueba++;
                }
                else
                {
                    ViewBag.Espar = true;
                    string imageBase64Data = Convert.ToBase64String(el.FotoAnuncio);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                    ViewData["TituloPar" + sendPar] = el.Titulo;
                    ViewData["DescripcionPar" + sendPar] = el.Descripcion;
                    ViewData["anuncioFotoPar" + sendPar] = imageDataURL;
                    sendPar++;
                    prueba++;
                }
                contador++;
            }

            var pollo = "asd";
            ViewData[pollo] = "asd";
            //return View();
            return View(_context.Anuncio);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
