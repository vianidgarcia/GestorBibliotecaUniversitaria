using System;
using System.Reflection;
using GestorBibliotecaUniversitaria;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BibliotecaTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CalcularMulta_PrestamoVencido_GeneraMultaYRestableceDisponibilidad()
        {
            // Preparación
            GestorBiblioteca gestor = new GestorBiblioteca();
            gestor.RegistrarLibro("Cien Años de Soledad", "Gabriel García Márquez", 1967, 3);
            gestor.RegistrarEstudiante("Juan Pérez");

            // Ejecutar préstamo
            bool realizado = gestor.RealizarPrestamo(0, 0);
            Assert.IsTrue(realizado, "El préstamo debería realizarse correctamente.");

            // Obtener préstamo creado
            var usuario = gestor.BuscarUsuarioPorIndice(0);
            Assert.IsNotNull(usuario);
            var prestamos = gestor.ObtenerPrestamosPorUsuario(usuario.GetId());
            Assert.AreEqual(1, prestamos.Count, "Debe existir un préstamo activo para el usuario.");
            var prestamo = prestamos[0];

            // Forzar fecha límite al pasado para simular retraso (usando reflexión)
            var propFechaLimite = typeof(Prestamo).GetProperty("FechaLimite", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(propFechaLimite, "No se encontró la propiedad privada 'FechaLimite' por reflexión.");
            propFechaLimite.SetValue(prestamo, DateTime.Now.AddDays(-5));

            // Devolver préstamo (esto fijará FechaDevolucion = Now)
            gestor.DevolverPrestamo(0);

            // Comprobaciones
            Assert.IsFalse(prestamo.EstaActivo(), "El préstamo debe quedar marcado como devuelto.");
            double multa = prestamo.CalcularMulta();
            Assert.IsTrue(multa > 0, "Se esperaba una multa mayor que cero por devolución tardía.");

            // Verificar que la disponibilidad del recurso volvió a su valor original
            var recurso = prestamo.GetRecurso();
            Assert.AreEqual(recurso.GetCantidadTotal(), recurso.GetCantidadDisponible(), "La cantidad disponible debe coincidir con la total después de la devolución.");
        }

        [TestMethod]
        public void RenovarPrestamo_PrimeraRenovacionOK_SegundaRenovacionFalla()
        {
            // Preparación
            GestorBiblioteca gestor = new GestorBiblioteca();
            gestor.RegistrarLibro("El Principito", "Antoine de Saint-Exupéry", 1943, 1);
            gestor.RegistrarProfesor("Dra. Gómez");

            // Realizar préstamo
            bool realizado = gestor.RealizarPrestamo(0, 0);
            Assert.IsTrue(realizado, "El préstamo debería realizarse correctamente.");

            // Primera renovación: debe ser exitosa
            bool renovado1 = gestor.RenovarPrestamo(0);
            Assert.IsTrue(renovado1, "La primera renovación debería ser exitosa.");

            // Segunda renovación: debe fallar porque ya fue renovado
            bool renovado2 = gestor.RenovarPrestamo(0);
            Assert.IsFalse(renovado2, "La segunda renovación debería fallar (ya renovado).");
        }

        [TestMethod]
        public void LimiteEstudiante_NoPermiteMasDeTresPrestamos()
        {
            // Preparación: registrar 4 libros distintos y 1 estudiante
            GestorBiblioteca gestor = new GestorBiblioteca();
            gestor.RegistrarLibro("Libro A", "Autor A", 2000, 1);
            gestor.RegistrarLibro("Libro B", "Autor B", 2001, 1);
            gestor.RegistrarLibro("Libro C", "Autor C", 2002, 1);
            gestor.RegistrarLibro("Libro D", "Autor D", 2003, 1);
            gestor.RegistrarEstudiante("Estudiante Limite");

            var usuario = gestor.BuscarUsuarioPorIndice(0);
            Assert.IsNotNull(usuario);

            // Un estudiante puede tomar hasta 3 recursos
            for (int i = 0; i < 3; i++)
            {
                bool exito = gestor.RealizarPrestamo(0, 0); // siempre pedir el primer recurso disponible
                Assert.IsTrue(exito, $"Préstamo #{i + 1} debería ser exitoso para el estudiante.");
            }

            // Intento adicional debe fallar
            bool cuarto = gestor.RealizarPrestamo(0, 0);
            Assert.IsFalse(cuarto, "El estudiante no debería poder tomar un cuarto recurso (límite = 3).");

            // Verificar contador de préstamos activos
            int cuenta = gestor.ContarPrestamosActivosUsuario(usuario.GetId());
            Assert.AreEqual(3, cuenta, "El estudiante debe tener exactamente 3 préstamos activos.");
        }

        [TestMethod]
        public void LimiteProfesor_NoPermiteMasDeCincoPrestamos()
        {
            // Preparación: registrar 6 libros distintos y 1 profesor
            GestorBiblioteca gestor = new GestorBiblioteca();
            gestor.RegistrarLibro("Libro 1", "Autor 1", 2010, 1);
            gestor.RegistrarLibro("Libro 2", "Autor 2", 2011, 1);
            gestor.RegistrarLibro("Libro 3", "Autor 3", 2012, 1);
            gestor.RegistrarLibro("Libro 4", "Autor 4", 2013, 1);
            gestor.RegistrarLibro("Libro 5", "Autor 5", 2014, 1);
            gestor.RegistrarLibro("Libro 6", "Autor 6", 2015, 1);
            gestor.RegistrarProfesor("Profesor Limite");

            var usuario = gestor.BuscarUsuarioPorIndice(0);
            Assert.IsNotNull(usuario);

            // Un profesor puede tomar hasta 5 recursos
            for (int i = 0; i < 5; i++)
            {
                bool exito = gestor.RealizarPrestamo(0, 0); // siempre pedir el primer recurso disponible
                Assert.IsTrue(exito, $"Préstamo #{i + 1} debería ser exitoso para el profesor.");
            }

            // Intento adicional debe fallar
            bool sexto = gestor.RealizarPrestamo(0, 0);
            Assert.IsFalse(sexto, "El profesor no debería poder tomar un sexto recurso (límite = 5).");

            // Verificar contador de préstamos activos
            int cuenta = gestor.ContarPrestamosActivosUsuario(usuario.GetId());
            Assert.AreEqual(5, cuenta, "El profesor debe tener exactamente 5 préstamos activos.");
        }
    }
}
