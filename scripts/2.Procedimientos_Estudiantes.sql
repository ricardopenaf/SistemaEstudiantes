-- =============================================
-- Procedimientos Almacenados para Tabla: Estudiante
-- =============================================

USE Estudiantes;
GO

-- =============================================
-- SP: Seleccionar todos los estudiantes
-- =============================================
CREATE OR ALTER PROCEDURE SP_Estudiante_Seleccionar
AS
BEGIN
    BEGIN TRY
        SELECT 
            e.IdEstudiante,
            e.Nombres,
            e.Apellidos,
            e.Edad,
            e.Sexo,
            e.FechaNacimiento,
            e.Direccion,
            e.Telefono,
            e.Email,
            e.IdCurso,
            c.NombreCurso,
            c.Nivel,
            e.FechaRegistro,
            e.Activo
        FROM Estudiante e
        INNER JOIN Curso c ON e.IdCurso = c.IdCurso
        WHERE e.Activo = 1
        ORDER BY e.Apellidos, e.Nombres;
    END TRY
    BEGIN CATCH
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Estudiante_Seleccionar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Seleccionar estudiante por ID
-- =============================================
CREATE OR ALTER PROCEDURE SP_Estudiante_SeleccionarPorId
    @IdEstudiante INT
AS
BEGIN
    BEGIN TRY
        SELECT 
            e.IdEstudiante,
            e.Nombres,
            e.Apellidos,
            e.Edad,
            e.Sexo,
            e.FechaNacimiento,
            e.Direccion,
            e.Telefono,
            e.Email,
            e.IdCurso,
            c.NombreCurso,
            c.Nivel,
            e.FechaRegistro,
            e.Activo
        FROM Estudiante e
        INNER JOIN Curso c ON e.IdCurso = c.IdCurso
        WHERE e.IdEstudiante = @IdEstudiante;
    END TRY
    BEGIN CATCH
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Estudiante_SeleccionarPorId', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Insertar estudiante
-- =============================================
CREATE OR ALTER PROCEDURE SP_Estudiante_Insertar
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @Edad INT,
    @Sexo CHAR(1),
    @FechaNacimiento DATE,
    @Direccion NVARCHAR(200) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @IdCurso INT,
    @IdEstudiante INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        -- Validar que el curso exista y esté activo
        IF NOT EXISTS (SELECT 1 FROM Curso WHERE IdCurso = @IdCurso AND Activo = 1)
        BEGIN
            RAISERROR('El curso especificado no existe o no está activo', 16, 1);
            RETURN;
        END
        
        INSERT INTO Estudiante (
            Nombres, Apellidos, Edad, Sexo, FechaNacimiento, 
            Direccion, Telefono, Email, IdCurso
        )
        VALUES (
            @Nombres, @Apellidos, @Edad, @Sexo, @FechaNacimiento,
            @Direccion, @Telefono, @Email, @IdCurso
        );
        
        SET @IdEstudiante = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION
        
        SELECT @IdEstudiante AS IdEstudiante;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Estudiante_Insertar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Actualizar estudiante
-- =============================================
CREATE OR ALTER PROCEDURE SP_Estudiante_Actualizar
    @IdEstudiante INT,
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @Edad INT,
    @Sexo CHAR(1),
    @FechaNacimiento DATE,
    @Direccion NVARCHAR(200) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @IdCurso INT,
    @Activo BIT = 1
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        -- Validar que el curso exista y esté activo
        IF NOT EXISTS (SELECT 1 FROM Curso WHERE IdCurso = @IdCurso AND Activo = 1)
        BEGIN
            RAISERROR('El curso especificado no existe o no está activo', 16, 1);
            RETURN;
        END
        
        UPDATE Estudiante
        SET 
            Nombres = @Nombres,
            Apellidos = @Apellidos,
            Edad = @Edad,
            Sexo = @Sexo,
            FechaNacimiento = @FechaNacimiento,
            Direccion = @Direccion,
            Telefono = @Telefono,
            Email = @Email,
            IdCurso = @IdCurso,
            Activo = @Activo
        WHERE IdEstudiante = @IdEstudiante;
        
        COMMIT TRANSACTION
        
        SELECT @@ROWCOUNT AS FilasAfectadas;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Estudiante_Actualizar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Eliminar estudiante (lógico)
-- =============================================
CREATE OR ALTER PROCEDURE SP_Estudiante_Eliminar
    @IdEstudiante INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        UPDATE Estudiante
        SET Activo = 0
        WHERE IdEstudiante = @IdEstudiante;
        
        COMMIT TRANSACTION
        
        SELECT @@ROWCOUNT AS FilasAfectadas;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Estudiante_Eliminar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE [dbo].[SP_ListaCursos]
AS
BEGIN
    BEGIN TRY
        SELECT 
            IdCurso,
            Descripcion
        FROM Curso
        WHERE Activo = 1
        ORDER BY NombreCurso;
    END TRY
    BEGIN CATCH
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Curso_Seleccionar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO
-- =============================================
-- SP: Insertar Calificaciones
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[SP_Calificacion_Insertar]
    @IdEstudiante	int,
    @IdMateria		int,
	@Periodo		NVARCHAR(50),
    @Nota			decimal(3, 1),
    @Observaciones  NVARCHAR(500)=null,
	@IdNotas	    INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        INSERT INTO Calificaciones(
            IdEstudiante, IdMateria, Periodo, Nota, Observaciones, FechaCalificacion
        )
        VALUES (
           @IdEstudiante,@IdMateria,@Periodo,@Nota,@Observaciones,GETDATE()
        );
        
        SET @IdNotas = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION
        
        SELECT @IdNotas AS IdCalificacion;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Calificacion_Insertar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

CREATE OR ALTER  PROCEDURE [dbo].[SP_ListaMaterias]
AS
BEGIN
    BEGIN TRY
        SELECT 
            IdMateria,
            Descripcion
        FROM Materia
        WHERE Activo = 1
        ORDER BY IdMateria;
    END TRY
    BEGIN CATCH
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_ListaMaterias', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
go

CREATE OR ALTER PROCEDURE SP_Calificacion_SeleccionarPorEstudiante
    @IdEstudiante INT
AS
BEGIN
    SELECT 
        c.IdCalificacion,
        c.IdEstudiante,
        c.IdMateria,
        ISNULL(m.Descripcion, 'Sin Materia') AS DescripcionMateria,  -- AGREGAR ESTO
        c.Periodo,
        c.Nota,
        c.Observaciones,
        c.FechaCalificacion
    FROM Calificaciones c
    LEFT JOIN Materia m ON c.IdMateria = m.IdMateria
    WHERE c.IdEstudiante = @IdEstudiante
    ORDER BY c.FechaCalificacion DESC
END
GO


PRINT 'Procedimientos almacenados de Estudiante creados exitosamente';
GO
