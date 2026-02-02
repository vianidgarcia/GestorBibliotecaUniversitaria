using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorBibliotecaUniversitaria
{
    public abstract class Usuario
    {
        protected string Id { get; set; } = string.Empty;
        protected string Nombre { get; set; } = string.Empty;
        protected double MultaPorDia { get; set; } = 10;
        protected string Rol { get; set; } = string.Empty;

        public abstract int DiasMaximoPrestamo { get; }
        public abstract int LimiteRecursos { get; }

        protected Usuario(string id, string nombre, string rol)
        {
            Id = id;
            Nombre = nombre;
            Rol = rol;
        }

        public string GetId() => Id;
        public string GetNombre() => Nombre;
        public double GetMultaPorDia() => MultaPorDia;
        public string GetRol() => Rol;
       

        /// <summary>
        /// Muestra la información del usuario
        /// </summary>
        public void MostrarInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Tipo: {Rol}");
            Console.WriteLine($"Límite de recursos: {LimiteRecursos}");
            Console.WriteLine($"Días máximo de préstamo: {DiasMaximoPrestamo}");
        }
    }

    public class Estudiante : Usuario
    {
        public override int DiasMaximoPrestamo => 7;
        public override int LimiteRecursos => 3;

        public Estudiante(string id, string nombre)
            : base(id, nombre, "Estudiante")
        {
        }
    }

    public class Profesor : Usuario
    {
        public override int DiasMaximoPrestamo => 30;
        public override int LimiteRecursos => 5;

        public Profesor(string id, string nombre)
            : base(id, nombre, "Profesor")
        {
        }
    }
}

