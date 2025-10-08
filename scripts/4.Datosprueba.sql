-- =============================================
-- Script de Datos de Prueba
-- Proyecto: Sistema de Gesti�n de Estudiantes
-- =============================================

USE Estudiantes;
GO

-- Insertar Cursos
INSERT INTO Curso (NombreCurso, Descripcion, Nivel) VALUES
('Primero A', 'Primer grado - Secci�n A', 'Primaria'),
('Segundo B', 'Segundo grado - Secci�n B', 'Primaria'),
('Tercero A', 'Tercer grado - Secci�n A', 'Primaria'),
('Sexto A', 'Sexto grado - Secci�n A', 'Bachillerato'),
('Noveno B', 'Noveno grado - Secci�n B', 'Bachillerato');
GO

-- Insertar Materias
INSERT INTO Materia (NombreMateria, Descripcion, Creditos) VALUES
('Matem�ticas', 'Fundamentos matem�ticos y c�lculo', 4),
('Espa�ol', 'Lenguaje y literatura espa�ola', 3),
('Ciencias Naturales', 'Biolog�a, f�sica y qu�mica b�sica', 4),
('Ciencias Sociales', 'Historia, geograf�a y civismo', 3),
('Ingl�s', 'Idioma ingl�s b�sico e intermedio', 3),
('Educaci�n F�sica', 'Actividad f�sica y deportes', 2),
('Artes', 'Expresi�n art�stica y cultural', 2),
('Inform�tica', 'Tecnolog�a y computaci�n', 3);
GO

-- Insertar Estudiantes
INSERT INTO Estudiante (Nombres, Apellidos, Edad, Sexo, FechaNacimiento, Direccion, Telefono, Email, IdCurso) VALUES
('Juan Carlos', 'Garc�a P�rez', 12, 'M', '2013-03-15', 'Calle 10 #20-30', '3001234567', 'juan.garcia@email.com', 1),
('Mar�a Fernanda', 'Rodr�guez L�pez', 13, 'F', '2012-07-22', 'Carrera 5 #12-45', '3109876543', 'maria.rodriguez@email.com', 2),
('Carlos Andr�s', 'Mart�nez Silva', 14, 'M', '2011-11-08', 'Avenida 8 #15-20', '3201122334', 'carlos.martinez@email.com', 3),
('Ana Luc�a', 'Hern�ndez G�mez', 11, 'F', '2014-05-30', 'Calle 22 #8-15', '3156789012', 'ana.hernandez@email.com', 1),
('Diego Alejandro', 'L�pez Ram�rez', 15, 'M', '2010-09-18', 'Carrera 12 #25-40', '3187654321', 'diego.lopez@email.com', 4),
('Laura Valentina', 'S�nchez Torres', 16, 'F', '2009-02-14', 'Calle 30 #18-22', '3145678901', 'laura.sanchez@email.com', 5),
('Sebasti�n', 'Ram�rez Castro', 12, 'M', '2013-06-25', 'Carrera 20 #10-35', '3192345678', 'sebastian.ramirez@email.com', 2),
('Isabella', 'Vargas Moreno', 13, 'F', '2012-12-10', 'Avenida 15 #30-50', '3167890123', 'isabella.vargas@email.com', 3),
('Mateo', 'G�mez Ruiz', 15, 'M', '2010-04-20', 'Calle 40 #22-18', '3203456789', 'mateo.gomez@email.com', 4),
('Sof�a', 'Torres D�az', 16, 'F', '2009-08-05', 'Carrera 25 #35-42', '3154567890', 'sofia.torres@email.com', 5),
('Daniel', 'Castro Jim�nez', 11, 'M', '2014-01-12', 'Calle 18 #12-28', '3181234567', 'daniel.castro@email.com', 1),
('Valentina', 'Jim�nez Parra', 14, 'F', '2011-10-28', 'Avenida 22 #40-60', '3198765432', 'valentina.jimenez@email.com', 3),
('Santiago', 'Parra Vega', 12, 'M', '2013-09-15', 'Carrera 30 #15-25', '3162345678', 'santiago.parra@email.com', 2),
('Camila', 'Vega Rojas', 15, 'F', '2010-07-03', 'Calle 50 #28-35', '3173456789', 'camila.vega@email.com', 4),
('Nicol�s', 'Rojas M�ndez', 16, 'M', '2009-11-22', 'Avenida 35 #45-55', '3184567890', 'nicolas.rojas@email.com', 5);
GO

