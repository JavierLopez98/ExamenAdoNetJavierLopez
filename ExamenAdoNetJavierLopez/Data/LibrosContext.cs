using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ExamenAdoNetJavierLopez.Models;

#region Procesos
//alter procedure nuevolibro (@Titulo nvarchar(255),@Autor nvarchar(255),@Sinopsis nvarchar(MAX),@Imagen nvarchar(255),@Genero int)
//as
//    insert into Libros values((Select max(IdLibro)+1 from libros),@Titulo,@Autor,@Sinopsis,@Imagen,@Genero)
//go

//create procedure cambialibro(@idlibro int, @Titulo nvarchar(255),@Autor nvarchar(255),@Sinopsis nvarchar(MAX),@Imagen nvarchar(255),@Genero int)
//as
//    update libros set Titulo=@Titulo, Autor = @Autor, Sinopsis = @Sinopsis, Imagen = @Imagen, IdGenero = @Genero where IdLibro=@idlibro
//go

//create procedure elminarGenero(@idgenero int)
//as

//update libros set IdGenero=0 where IdGenero=@idgenero

//delete from generos where IdGenero=@idgenero

//go
#endregion

namespace ExamenAdoNetJavierLopez.Data
{

    public class LibrosContext
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataAdapter adlibro;
        SqlDataAdapter adgenero;
        DataTable tablalibro;
        DataTable tablagenero;

