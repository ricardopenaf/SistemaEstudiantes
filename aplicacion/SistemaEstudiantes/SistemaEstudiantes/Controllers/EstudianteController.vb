Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    Public Class EstudianteController
        Inherits Controller


        Private ReadOnly _bll As New EstudianteBLL()
        Private ReadOnly _cursoBLL As New CursoBLL()
        Private ReadOnly _materiasBLL As New MateriaBLL()
        Private ReadOnly _calificacionBLL As New CalificacionBLL()
        Public Sub New()
            _bll = New EstudianteBLL()
            _calificacionBLL = New CalificacionBLL()  ' ← VERIFICAR QUE ESTO EXISTA
        End Sub
        ' GET: Estudiante
        Function Index() As ActionResult
            Return View()
        End Function
        Function Create() As ActionResult
            ' Llenar lista de cursos activos para el combo
            ViewBag.Cursos = New SelectList(_cursoBLL.ObtenerTodos(), "IdCurso", "Descripcion")
            Return View()
        End Function
        ' POST: Estudiante/Create
        <HttpPost()>
        Function Create(ByVal estudiante As Estudiante) As ActionResult
            Try
                If ModelState.IsValid Then
                    estudiante.FechaRegistro = DateTime.Now
                    estudiante.Activo = True
                    _bll.Insertar(estudiante)

                    TempData("Mensaje") = "El estudiante fue creado exitosamente."
                    Return RedirectToAction("Index")
                End If

                ' Si algo falla, recargar cursos
                ViewBag.Cursos = New SelectList(_cursoBLL.ObtenerTodos(), "IdCurso", "NombreCurso", estudiante.IdCurso)
                Return View(estudiante)

            Catch ex As Exception
                ViewBag.Error = "Error al crear el estudiante: " & ex.Message
                ViewBag.Cursos = New SelectList(_cursoBLL.ObtenerTodos(), "IdCurso", "NombreCurso", estudiante.IdCurso)
                Return View(estudiante)
            End Try
        End Function
        Function Edit(id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim estudiante = _bll.ObtenerPorId(id)
            If estudiante Is Nothing Then
                Return HttpNotFound()
            End If

            ViewBag.Cursos = New SelectList(_cursoBLL.ObtenerTodos(), "IdCurso", "Descripcion", estudiante.IdCurso)
            Return View(estudiante)
        End Function

        <HttpPost()>
        Function Edit(estudiante As Estudiante) As ActionResult
            Try
                If ModelState.IsValid Then
                    _bll.Actualizar(estudiante)
                    Return Json(New With {.success = True})
                End If
                Return Json(New With {.success = False, .error = "Modelo inválido"})
            Catch ex As Exception
                Return Json(New With {.success = False, .error = ex.Message})
            End Try
        End Function

        Function ObtenerPorId(id As Integer) As JsonResult
            Dim est = _bll.ObtenerPorId(id)
            Return Json(est, JsonRequestBehavior.AllowGet)
        End Function
        Function Eliminar(id As Integer) As ActionResult
            Dim estudiante = _bll.ObtenerPorId(id)
            If estudiante Is Nothing Then
                Return HttpNotFound("Estudiante no encontrado.")
            End If
            Return View(estudiante)
        End Function

        <HttpPost>
        Function EliminarConfirmado(id As Integer) As ActionResult
            Try
                If _bll.Eliminar(id) Then
                    TempData("Mensaje") = "✅ El estudiante fue eliminado correctamente."
                Else
                    TempData("Error") = "⚠️ No se pudo eliminar el estudiante."
                End If
                Return RedirectToAction("Index")
            Catch ex As Exception
                TempData("Error") = "❌ Error al eliminar: " & ex.Message
                Return RedirectToAction("Index")
            End Try
        End Function
        Public Function AdicionarNota(ByVal idEstudiante As Integer) As ActionResult

            Dim estudiante As Estudiante = _bll.ObtenerPorId(idEstudiante)

            If estudiante Is Nothing Then
                ' Manejar si el estudiante no existe
                Return RedirectToAction("Index")
            End If

            Dim viewModel As New AdicionarNotaViewModel With {
            .Estudiante = estudiante,
            .Cursos = _cursoBLL.ObtenerTodos(),
            .Materias = _materiasBLL.ObtenerTodos()
        }

            ' Retornar la vista Views/Estudiantes/AdicionarNota.vbhtml
            Return View(viewModel)
        End Function
        ''' <summary>
        ''' Recibe el objeto Calificacion con todos los campos vía AJAX y llama al BLL.
        ''' </summary>
        <HttpPost>
        Public Function GuardarNota(ByVal calificacion As Calificacion) As JsonResult
            Try

                Dim resultado As Boolean = _calificacionBLL.Insertar(calificacion)

                If resultado Then
                    Return Json(New With {.success = True, .message = "Nota guardada exitosamente."})
                Else
                    Return Json(New With {.success = False, .message = "Error al insertar la nota en la base de datos (0 filas afectadas)."})
                End If
            Catch ex As Exception
                Response.StatusCode = 500
                Return Json(New With {.success = False, .message = "Error del servidor: " & ex.Message & " - Detalle: " & ex.InnerException?.Message})
            End Try
        End Function
        Public Function ListarNotas(ByVal idEstudiante As Integer) As ActionResult

            If idEstudiante <= 0 Then
                Return RedirectToAction("Index", "Estudiantes")
            End If

            Dim estudiante As Estudiante = _bll.ObtenerPorId(idEstudiante)

            If estudiante Is Nothing Then
                Return RedirectToAction("Index", "Estudiantes")
            End If

            Dim viewModel As New ListarNotasViewModel With {
            .IdEstudiante = estudiante.IdEstudiante,
            .NombreCompletoEstudiante = estudiante.Nombres & " " & estudiante.Apellidos,
            .Calificaciones = New List(Of Calificacion)() ' Dejamos la lista vacía o nula, ya que JS la poblará
        }

            Return View(viewModel)
        End Function

        <HttpGet>
        Public Function ObtenerNotasJson(ByVal idEstudiante As Integer) As JsonResult
            Try
                System.Diagnostics.Debug.WriteLine("========================================")
                System.Diagnostics.Debug.WriteLine("ObtenerNotasJson llamado")
                System.Diagnostics.Debug.WriteLine("ID Estudiante: " & idEstudiante)

                ' Verificar si _calificacionBLL está inicializado
                If _calificacionBLL Is Nothing Then
                    System.Diagnostics.Debug.WriteLine("ERROR: _calificacionBLL es Nothing")
                    Response.StatusCode = 500
                    Return Json(New With {.success = False, .message = "_calificacionBLL no inicializado"}, JsonRequestBehavior.AllowGet)
                End If

                System.Diagnostics.Debug.WriteLine("_calificacionBLL inicializado correctamente")

                ' Intentar obtener calificaciones
                Dim calificaciones As List(Of CalificacionDetalle) = _calificacionBLL.ObtenerPorEstudiante(idEstudiante)

                System.Diagnostics.Debug.WriteLine("Calificaciones obtenidas: " & If(calificaciones IsNot Nothing, calificaciones.Count.ToString(), "NULL"))

                If calificaciones Is Nothing Then
                    calificaciones = New List(Of CalificacionDetalle)()
                End If

                ' Crear datos JSON manualmente
                Dim datosJson As New List(Of Object)

                For Each cal In calificaciones
                    System.Diagnostics.Debug.WriteLine("Procesando calificación ID: " & cal.IdCalificacion)

                    datosJson.Add(New With {
                .IdCalificacion = cal.IdCalificacion,
                .DescripcionMateria = "Materia " & cal.IdMateria.ToString(),
                .Periodo = If(cal.Periodo, "N/A"),
                .Nota = cal.Nota,
                .Observaciones = If(cal.Observaciones, ""),
                .FechaCalificacion = cal.FechaCalificacion.ToString("yyyy-MM-ddTHH:mm:ss")
            })
                Next

                System.Diagnostics.Debug.WriteLine("JSON creado con " & datosJson.Count & " elementos")
                System.Diagnostics.Debug.WriteLine("========================================")

                Return Json(New With {.success = True, .data = datosJson}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("========================================")
                System.Diagnostics.Debug.WriteLine("ERROR CAPTURADO:")
                System.Diagnostics.Debug.WriteLine("Mensaje: " & ex.Message)
                System.Diagnostics.Debug.WriteLine("Tipo: " & ex.GetType().ToString())
                System.Diagnostics.Debug.WriteLine("Stack Trace: " & ex.StackTrace)
                If ex.InnerException IsNot Nothing Then
                    System.Diagnostics.Debug.WriteLine("Inner Exception: " & ex.InnerException.Message)
                End If
                System.Diagnostics.Debug.WriteLine("========================================")

                Response.StatusCode = 500
                Return Json(New With {
            .success = False,
            .message = ex.Message,
            .tipo = ex.GetType().ToString()
        }, JsonRequestBehavior.AllowGet)
            End Try
        End Function
        Function ObtenerTodos() As JsonResult
            Dim lista = _bll.ObtenerTodos()
            Return Json(lista, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace