using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaUniversitaria
{
    public abstract class Recurso
    {
        protected string Id { get; set; }
        protected string Titulo { get; set; }
        protected int CantidadTotal { get; set; }
        protected int CantidadDisponible { get; set; }

        protected Recurso(string id, string titulo, int cantT)
        {
            Id = id;
            Titulo = titulo;
            CantidadTotal = cantT;
            CantidadDisponible = cantT;
        }

        public string GetId() => Id;
        public string GetTitulo() => Titulo;
        public int GetCantidadDisponible() => CantidadDisponible;

        public int GetCantidadTotal() => CantidadTotal;
        public bool EstaDisponible() => CantidadDisponible != 0;

        public void DecrementarDisponibilidad()
        {
            if (CantidadDisponible > 0)
            {
                CantidadDisponible--;
            }
        }

        public void IncrementarDisponibilidad()
        {
            if (CantidadDisponible < CantidadTotal)
            {
                CantidadDisponible++;
            }
        }

        public abstract void MostrarInfo();
        
    }

    public class Libro : Recurso
    { 
        private string Autor { get; set; }
        private int Año { get; set; }

        public Libro (string id, string titulo, int cantT, string autor, int año)
            : base (id, titulo, cantT)
        {
            Autor = autor;
            Año = año;
        }

        public override void MostrarInfo() 
        {
            Console.WriteLine($"[LIBRO] ID: {Id}");
            Console.WriteLine($"  Título: {Titulo}");
            Console.WriteLine($"  Autor: {Autor}");
            Console.WriteLine($"  Año: {Año}");
            Console.WriteLine($"  Copias: {CantidadDisponible}/{CantidadTotal} disponibles");
        }

        public string GetAutor => Autor;
        public int GetAño => Año;
    }

    public class Revista : Recurso
    {
        private string Edicion { get; set; }

        public Revista(string id, string titulo, int cantT, string edicion)
            : base(id, titulo, cantT)
        {
            Edicion = edicion;
        }

        public override void MostrarInfo()
        {
            Console.WriteLine($"[REVISTA] ID: {Id}");
            Console.WriteLine($"  Título: {Titulo}");
            Console.WriteLine($"  Edición: {Edicion}");
            Console.WriteLine($"  Copias: {CantidadDisponible}/{CantidadTotal} disponibles");
        }

        public string GetEdicion => Edicion;
    }
}
