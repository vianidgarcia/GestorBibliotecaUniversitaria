// See https://aka.ms/new-console-template for more information
using GestorBibliotecaUniversitaria;

Console.Title = "Gestor de Biblioteca Universitaria";
GestorBiblioteca gestor = new GestorBiblioteca();

static void Pausa()
{
    Console.WriteLine("\nPulse una tecla para continuar...");
    Console.ReadKey(true);
}

bool exit = false;
while (!exit)
{
    Menus.MostrarMenuPrincipal();
    var opcion = Console.ReadLine();
    switch (opcion)
    {
        case "1":
            bool backToMainFromResources = false;
            while (!backToMainFromResources)
            {
                Console.Clear();
                Menus.MostrarMenuRecursos();
                var recursoOption = Console.ReadLine();
                Console.Clear();
                switch (recursoOption)
                {
                    case "1":
                        Console.Write("Ingrese el título del libro: ");
                        string tituloLibro = Console.ReadLine() ?? "";
                        Console.Write("Ingrese el autor del libro: ");
                        string autorLibro = Console.ReadLine() ?? "";
                        Console.Write("Ingrese el año de publicación: ");
                        int anioPublicacionLibro = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Ingrese la cantidad de copias: ");
                        int cantidadCopiasLibro = int.Parse(Console.ReadLine() ?? "0");
                        gestor.RegistrarLibro(tituloLibro, autorLibro, anioPublicacionLibro, cantidadCopiasLibro);
                        Console.WriteLine("Libro registrado exitosamente.");
                        Pausa();
                        break;
                    case "2":
                        Console.Write("Ingrese el título de la revista: ");
                        string tituloRevista = Console.ReadLine() ?? "";
                        Console.Write("Ingrese el número de edición: ");
                        string? numeroEdicion = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(numeroEdicion))
                        {
                            numeroEdicion = "";
                        }
                        Console.Write("Ingrese la cantidad de copias: ");
                        int cantidadCopiasRevista = int.Parse(Console.ReadLine() ?? "0");
                        gestor.RegistrarRevista(tituloRevista, numeroEdicion, cantidadCopiasRevista);
                        Console.WriteLine("Revista registrada exitosamente.");
                        Pausa();
                        break;
                    case "3":
                        gestor.MostrarTodosLosRecursos();
                        Pausa();
                        break;
                    case "0":
                        backToMainFromResources = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        Pausa();
                        break;
                }
            }
            break;

        case "2":
            bool backToMainFromUsuarios = false;
            while (!backToMainFromUsuarios)
            {
                Console.Clear();
                Menus.MostrarMenuUsuarios();
                var usuarioOption = Console.ReadLine();
                Console.Clear();
                switch (usuarioOption)
                {
                    case "1":
                        Console.Write("Ingrese el Nombre del Estudiante:");
                        string nomEst = Console.ReadLine() ?? "";
                        gestor.RegistrarEstudiante(nomEst);
                        Pausa();
                        break;
                    case "2":
                        Console.Write("Ingrese el Nombre del Profesor:");
                        string nomProf = Console.ReadLine() ?? "";
                        gestor.RegistrarProfesor(nomProf);
                        Pausa();
                        break;
                    case "3":
                        gestor.MostrarTodosLosUsuarios();
                        Pausa();
                        break;
                    case "0":
                        backToMainFromUsuarios = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        Pausa();
                        break;
                }
            }
            break;

        case "3":
            bool backToMainFromPrestamos = false;
            while (!backToMainFromPrestamos)
            {
                Console.Clear();
                Menus.MostrarMenuPrestamos();
                var prestamosOption = Console.ReadLine();
                Console.Clear();
                switch (prestamosOption)
                {
                    case "1":
                        // Mostrar recursos resumidos y pedir índice
                        var recursosResumidos = gestor.ObtenerTodosLosRecursosResumidos();
                        if (recursosResumidos.Count == 0)
                        {
                            Console.WriteLine("No hay recursos disponibles para préstamo.");
                            Pausa();
                            break;
                        }
                        Console.WriteLine("Recursos disponibles:");
                        foreach (var linea in recursosResumidos)
                        {
                            Console.WriteLine(linea);
                        }
                        Console.Write("Ingrese el Índice del Recurso (número): ");
                        string entradaRecurso = Console.ReadLine() ?? "";
                        if (!int.TryParse(entradaRecurso, out int indiceRecursoUsuario))
                        {
                            Console.WriteLine("Índice de recurso inválido.");
                            Pausa();
                            break;
                        }
                        int indiceRecurso = indiceRecursoUsuario - 1; // convertir a 0-based

                        // Mostrar usuarios resumidos y pedir índice
                        var usuariosResumidos = gestor.ObtenerUsuariosResumidos();
                        if (usuariosResumidos.Count == 0)
                        {
                            Console.WriteLine("No hay usuarios registrados para realizar el préstamo.");
                            Pausa();
                            break;
                        }
                        Console.WriteLine("\nUsuarios registrados:");
                        foreach (var linea in usuariosResumidos)
                        {
                            Console.WriteLine(linea);
                        }
                        Console.Write("Ingrese el Índice del Usuario (número): ");
                        string entradaUsuario = Console.ReadLine() ?? "";
                        if (!int.TryParse(entradaUsuario, out int indiceUsuarioUsuario))
                        {
                            Console.WriteLine("Índice de usuario inválido.");
                            Pausa();
                            break;
                        }
                        int indiceUsuario = indiceUsuarioUsuario - 1; // convertir a 0-based

                        gestor.RealizarPrestamo(indiceRecurso, indiceUsuario);
                        Pausa();
                        break;
                    case "2":
                        Console.Write("Ingrese el Indice del Préstamo a renovar:");
                        int indiceRenovacion = int.Parse(Console.ReadLine() ?? "0");
                        gestor.RenovarPrestamo(indiceRenovacion);
                        Pausa();
                        break;
                    case "3":
                        Console.Write("Ingrese el Indice del Préstamo a devolver:");
                        int indiceDevolucion = int.Parse(Console.ReadLine() ?? "0");
                        gestor.DevolverPrestamo(indiceDevolucion);
                        Pausa();
                        break;
                    case "4":
                        gestor.MostrarPrestamosActivos();
                        Pausa();
                        break;
                    case "0":
                        backToMainFromPrestamos = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        Pausa();
                        break;
                }
            }
                break; 
        case "4":
            exit = true;
            Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción no válida. Intente de nuevo.");
            Pausa();
            break;
    }
}
