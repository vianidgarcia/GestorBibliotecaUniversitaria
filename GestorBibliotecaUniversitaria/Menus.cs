using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace GestorBibliotecaUniversitaria
{
    public static class Menus
    {
        // Colores temáticos para consistencia
        private const ConsoleColor ColorBorde = ConsoleColor.Cyan;
        private const ConsoleColor ColorTitulo = ConsoleColor.White;
        private const ConsoleColor ColorOpcion = ConsoleColor.Gray;
        private const ConsoleColor ColorNumero = ConsoleColor.Yellow;

        public static void MostrarMenuPrincipal()
        {
            DibujarCabecera("GESTIÓN DE BIBLIOTECA UNIVERSITARIA");

            EscribirOpcion("1", "Gestión de Recursos");
            EscribirOpcion("2", "Gestión de Usuarios");
            EscribirOpcion("3", "Gestión de Préstamos");
            EscribirOpcion("4", "Salir del Sistema");

            DibujarPie();
        }

        public static void MostrarMenuRecursos()
        {
            DibujarCabecera("MENÚ DE RECURSOS");

            EscribirOpcion("1", "Registrar Libro");
            EscribirOpcion("2", "Registrar Revista");
            EscribirOpcion("3", "Mostrar Todos los Recursos");
            EscribirOpcion("0", "Volver al menú anterior");

            DibujarPie();
        }

        public static void MostrarMenuUsuarios()
        {
            DibujarCabecera("MENÚ DE USUARIOS");

            EscribirOpcion("1", "Registrar Estudiante");
            EscribirOpcion("2", "Registrar Profesor");
            EscribirOpcion("3", "Mostrar Todos los Usuarios");
            EscribirOpcion("0", "Volver al menú anterior");

            DibujarPie();
        }
        public static void MostrarMenuPrestamos()
        {
            DibujarCabecera("MENÚ DE PRÉSTAMOS");
            EscribirOpcion("1", "Realizar Préstamo");
            EscribirOpcion("2", "Renovar Préstamo");
            EscribirOpcion("3", "Devolver Recurso");
            EscribirOpcion("4", "Mostrar Préstamos Activos");
            EscribirOpcion("0", "Volver al menú anterior");
            DibujarPie();
        }

        // --- MÉTODOS DE SOPORTE PARA EL DISEÑO ---

        private static void DibujarCabecera(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ColorBorde;
            Console.WriteLine("\n\t╔" + new string('═', 50) + "╗");

            Console.Write("\t║");
            Console.ForegroundColor = ColorTitulo;
            // Centrar el texto dinámicamente
            int espacios = (50 - titulo.Length) / 2;
            Console.Write(new string(' ', espacios) + titulo + new string(' ', 50 - titulo.Length - espacios));

            Console.ForegroundColor = ColorBorde;
            Console.WriteLine("║");
            Console.WriteLine("\t╚" + new string('═', 50) + "╝\n");
        }

        private static void EscribirOpcion(string numero, string texto)
        {
            Console.Write("\t  ");
            Console.ForegroundColor = ColorNumero;
            Console.Write($"{numero}. ");
            Console.ForegroundColor = ColorOpcion;
            Console.WriteLine(texto);
        }

        private static void DibujarPie()
        {
            Console.ForegroundColor = ColorBorde;
            Console.WriteLine("\n\t" + new string('─', 52));
            Console.ForegroundColor = ColorOpcion;
            Console.Write("\t  Seleccione una opción: ");
            Console.ResetColor();
        }
    }
}