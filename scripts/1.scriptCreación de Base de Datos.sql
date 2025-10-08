-- =============================================
-- Script de Creación de Base de Datos
-- Proyecto: Sistema de Gestión de Estudiantes
-- Autor: Sistema
-- Fecha: 2025-10-06
-- =============================================

USE master;
GO

-- Eliminar la base de datos si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Estudiantes')
BEGIN
    ALTER DATABASE Estudiantes SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Estudiantes;
END
GO

-- Crear la base de datos
CREATE DATABASE Estudiantes;
GO

USE Estudiantes;
GO

-- =============================================
-- Tabla: Curso
-- Descripción: Almacena los cursos disponibles
-- =============================================
CREATE TABLE Curso (
    IdCurso INT IDENTITY(1,1) NOT NULL,
    NombreCurso NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Nivel NVARCHAR(50) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Curso PRIMARY KEY (IdCurso),
    CONSTRAINT UQ_Curso_Nombre UNIQUE (NombreCurso)
);
GO

-- =============================================
-- Tabla: Materia
-- Descripción: Almacena las materias o asignaturas
-- =============================================
CREATE TABLE Materia (
    IdMateria INT IDENTITY(1,1) NOT NULL,
    NombreMateria NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Creditos INT NOT NULL DEFAULT 3,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Materia PRIMARY KEY (IdMateria),
    CONSTRAINT UQ_Materia_Nombre UNIQUE (NombreMateria),
    CONSTRAINT CK_Materia_Creditos CHECK (Creditos > 0)
);
GO

-- =============================================
-- Tabla: Estudiante
-- Descripción: Almacena información de los estudiantes
-- =============================================
CREATE TABLE Estudiante (
    IdEstudiante INT IDENTITY(1,1) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Sexo CHAR(1) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Direccion NVARCHAR(200) NULL,
    Telefono NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    IdCurso INT NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Estudiante PRIMARY KEY (IdEstudiante),
    CONSTRAINT FK_Estudiante_Curso FOREIGN KEY (IdCurso) REFERENCES Curso(IdCurso),
    CONSTRAINT CK_Estudiante_Sexo CHECK (Sexo IN ('M', 'F')),
    CONSTRAINT CK_Estudiante_Edad CHECK (Edad >= 5 AND Edad <= 100)
);
GO

-- =============================================
-- Tabla: Calificaciones
-- Descripción: Almacena las calificaciones de los estudiantes
-- =============================================
CREATE TABLE Calificaciones (
    IdCalificacion INT IDENTITY(1,1) NOT NULL,
    IdEstudiante INT NOT NULL,
    IdMateria INT NOT NULL,
    Periodo NVARCHAR(50) NOT NULL,
    Nota DECIMAL(4,2) NOT NULL,
    Observaciones NVARCHAR(500) NULL,
    FechaCalificacion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Calificaciones PRIMARY KEY (IdCalificacion),
    CONSTRAINT FK_Calificaciones_Estudiante FOREIGN KEY (IdEstudiante) REFERENCES Estudiante(IdEstudiante),
    CONSTRAINT FK_Calificaciones_Materia FOREIGN KEY (IdMateria) REFERENCES Materia(IdMateria),
    CONSTRAINT CK_Calificaciones_Nota CHECK (Nota >= 0 AND Nota <= 10)
);
GO

-- =============================================
-- Tabla: LogErrores
-- Descripción: Almacena los errores generados en los SP
-- =============================================
CREATE TABLE LogErrores (
    IdLog INT IDENTITY(1,1) NOT NULL,
    NombreProcedimiento NVARCHAR(200) NOT NULL,
    NumeroError INT NOT NULL,
    MensajeError NVARCHAR(MAX) NOT NULL,
    FechaError DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_LogErrores PRIMARY KEY (IdLog)
);
GO

-- =============================================
-- Índices adicionales para optimización
-- =============================================
CREATE INDEX IX_Estudiante_Curso ON Estudiante(IdCurso);
CREATE INDEX IX_Calificaciones_Estudiante ON Calificaciones(IdEstudiante);
CREATE INDEX IX_Calificaciones_Materia ON Calificaciones(IdMateria);
CREATE INDEX IX_Calificaciones_Nota ON Calificaciones(Nota);
GO

PRINT 'Base de datos y tablas creadas exitosamente';
GO