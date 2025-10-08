Public Class CursoBLL
    ''' <summary>
    ''' Obtiene todos los cursos 
    ''' </summary>
    ''' 
    Private ReadOnly _cursoDAL As New CursoDAL()
    Public Function ObtenerTodos() As List(Of Curso)
        Try
            Return _cursoDAL.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener los cursos: " & ex.Message, ex)
        End Try
    End Function
End Class
