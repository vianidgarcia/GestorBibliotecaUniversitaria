using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaUniversitaria
{
    public class Prestamo
    {
        private Recurso Recurso { get; set; }
        private Usuario Usuario { get; set; }
        private DateTime FechaPrestamo { get; set; }
        private DateTime FechaLimite { get; set; }
        private DateTime? FechaDevolucion { get; set; }
        private bool Renovado { get; set; }

        public Prestamo(Recurso recurso, Usuario usuario)
        {
            Recurso = recurso;
            Usuario = usuario;
            FechaPrestamo = DateTime.Now;
            FechaLimite = DateTime.Now.AddDays(usuario.DiasMaximoPrestamo);
            FechaDevolucion = null;
            Renovado = false;
        }

        // Métodos públicos para acceder a propiedades privadas
        public Recurso GetRecurso() => Recurso;
        public Usuario GetUsuario() => Usuario;
        public DateTime GetFechaPrestamo() => FechaPrestamo;
        public DateTime GetFechaLimite() => FechaLimite;
        public DateTime? GetFechaDevolucion() => FechaDevolucion;

        /// <summary>
        /// Verifica si el préstamo está activo (no devuelto)
        /// </summary>
        public bool EstaActivo() => FechaDevolucion == null;

        /// <summary>
        /// Verifica si el préstamo está vencido
        /// </summary>
        public bool EstaVencido() => DateTime.Now > FechaLimite && EstaActivo();

        /// <summary>
        /// Verifica si el préstamo ya fue renovado
        /// </summary>
        public bool FueRenovado() => Renovado;

        /// <summary>
        /// Calcula la multa por días de retraso
        /// </summary>
        public double CalcularMulta()
        {
            if (FechaDevolucion != null && FechaDevolucion > FechaLimite)
            {
                int diasRetraso = (FechaDevolucion.Value - FechaLimite).Days;
                return diasRetraso * Usuario.GetMultaPorDia();
            }
            return 0;
        }

        /// <summary>
        /// Renueva el préstamo extendiendo la fecha límite
        /// Retorna true si la renovación fue exitosa
        /// </summary>
        public bool Renovar()
        {
            // Validar que no esté vencido
            if (EstaVencido())
            {
                Console.WriteLine("ERROR: No se puede renovar un préstamo vencido. Debe devolverse primero.");
                return false;
            }

            // Validar que no haya sido renovado antes
            if (Renovado)
            {
                Console.WriteLine("ERROR: Este préstamo ya fue renovado anteriormente.");
                return false;
            }

            // Renovar: reiniciar fecha límite desde hoy
            FechaLimite = DateTime.Now.AddDays(Usuario.DiasMaximoPrestamo);
            Renovado = true;
            Console.WriteLine($"Préstamo renovado exitosamente. Nueva fecha límite: {FechaLimite:dd/MM/yyyy}");
            return true;
        }

        /// <summary>
        /// Registra la devolución del recurso y retorna la multa
        /// </summary>
        public double Devolver()
        {
            FechaDevolucion = DateTime.Now;
            double multa = CalcularMulta();
            Recurso.IncrementarDisponibilidad();

            if (multa > 0)
            {
                Console.WriteLine($"MULTA: ${multa:F2} por {(FechaDevolucion.Value - FechaLimite).Days} día(s) de retraso.");
            }
            else
            {
                Console.WriteLine("Devolución a tiempo. Sin multa.");
            }

            return multa;
        }

        /// <summary>
        /// Muestra información detallada del préstamo
        /// </summary>
        public void MostrarInfo()
        {
            Console.WriteLine($"Recurso: {Recurso.GetTitulo()} (ID: {Recurso.GetId()})");
            Console.WriteLine($"Usuario: {Usuario.GetNombre()} (ID: {Usuario.GetId()})");
            Console.WriteLine($"Fecha de Préstamo: {FechaPrestamo:dd/MM/yyyy}");
            Console.WriteLine($"Fecha Límite: {FechaLimite:dd/MM/yyyy}");
            Console.WriteLine($"Estado: {(EstaActivo() ? (EstaVencido() ? "VENCIDO" : "Activo") : "Devuelto")}");
            if (Renovado)
            {
                Console.WriteLine("(Renovado)");
            }
            if (FechaDevolucion != null)
            {
                Console.WriteLine($"Fecha de Devolución: {FechaDevolucion:dd/MM/yyyy}");
            }
        }

        /// <summary>
        /// Retorna un resumen breve del préstamo para listas
        /// </summary>
        public string ObtenerResumenBreve()
        {
            string estado = EstaActivo() ? (EstaVencido() ? "[VENCIDO]" : "[Activo]") : "[Devuelto]";
            return $"{Usuario.GetNombre()} - {Recurso.GetTitulo()} {estado}";
        }
    }
}
