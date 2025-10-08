-- =============================================
-- Procedimientos Almacenados para Tabla: Curso
-- =============================================

USE Estudiantes;
GO

-- =============================================
-- SP: Seleccionar todos los cursos
-- =============================================
CREATE OR ALTER PROCEDURE SP_Curso_Seleccionar
AS
BEGIN
    BEGIN TRY
        SELECT 
            IdCurso,
            NombreCurso,
            Descripcion,
            Nivel,
            FechaCreacion,
            Activo
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
-- SP: Seleccionar curso por ID
-- =============================================
CREATE OR ALTER PROCEDURE SP_Curso_SeleccionarPorId
    @IdCurso INT
AS
BEGIN
    BEGIN TRY
        SELECT 
            IdCurso,
            NombreCurso,
            Descripcion,
            Nivel,
            FechaCreacion,
            Activo
        FROM Curso
        WHERE IdCurso = @IdCurso;
    END TRY
    BEGIN CATCH
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Curso_SeleccionarPorId', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Insertar curso
-- =============================================
CREATE OR ALTER PROCEDURE SP_Curso_Insertar
    @NombreCurso NVARCHAR(100),
    @Descripcion NVARCHAR(500) = NULL,
    @Nivel NVARCHAR(50),
    @IdCurso INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        INSERT INTO Curso (NombreCurso, Descripcion, Nivel)
        VALUES (@NombreCurso, @Descripcion, @Nivel);
        
        SET @IdCurso = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION
        
        SELECT @IdCurso AS IdCurso;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Curso_Insertar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Actualizar curso
-- =============================================
CREATE OR ALTER PROCEDURE SP_Curso_Actualizar
    @IdCurso INT,
    @NombreCurso NVARCHAR(100),
    @Descripcion NVARCHAR(500) = NULL,
    @Nivel NVARCHAR(50),
    @Activo BIT = 1
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        UPDATE Curso
        SET 
            NombreCurso = @NombreCurso,
            Descripcion = @Descripcion,
            Nivel = @Nivel,
            Activo = @Activo
        WHERE IdCurso = @IdCurso;
        
        COMMIT TRANSACTION
        
        SELECT @@ROWCOUNT AS FilasAfectadas;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Curso_Actualizar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP: Eliminar curso (lógico)
-- =============================================
CREATE OR ALTER PROCEDURE SP_Curso_Eliminar
    @IdCurso INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        -- Validar que no tenga estudiantes activos
        IF EXISTS (SELECT 1 FROM Estudiante WHERE IdCurso = @IdCurso AND Activo = 1)
        BEGIN
            RAISERROR('No se puede eliminar el curso porque tiene estudiantes activos asociados', 16, 1);
            RETURN;
        END
        
        UPDATE Curso
        SET Activo = 0
        WHERE IdCurso = @IdCurso;
        
        COMMIT TRANSACTION
        
        SELECT @@ROWCOUNT AS FilasAfectadas;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        INSERT INTO LogErrores (NombreProcedimiento, NumeroError, MensajeError)
        VALUES ('SP_Curso_Eliminar', ERROR_NUMBER(), ERROR_MESSAGE());
        
        THROW;
    END CATCH
END
GO

PRINT 'Procedimientos almacenados de Curso creados exitosamente';
GO