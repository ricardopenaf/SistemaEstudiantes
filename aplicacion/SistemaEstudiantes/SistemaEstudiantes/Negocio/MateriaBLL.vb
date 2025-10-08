Public Class MateriaBLL
    Private ReadOnly _materiasDAL As New MateriasDAL()
    Public Function ObtenerTodos() As List(Of Materia)
        Try
            Return _materiasDAL.ObtenerTodos()
        Catch ex As Exception
            Throw New Exception("Error en la capa de negocio al obtener las materias: " & ex.Message, ex)
        End Try
    End Function
End Class