        public LibrosContext()
        {
            String cadena = "Data Source=DESKTOP-4T2BHUM;Initial Catalog=LibrosAdo;Persist Security Info=True;User ID=SA;Password=MCSD2020";
            this.tablagenero = new DataTable();
            this.tablalibro = new DataTable();
            this.adlibro = new SqlDataAdapter("select * from libros order by idLibro", cadena);
            this.adlibro.Fill(this.tablalibro);
            this.adgenero = new SqlDataAdapter("select * from generos order by idGenero", cadena);
            this.adgenero.Fill(this.tablagenero);
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Libro> GetLibros()
        {
            List<Libro> libros = new List<Libro>();
            var consulta = from datos in this.tablalibro.AsEnumerable() select datos;
            foreach (var row in consulta)
            {
                Libro lib = new Libro();
                lib.IdLibro = row.Field<int>("IdLibro");
                lib.Titulo = row.Field<String>("Titulo");
                lib.Autor = row.Field<String>("Autor");
                lib.Sinopsis = row.Field<String>("Sinopsis");
                lib.Imagen = row.Field<String>("Imagen");
                lib.IdGenero = row.Field<int>("IdGenero");
                libros.Add(lib);
            }
            return libros;
        }

        public Libro getLibro(int id)
        {
            Libro lib = new Libro();
            var consulta = from datos in this.tablalibro.AsEnumerable() where datos.Field<int>("IdLibro") == id select datos;
            var row = consulta.First();
            lib.IdLibro = row.Field<int>("IdLibro");
            lib.Titulo = row.Field<String>("Titulo");
            lib.Autor = row.Field<String>("Autor");
            lib.Sinopsis = row.Field<String>("Sinopsis");
            lib.Imagen = row.Field<String>("Imagen");
            lib.IdGenero = row.Field<int>("IdGenero");
            return lib;
        }

        public List<Genero> getGeneros()
        {
            List<Genero> generos = new List<Genero>();
            var consulta = from datos in tablagenero.AsEnumerable() select datos;
            foreach (var row in consulta)
            {
                Genero gen = new Genero();
                gen.IdGenero = row.Field<int>("IdGenero");
                gen.Gnombre = row.Field<String>("Genero");
                generos.Add(gen);
            }
            return generos;
        }
        public String getGeneroLibro(int idlibro)
        {
            
            var consultalibro = from datos in tablalibro.AsEnumerable() where datos.Field<int>("idLibro")==idlibro select datos;
            var rowlibro = consultalibro.First();
            int idgenero = rowlibro.Field<int>("idGenero");
            var consultagenero = from datos in tablagenero.AsEnumerable() where datos.Field<int>("IdGenero") == idgenero select datos;
            var rowgenero = consultagenero.First();
            String genero = rowgenero.Field<String>("Genero");
            return genero;

        }
        public List<Libro> getLibrosporGenero(int idgenero)
        {
            List<Libro> libros = new List<Libro>();
            var consulta = from datos in tablalibro.AsEnumerable() where datos.Field<int>("idGenero") == idgenero select datos;
            foreach (var row in consulta)
            {
                Libro lib = new Libro();
                lib.IdLibro = row.Field<int>("IdLibro");
                lib.Titulo = row.Field<String>("Titulo");
                lib.Autor = row.Field<String>("Autor");
                lib.Sinopsis = row.Field<String>("Sinopsis");
                lib.Imagen = row.Field<String>("Imagen");
                lib.IdGenero = row.Field<int>("IdGenero");
                libros.Add(lib);
            }
            return libros;
        }

        public void createLibro(String Titulo, String Autor, String Sinopsis, String Imagen, int IdGenero)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "nuevolibro";
            this.com.Parameters.AddWithValue("@Titulo", Titulo);
            this.com.Parameters.AddWithValue("@Autor", Autor);
            this.com.Parameters.AddWithValue("@Sinopsis", Sinopsis);
            this.com.Parameters.AddWithValue("@Imagen", Imagen);
            this.com.Parameters.AddWithValue("@Genero", IdGenero);
            this.cn.Open();
            int insertados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Genero GetGeneroId(int id)
        {

            var consulta = from datos in tablagenero.AsEnumerable() where datos.Field<int>("idGenero") == id select datos;
            var row = consulta.First();
            Genero gen = new Genero();
            gen.IdGenero = row.Field<int>("IdGenero");
            gen.Gnombre = row.Field<String>("Genero");
            return gen;
        }

        public void CambiaLibro(int idLibro, String Titulo, String Autor, String Sinopsis, String Imagen, int IdGenero)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "cambialibro";
            this.com.Parameters.AddWithValue("@idlibro", idLibro);
            this.com.Parameters.AddWithValue("@Titulo", Titulo);
            this.com.Parameters.AddWithValue("@Autor", Autor);
            this.com.Parameters.AddWithValue("@Sinopsis", Sinopsis);
            this.com.Parameters.AddWithValue("@Imagen", Imagen);
            this.com.Parameters.AddWithValue("@Genero", IdGenero);
            this.cn.Open();
            int modificados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void CreaGenero(string Genero)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "nuevoGenero";
            this.com.Parameters.AddWithValue("@Genero", Genero);
            this.cn.Open();
            int insertados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public void EliminaGenero(int idgenero)
        {
            this.com.CommandText = "eliminarGenero";
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.Parameters.AddWithValue("@idgenero", idgenero);
            this.cn.Open();
            int eliminados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public List<Libro> getLibrosNombreGenero(String gnombre)
        {
            List<Libro> libros = new List<Libro>();
            var consultagenero = from datos in tablagenero.AsEnumerable() where datos.Field<String>("Genero") == gnombre select datos;
            var rowgenero = consultagenero.First();
            var idgenero = rowgenero.Field<int>("IdGenero");
            var consultalibro= from datos in tablalibro.AsEnumerable() where datos.Field<int>("IdGenero") == idgenero select datos;
            foreach (var row in consultalibro)
            {
                Libro lib = new Libro();
                lib.IdLibro = row.Field<int>("IdLibro");
                lib.Titulo = row.Field<String>("Titulo");
                lib.Autor = row.Field<String>("Autor");
                lib.Sinopsis = row.Field<String>("Sinopsis");
                lib.Imagen = row.Field<String>("Imagen");
                lib.IdGenero = row.Field<int>("IdGenero");
                libros.Add(lib);
            }
            return libros;
        }
    }
}
