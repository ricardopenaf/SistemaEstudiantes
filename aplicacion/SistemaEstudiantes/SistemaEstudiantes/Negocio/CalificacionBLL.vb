Public Class CalificacionBLL
    Private ReadOnly _calificacionDAL As New CalificacionDAL()

    ''' <summary>
    ''' Obtiene todas las calificaciones
    ''' </summary>
    Public Function ObtenerTodos() As List(Of Calificacion)
        Try
            Return _calificacionDAL.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener calificaciones: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una calificación por ID
    ''' </summary>
    Public Function ObtenerPorId(idCalificacion As Integer) As Calificacion
        If idCalificacion <= 0 Then
            Throw New ArgumentException("El ID de la calificación debe ser mayor a cero")
        End If

        Try
            Return _calificacionDAL.ObtenerPorId(idCalificacion)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener calificación: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene calificaciones por estudiante
    ''' </summary>
    Public Function ObtenerPorEstudiante(idEstudiante As Integer) As List(Of CalificacionDetalle)
        Try
            Return _calificacionDAL.ObtenerPorEstudiante(idEstudiante)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error en BLL: " & ex.Message)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Inserta una nueva calificación con validaciones
    ''' </summary>
    Public Function Insertar(calificacion As Calificacion) As Boolean
        ' Validaciones
        ValidarCalificacion(calificacion)

        Try
            Return _calificacionDAL.Insertar(calificacion)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al insertar calificación: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Actualiza una calificación existente con validaciones
    ''' </summary>
    Public Function Actualizar(calificacion As Calificacion) As Boolean
        If calificacion.Nota <= 0 Then
            Throw New ArgumentException("El ID de la calificación debe ser mayor a cero")
        End If

        ' Validaciones
        ValidarCalificacion(calificacion)

        Try
            Return _calificacionDAL.Actualizar(calificacion)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al actualizar calificación: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina una calificación
    ''' </summary>
    Public Function Eliminar(idCalificacion As Integer) As Boolean
        If idCalificacion <= 0 Then
            Throw New ArgumentException("El ID de la calificación debe ser mayor a cero")
        End If

        Try
            Return _calificacionDAL.Eliminar(idCalificacion)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al eliminar calificación: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Valida los datos de la calificación
    ''' </summary>
    Private Sub ValidarCalificacion(calificacion As Calificacion)
        If calificacion.IdEstudiante <= 0 Then
            Throw New ArgumentException("Debe seleccionar un estudiante válido")
        End If

        If calificacion.IdMateria <= 0 Then
            Throw New ArgumentException("Debe seleccionar una materia válida")
        End If

        If String.IsNullOrWhiteSpace(calificacion.Periodo) Then
            Throw New ArgumentException("El periodo es requerido")
        End If

        If calificacion.Nota < 0 OrElse calificacion.Nota > 10 Then
            Throw New ArgumentException("La nota debe estar entre 0 y 10")
        End If
    End Sub
End Class
