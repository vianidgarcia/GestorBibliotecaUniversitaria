# 📚 Sistema de Gestión de Biblioteca Universitaria

Proyecto de Programación Orientada a Objetos - Segundo Semestre

---

## Descripción

Sistema para administrar una biblioteca universitaria. Permite registrar libros, revistas, usuarios (estudiantes y profesores) y controlar préstamos con multas por retraso.

---

## Funcionalidades

### 1. Gestión de Recursos
- Registrar libros y revistas
- Control de copias disponibles
- Ver todos los recursos registrados

### 2. Gestión de Usuarios
- Registrar estudiantes
- Registrar profesores
- Ver todos los usuarios

### 3. Gestión de Préstamos
- Realizar préstamos
- Renovar préstamos (solo 1 vez)
- Devolver recursos
- Calcular multas automáticamente
- Ver préstamos activos

---

## Tecnologías Utilizadas

- **Lenguaje:** C#
- **Framework:** .NET 8.0
- **IDE:** Visual Studio 2022

---

## Cómo Ejecutar el Proyecto

### Desde Visual Studio
1. Abrir el archivo `.csproj` en Visual Studio
2. Presionar `F5` o click en "Ejecutar"

### Desde la Terminal
```bash
dotnet run
```

---

## Estructura del Proyecto

```
GestorBibliotecaUniversitaria/
│
├── Recurso.cs          (Clase abstracta para recursos)
├── Libro.cs            (Hereda de Recurso)
├── Revista.cs          (Hereda de Recurso)
├── Usuario.cs          (Clase abstracta para usuarios)
├── Estudiante.cs       (Hereda de Usuario)
├── Profesor.cs         (Hereda de Usuario)
├── Prestamo.cs         (Gestiona préstamos)
├── GestorBiblioteca.cs (Controlador principal)
├── Menus.cs            (Interfaz de usuario)
└── Program.cs          (Punto de entrada)
```

---

## Reglas del Sistema

### Límites de Préstamos

| Usuario | Días Permitidos | Máximo de Recursos |
|---------|----------------|-------------------|
| Estudiante | 7 días | 3 libros/revistas |
| Profesor | 30 días | 5 libros/revistas |

### Multas
- **$10 por cada día de retraso**
- Ejemplo: 5 días de retraso = $50

### Renovaciones
- Solo se puede renovar **1 vez** por préstamo
- No se puede renovar si el préstamo ya está vencido

---

## Ejemplo de Uso

1. **Registrar un libro:**
   - Menú → Gestión de Recursos → Registrar Libro
   - Título: "Don Quijote"
   - Autor: "Cervantes"
   - Año: 1605
   - Copias: 3

2. **Registrar un estudiante:**
   - Menú → Gestión de Usuarios → Registrar Estudiante
   - Nombre: "María López"
   - Se genera automáticamente ID: E001

3. **Realizar préstamo:**
   - Menú → Gestión de Préstamos → Realizar Préstamo
   - Seleccionar libro y estudiante
   - Sistema asigna fecha límite (7 días)

4. **Devolver libro:**
   - Menú → Gestión de Préstamos → Devolver Recurso
   - Si hay retraso, muestra la multa

---

## Conceptos de POO Aplicados

- ✅ **Herencia:** Libro y Revista heredan de Recurso
- ✅ **Polimorfismo:** Estudiante y Profesor heredan de Usuario
- ✅ **Encapsulamiento:** Propiedades privadas con getters
- ✅ **Abstracción:** Clases abstractas Recurso y Usuario

---

## Limitaciones

- Los datos NO se guardan al cerrar el programa (almacenamiento en memoria)
- Solo funciona en consola (no tiene interfaz gráfica)
- Las multas solo se muestran, no se cobran realmente

---

## Pruebas Realizadas

Se realizaron 4 pruebas unitarias con MSTest:

1. ✅ Cálculo correcto de multas
2. ✅ Renovación limitada a 1 vez
3. ✅ Límite de 3 recursos para estudiantes
4. ✅ Límite de 5 recursos para profesores

**Resultado:** Todas las pruebas pasaron correctamente

---

## Autor

**Daniela Garcia**
- Universidad: TecNM ITSM
- Carrera: Informática
- Materia: Programación Orientada a Objetos
- Semestre: 2

---

## Fecha de Entrega

Febrero 2026

---

## Notas

Este proyecto fue desarrollado como práctica de los conceptos de Programación Orientada a Objetos vistos en clase.
