using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaUniversitaria
{
    public class GestorBiblioteca
    {
        // Listas principales del sistema
        private List<Recurso> Recursos;
        private List<Usuario> Usuarios;
        private List<Prestamo> Prestamos;

        // Contadores para IDs autogenerados
        private int ContadorRecursos;
        private int ContadorEstudiantes;
        private int ContadorProfesores;

        public GestorBiblioteca()
        {
            Recursos = new List<Recurso>();
            Usuarios = new List<Usuario>();
            Prestamos = new List<Prestamo>();
            ContadorRecursos = 1;
            ContadorEstudiantes = 1;
            ContadorProfesores = 1;
        }

        #region Gestión de Recursos

        /// <summary>
        /// Registra un nuevo libro en el sistema o agrega copias si ya existe
        /// </summary>
        public void RegistrarLibro(string titulo, string autor, int año, int cantidad)
        {
            // Buscar libro existente por título, autor y año (ignorando mayúsculas)
            var existente = Recursos
                .OfType<Libro>()
                .FirstOrDefault(l =>
                    string.Equals(l.GetTitulo(), titulo, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(l.GetAutor, autor, StringComparison.OrdinalIgnoreCase) &&
                    l.GetAño == año);

            if (existente != null)
            {
                // Si existe, agregar copias al inventario en lugar de crear un nuevo recurso
                existente.AgregarCopias(cantidad);
                Console.WriteLine($"Libro '{titulo}' ya existente. Se agregaron {cantidad} copias. Total ahora: {existente.GetCantidadTotal()}");
                return;
            }

            string id = $"R{ContadorRecursos:D3}";
            Libro nuevoLibro = new Libro(id, titulo, cantidad, autor, año);
            Recursos.Add(nuevoLibro);
            ContadorRecursos++;
            Console.WriteLine($"Libro '{titulo}' registrado exitosamente con ID: {id}");
        }

        /// <summary>
        /// Registra una nueva revista en el sistema
        /// </summary>
        public void RegistrarRevista(string titulo, string edicion, int cantidad)
        {
            string id = $"R{ContadorRecursos:D3}";
            Revista nuevaRevista = new Revista(id, titulo, cantidad, edicion);
            Recursos.Add(nuevaRevista);
            ContadorRecursos++;
            Console.WriteLine($"Revista '{titulo}' registrada exitosamente con ID: {id}");
        }

        /// <summary>
        /// Muestra todos los recursos registrados
        /// </summary>
        public void MostrarTodosLosRecursos()
        {
            if (Recursos.Count == 0)
            {
                Console.WriteLine("No hay recursos registrados.");
                return;
            }

            Console.WriteLine("\n=== RECURSOS REGISTRADOS ===");
            foreach (var recurso in Recursos)
            {
                recurso.MostrarInfo();
                Console.WriteLine("----------------------------");
            }
        }

        /// <summary>
        /// Obtiene lista de recursos disponibles con formato para mostrar
        /// </summary>
        public List<string> ObtenerRecursosDisponibles()
        {
            return Recursos
                .Where(r => r.EstaDisponible())
                .Select((r, index) => $"{index + 1}. {r.GetTitulo()} ({r.GetCantidadDisponible()} copias disponibles)")
                .ToList();
        }

        /// <summary>
        /// Obtiene lista de todos los recursos con formato para mostrar
        /// </summary>
        public List<string> ObtenerTodosLosRecursosResumidos()
        {
            return Recursos
                .Select((r, index) => $"{index + 1}. {r.GetTitulo()} - {r.GetCantidadDisponible()}/{r.GetCantidadTotal()} disponibles")
                .ToList();
        }

        /// <summary>
        /// Busca un recurso por índice (de la lista mostrada al usuario)
        /// </summary>
        public Recurso? BuscarRecursoPorIndice(int indice, bool soloDisponibles = true)
        {
            var lista = soloDisponibles
                ? Recursos.Where(r => r.EstaDisponible()).ToList()
                : Recursos;

            if (indice < 0 || indice >= lista.Count)
            {
                return null;
            }

            return lista[indice];
        }

        #endregion

        #region Gestión de Usuarios

        /// <summary>
        /// Registra un nuevo estudiante en el sistema
        /// </summary>
        public void RegistrarEstudiante(string nombre)
        {
            string id = $"E{ContadorEstudiantes:D3}";
            Estudiante nuevoEstudiante = new Estudiante(id, nombre);
            Usuarios.Add(nuevoEstudiante);
            ContadorEstudiantes++;
            Console.WriteLine($"Estudiante '{nombre}' registrado exitosamente con ID: {id}");
        }

        /// <summary>
        /// Registra un nuevo profesor en el sistema
        /// </summary>
        public void RegistrarProfesor(string nombre)
        {
            string id = $"P{ContadorProfesores:D3}";
            Profesor nuevoProfesor = new Profesor(id, nombre);
            Usuarios.Add(nuevoProfesor);
            ContadorProfesores++;
            Console.WriteLine($"Profesor '{nombre}' registrado exitosamente con ID: {id}");
        }

        /// <summary>
        /// Muestra todos los usuarios registrados
        /// </summary>
        public void MostrarTodosLosUsuarios()
        {
            if (Usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }

            Console.WriteLine("\n=== USUARIOS REGISTRADOS ===");
            foreach (var usuario in Usuarios)
            {
                usuario.MostrarInfo();
                Console.WriteLine("----------------------------");
            }
        }

        /// <summary>
        /// Obtiene lista de usuarios con formato para mostrar
        /// </summary>
        public List<string> ObtenerUsuariosResumidos()
        {
            return Usuarios
                .Select((u, index) => $"{index + 1}. {u.GetId()} - {u.GetNombre()} ({u.GetRol()})")
                .ToList();
        }

        /// <summary>
        /// Busca un usuario por índice (de la lista mostrada al usuario)
        /// </summary>
        public Usuario? BuscarUsuarioPorIndice(int indice)
        {
            if (indice < 0 || indice >= Usuarios.Count)
            {
                return null;
            }

            return Usuarios[indice];
        }

        #endregion

        #region Gestión de Préstamos

        /// <summary>
        /// Realiza un nuevo préstamo
        /// </summary>
        public bool RealizarPrestamo(int indiceRecurso, int indiceUsuario)
        {
            // Buscar recurso (solo disponibles)
            Recurso? recurso = BuscarRecursoPorIndice(indiceRecurso, true);
            if (recurso == null)
            {
                Console.WriteLine("ERROR: Recurso no encontrado.");
                return false;
            }

            // Buscar usuario
            Usuario? usuario = BuscarUsuarioPorIndice(indiceUsuario);
            if (usuario == null)
            {
                Console.WriteLine("ERROR: Usuario no encontrado.");
                return false;
            }

            // Validar disponibilidad del recurso
            if (!recurso.EstaDisponible())
            {
                Console.WriteLine("ERROR: No hay copias disponibles de este recurso.");
                return false;
            }

            // Validar límite de recursos del usuario
            if (!ValidarLimiteUsuario(usuario))
            {
                Console.WriteLine($"ERROR: El usuario ha alcanzado su límite de {usuario.LimiteRecursos} recursos simultáneos.");
                return false;
            }

            // Crear préstamo
            Prestamo nuevoPrestamo = new Prestamo(recurso, usuario);
            Prestamos.Add(nuevoPrestamo);

            // Decrementar copias disponibles
            recurso.DecrementarDisponibilidad();

            Console.WriteLine($"Préstamo realizado exitosamente.");
            Console.WriteLine($"Fecha límite de devolución: {nuevoPrestamo.GetFechaLimite():dd/MM/yyyy}");

            return true;
        }

        /// <summary>
        /// Devuelve un recurso prestado
        /// </summary>
        public void DevolverPrestamo(int indicePrestamo)
        {
            if (indicePrestamo < 0 || indicePrestamo >= Prestamos.Count)
            {
                Console.WriteLine("ERROR: Préstamo no encontrado.");
                return;
            }

            Prestamo prestamo = Prestamos[indicePrestamo];

            if (!prestamo.EstaActivo())
            {
                Console.WriteLine("ERROR: Este préstamo ya fue devuelto.");
                return;
            }

            double multa = prestamo.Devolver();
            Console.WriteLine("Devolución registrada exitosamente.");
        }

        /// <summary>
        /// Renueva un préstamo activo
        /// </summary>
        public bool RenovarPrestamo(int indicePrestamo)
        {
            if (indicePrestamo < 0 || indicePrestamo >= Prestamos.Count)
            {
                Console.WriteLine("ERROR: Préstamo no encontrado.");
                return false;
            }

            Prestamo prestamo = Prestamos[indicePrestamo];

            if (!prestamo.EstaActivo())
            {
                Console.WriteLine("ERROR: No se puede renovar un préstamo ya devuelto.");
                return false;
            }

            return prestamo.Renovar();
        }

        /// <summary>
        /// Muestra todos los préstamos activos
        /// </summary>
        public void MostrarPrestamosActivos()
        {
            var prestamosActivos = Prestamos.Where(p => p.EstaActivo()).ToList();

            if (prestamosActivos.Count == 0)
            {
                Console.WriteLine("No hay préstamos activos.");
                return;
            }

            Console.WriteLine("\n=== PRÉSTAMOS ACTIVOS ===");
            for (int i = 0; i < prestamosActivos.Count; i++)
            {
                Console.WriteLine($"\nPréstamo #{i + 1}:");
                prestamosActivos[i].MostrarInfo();
                Console.WriteLine("----------------------------");
            }
        }

        /// <summary>
        /// Obtiene lista de préstamos activos con formato para mostrar
        /// </summary>
        public List<string> ObtenerPrestamosActivosResumidos()
        {
            return Prestamos
                .Where(p => p.EstaActivo())
                .Select((p, index) => $"{index + 1}. {p.ObtenerResumenBreve()}")
                .ToList();
        }

        /// <summary>
        /// Obtiene los préstamos activos de un usuario específico
        /// </summary>
        public List<Prestamo> ObtenerPrestamosPorUsuario(string idUsuario)
        {
            return Prestamos
                .Where(p => p.EstaActivo() && p.GetUsuario().GetId() == idUsuario)
                .ToList();
        }

        /// <summary>
        /// Cuenta la cantidad de préstamos activos de un usuario
        /// </summary>
        public int ContarPrestamosActivosUsuario(string idUsuario)
        {
            return Prestamos.Count(p => p.EstaActivo() && p.GetUsuario().GetId() == idUsuario);
        }

        #endregion

        #region Validaciones

        /// <summary>
        /// Valida si el usuario no ha excedido su límite de recursos
        /// </summary>
        private bool ValidarLimiteUsuario(Usuario usuario)
        {
            int prestamosActivos = ContarPrestamosActivosUsuario(usuario.GetId());
            return prestamosActivos < usuario.LimiteRecursos;
        }

        #endregion
    }
}
