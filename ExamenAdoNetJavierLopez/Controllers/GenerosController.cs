﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamenAdoNetJavierLopez.Data;
using ExamenAdoNetJavierLopez.Models;

namespace ExamenAdoNetJavierLopez.Controllers
{
    public class GenerosController : Controller
    {
        LibrosContext context;

        public GenerosController()
        {
            this.context = new LibrosContext();
        }

        public IActionResult Genero()
        {
            List<Genero> generos = this.context.getGeneros();
            return View(generos);
        }
        
        public IActionResult creaGenero()
        {
            return View();
        }
        [HttpPost]
        public IActionResult creaGenero(Genero gen)
        {
            this.context.CreaGenero(gen.Gnombre);
            return RedirectToAction("Genero");
        }
        public IActionResult eliminaGenero(int idgenero)
        {
            this.context.EliminaGenero(idgenero);
            return RedirectToAction("Genero");
        }
    }
}