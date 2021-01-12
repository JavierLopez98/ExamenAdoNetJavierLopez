using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAdoNetJavierLopez.Models
{
    public class Libro
    {
        public int IdLibro { set; get; }
        public String Titulo { set; get; }
        public String Autor { get; set; }
        public String Sinopsis { set; get; }
        public String Imagen { set; get; }
        public int IdGenero { set; get; }
    }
}
