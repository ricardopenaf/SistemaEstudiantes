Imports System.Web.Mvc

Public Class NotasController
    Inherits Controller

    ' Instanciar las capas DAL
    Private estudianteDAL As New EstudianteDAL()
    Private cursoDAL As New CursoDAL()
    Private materiasDAL As New MateriasDAL()
    Private notasDAL As New CalificacionDAL()

    ' GET: Notas
    Public Function Index() As ActionResult
        Return View()
    End Function

    ' GET: Notas/Insertar
    Public Function Insertar() As ActionResult
        Return View()
    End Function

    ' API: GET - Obtener todos los estudiantes
    Public Function ObtenerEstudiantes() As JsonResult
        Try
            Dim estudiantes As List(Of Estudiante) = estudianteDAL.ObtenerTodos()

            Return Json(New With {
                .success = True,
                .data = estudiantes
            }, JsonRequestBehavior.AllowGet)

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al obtener estudiantes: " & ex.Message
            }, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' API: GET - Obtener todos los cursos
    Public Function ObtenerCursos() As JsonResult
        Try
            Dim cursos As List(Of Curso) = cursoDAL.ObtenerTodos()

            Return Json(New With {
                .success = True,
                .data = cursos
            }, JsonRequestBehavior.AllowGet)

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al obtener cursos: " & ex.Message
            }, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' API: GET - Obtener todas las materias
    Public Function ObtenerMaterias() As JsonResult
        Try
            Dim materias As List(Of Materia) = materiasDAL.ObtenerTodos()

            Return Json(New With {
                .success = True,
                .data = materias
            }, JsonRequestBehavior.AllowGet)

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al obtener materias: " & ex.Message
            }, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' POST: Guardar una nueva nota
    <HttpPost>
    Public Function GuardarNota(idEstudiante As Integer, idCurso As Integer, idMateria As Integer, nota As Decimal) As JsonResult
        Try
            ' Validar que los valores sean válidos
            If idEstudiante <= 0 Then
                Return Json(New With {
                    .success = False,
                    .message = "Debe seleccionar un estudiante válido"
                })
            End If

            If idCurso <= 0 Then
                Return Json(New With {
                    .success = False,
                    .message = "Debe seleccionar un curso válido"
                })
            End If

            If idMateria <= 0 Then
                Return Json(New With {
                    .success = False,
                    .message = "Debe seleccionar una materia válida"
                })
            End If

            If nota < 0 OrElse nota > 100 Then
                Return Json(New With {
                    .success = False,
                    .message = "La nota debe estar entre 0 y 100"
                })
            End If

            ' Crear objeto Nota (ajusta según tu modelo)
            Dim nuevaNota As New Calificacion With {
                .IdEstudiante = idEstudiante,
                .IdMateria = idMateria,
                .Nota = nota,
                .FechaCalificacion = DateTime.Now
            }

            ' Insertar la nota usando la capa DAL
            Dim resultado As Boolean = notasDAL.Insertar(nuevaNota)

            If resultado Then
                Return Json(New With {
                    .success = True,
                    .message = "Nota guardada correctamente"
                })
            Else
                Return Json(New With {
                    .success = False,
                    .message = "No se pudo guardar la nota"
                })
            End If

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al guardar la nota: " & ex.Message
            })
        End Try
    End Function

    Public Function Listar() As ActionResult
        Try
            Dim notas As List(Of Calificacion) = notasDAL.ObtenerTodos()
            Return View(notas)
        Catch ex As Exception
            ViewBag.Error = "Error al cargar las notas: " & ex.Message
            Return View(New List(Of Calificacion)())
        End Try
    End Function

    ' POST: Eliminar una nota
    <HttpPost>
    Public Function Eliminar(id As Integer) As JsonResult
        Try
            Dim resultado As Boolean = notasDAL.Eliminar(id)

            If resultado Then
                Return Json(New With {
                    .success = True,
                    .message = "Nota eliminada correctamente"
                })
            Else
                Return Json(New With {
                    .success = False,
                    .message = "No se pudo eliminar la nota"
                })
            End If

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al eliminar la nota: " & ex.Message
            })
        End Try
    End Function

    ' GET: Obtener nota por ID
    Public Function ObtenerPorId(id As Integer) As JsonResult
        Try
            Dim nota As Calificacion = notasDAL.ObtenerPorId(id)

            If nota IsNot Nothing Then
                Return Json(New With {
                    .success = True,
                    .data = nota
                }, JsonRequestBehavior.AllowGet)
            Else
                Return Json(New With {
                    .success = False,
                    .message = "Nota no encontrada"
                }, JsonRequestBehavior.AllowGet)
            End If

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al obtener la nota: " & ex.Message
            }, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    ' POST: Actualizar una nota
    <HttpPost>
    Public Function Actualizar(id As Integer, idEstudiante As Integer, idCurso As Integer, idMateria As Integer, nota As Decimal) As JsonResult
        Try
            ' Validaciones
            If nota < 0 OrElse nota > 100 Then
                Return Json(New With {
                    .success = False,
                    .message = "La nota debe estar entre 0 y 100"
                })
            End If

            ' Crear objeto Nota actualizado
            Dim notaActualizada As New Calificacion With {
                .IdCalificacion = id,
                .IdEstudiante = idEstudiante,
                .IdMateria = idMateria,
                .Nota = nota,
                .FechaCalificacion = DateTime.Now
            }

            ' Actualizar en la base de datos
            Dim resultado As Boolean = notasDAL.Actualizar(notaActualizada)

            If resultado Then
                Return Json(New With {
                    .success = True,
                    .message = "Nota actualizada correctamente"
                })
            Else
                Return Json(New With {
                    .success = False,
                    .message = "No se pudo actualizar la nota"
                })
            End If

        Catch ex As Exception
            Return Json(New With {
                .success = False,
                .message = "Error al actualizar la nota: " & ex.Message
            })
        End Try
    End Function
End Class