-- Insertar Calificaciones
INSERT INTO Calificaciones (IdEstudiante, IdMateria, Periodo, Nota, Observaciones) VALUES
-- Estudiante 1
(1, 1, '2024-1', 8.5, 'Buen desempe�o en operaciones b�sicas'),
(1, 2, '2024-1', 9.0, 'Excelente lectura comprensiva'),
(1, 3, '2024-1', 7.5, 'Debe reforzar temas de biolog�a'),
-- Estudiante 2
(2, 1, '2024-1', 6.5, 'Necesita practicar m�s'),
(2, 2, '2024-1', 8.0, 'Buena participaci�n en clase'),
(2, 4, '2024-1', 7.0, 'Cumple con las tareas'),
-- Estudiante 3
(3, 1, '2024-1', 9.5, 'Sobresaliente en matem�ticas'),
(3, 3, '2024-1', 8.5, 'Muy buen trabajo en laboratorio'),
(3, 5, '2024-1', 7.0, 'Debe mejorar pronunciaci�n'),
-- Estudiante 4
(4, 1, '2024-1', 5.5, 'Requiere apoyo adicional'),
(4, 2, '2024-1', 6.0, 'Avance lento pero constante'),
-- Estudiante 5
(5, 1, '2024-1', 8.0, 'Buen nivel acad�mico'),
(5, 3, '2024-1', 8.5, 'Destaca en ciencias'),
(5, 5, '2024-1', 9.0, 'Excelente dominio del idioma'),
-- Estudiante 6
(6, 1, '2024-1', 7.5, 'Desempe�o adecuado'),
(6, 4, '2024-1', 8.0, 'Interesada en historia'),
(6, 8, '2024-1', 9.5, 'Excelente en programaci�n'),
-- Estudiante 7
(7, 1, '2024-1', 6.0, 'Necesita reforzar conceptos'),
(7, 2, '2024-1', 7.5, 'Buena ortograf�a'),
-- Estudiante 8
(8, 1, '2024-1', 8.0, 'Buen razonamiento l�gico'),
(8, 3, '2024-1', 7.0, 'Cumple con objetivos'),
(8, 6, '2024-1', 9.0, 'Destacada en deportes'),
-- Estudiante 9
(9, 1, '2024-1', 9.0, 'Excelente estudiante'),
(9, 5, '2024-1', 8.5, 'Muy buena pronunciaci�n'),
-- Estudiante 10
(10, 1, '2024-1', 7.0, 'Desempe�o regular'),
(10, 4, '2024-1', 8.5, 'Excelente an�lisis hist�rico'),
(10, 8, '2024-1', 9.0, 'Muy h�bil con tecnolog�a'),
-- Estudiante 11
(11, 1, '2024-1', 5.0, 'Debe esforzarse m�s'),
(11, 2, '2024-1', 6.5, 'Mejorando gradualmente'),
-- Estudiante 12
(12, 1, '2024-1', 8.5, 'Muy aplicada'),
(12, 3, '2024-1', 9.0, 'Excelente en experimentos'),
-- Estudiante 13
(13, 1, '2024-1', 7.5, 'Buen trabajo'),
(13, 7, '2024-1', 9.5, 'Talento art�stico notable'),
-- Estudiante 14
(14, 1, '2024-1', 8.0, 'Desempe�o constante'),
(14, 5, '2024-1', 7.5, 'Avanzando bien'),
-- Estudiante 15
(15, 1, '2024-1', 9.5, 'Estudiante destacado'),
(15, 4, '2024-1', 9.0, 'Excelente comprensi�n'),
(15, 8, '2024-1', 10.0, 'Sobresaliente en tecnolog�a');
GO

PRINT 'Datos de prueba insertados exitosamente';
PRINT 'Total Cursos: 5';
PRINT 'Total Materias: 8';
PRINT 'Total Estudiantes: 15';
PRINT 'Total Calificaciones: 38';
GO