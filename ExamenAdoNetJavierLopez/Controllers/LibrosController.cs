
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamenAdoNetJavierLopez.Data;
using ExamenAdoNetJavierLopez.Models;

namespace ExamenAdoNetJavierLopez.Controllers
{
    public class LibrosController : Controller
    {
        LibrosContext context;

        public LibrosController()
        {
            this.context = new LibrosContext();
        }

        public IActionResult Libros(int idgenero)
        {
            if (idgenero == 0) { 
            List<Libro> libros = this.context.GetLibros();
            return View(libros);
        }
            else
            {
                List<Libro> libros = this.context.getLibrosporGenero(idgenero);
                return View(libros);
            }
        }
        public IActionResult DetalleLibro(int idlibro)
        {
            Libro lib=this.context.getLibro(idlibro);
            ViewBag.Genero = this.context.getGeneroLibro(idlibro);
            return View(lib);
        }

        public IActionResult Modificalibro(int idlibro)
        {
            Libro lib = this.context.getLibro(idlibro);
            List<Genero> generos = this.context.getGeneros();
            ViewBag.Titulo = lib.Titulo;
            ViewBag.Autor = lib.Autor;
            ViewBag.Sinopsis = lib.Sinopsis;
            ViewBag.Imagen = lib.Imagen;
            ViewBag.Genero = lib.IdGenero;
            ViewBag.Gnombre = this.context.GetGeneroId(lib.IdGenero).Gnombre;
            return View(generos);
        }
        [HttpPost]
        public IActionResult Modificalibro(Libro lib)
        {
            this.context.CambiaLibro(lib.IdLibro, lib.Titulo, lib.Autor, lib.Sinopsis, lib.Imagen, lib.IdGenero);
            return RedirectToAction("Libros");

        }
        public IActionResult CreaLibro()
        {
            List<Genero> generos = this.context.getGeneros();
            return View(generos);
        }
        [HttpPost]
        public IActionResult CreaLibro(Libro lib)
        {
            this.context.createLibro(lib.Titulo,lib.Autor,lib.Sinopsis,lib.Imagen,lib.IdGenero);
            return RedirectToAction("Libros");
        }
    }
}
