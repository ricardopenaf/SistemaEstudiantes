''' <summary>
''' Capa de Lógica de Negocio para Estudiantes
''' </summary>
Public Class EstudianteBLL

    Private ReadOnly _estudianteDAL As New EstudianteDAL()

    ''' <summary>
    ''' Obtiene todos los estudiantes activos
    ''' </summary>
    Public Function ObtenerTodos() As List(Of Estudiante)
        Try
            Return _estudianteDAL.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener los estudiantes: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un estudiante por su ID
    ''' </summary>
    Public Function ObtenerPorId(idEstudiante As Integer) As Estudiante
        If idEstudiante <= 0 Then
            Throw New ArgumentException("El ID del estudiante debe ser mayor a cero")
        End If

        Try
            Return _estudianteDAL.ObtenerPorId(idEstudiante)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener el estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los estudiantes cuyo promedio de notas es mayor o igual a la nota mínima indicada
    ''' </summary>
    Public Function ObtenerPorNota(notaMinima As Decimal) As List(Of Estudiante)
        If notaMinima < 0 OrElse notaMinima > 10 Then
            Throw New ArgumentException("La nota mínima debe estar entre 0 y 10")
        End If

        Try
            Return _estudianteDAL.ObtenerPorNota(notaMinima)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener estudiantes por nota: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Inserta un nuevo estudiante (con validaciones de negocio)
    ''' </summary>
    Public Function Insertar(estudiante As Estudiante) As Integer
        ValidarEstudiante(estudiante)

        Try
            Return _estudianteDAL.Insertar(estudiante)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al insertar el estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Actualiza la información de un estudiante existente (con validaciones)
    ''' </summary>
    Public Function Actualizar(estudiante As Estudiante) As Boolean
        If estudiante.IdEstudiante <= 0 Then
            Throw New ArgumentException("El ID del estudiante debe ser mayor a cero para actualizar")
        End If

        ValidarEstudiante(estudiante)

        Try
            Return _estudianteDAL.Actualizar(estudiante)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al actualizar el estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina (lógicamente) un estudiante
    ''' </summary>
    Public Function Eliminar(idEstudiante As Integer) As Boolean
        If idEstudiante <= 0 Then
            Throw New ArgumentException("El ID del estudiante debe ser mayor a cero")
        End If

        Try
            Return _estudianteDAL.Eliminar(idEstudiante)
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al eliminar el estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Valida los datos de un estudiante antes de insertarlo o actualizarlo
    ''' </summary>
    Private Sub ValidarEstudiante(estudiante As Estudiante)
        If String.IsNullOrWhiteSpace(estudiante.Nombres) Then
            Throw New ArgumentException("El nombre del estudiante es obligatorio")
        End If

        If String.IsNullOrWhiteSpace(estudiante.Apellidos) Then
            Throw New ArgumentException("El apellido del estudiante es obligatorio")
        End If

        If estudiante.Edad <= 0 OrElse estudiante.Edad > 120 Then
            Throw New ArgumentException("La edad del estudiante debe ser un valor válido (entre 1 y 120)")
        End If

        If estudiante.Sexo <> "M"c AndAlso estudiante.Sexo <> "F"c Then
            Throw New ArgumentException("El sexo debe ser 'M' o 'F'")
        End If

        If estudiante.IdCurso <= 0 Then
            Throw New ArgumentException("Debe seleccionar un curso válido")
        End If
    End Sub

End Class

