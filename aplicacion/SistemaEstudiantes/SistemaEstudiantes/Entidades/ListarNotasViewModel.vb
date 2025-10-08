Public Class ListarNotasViewModel
    Public Property IdEstudiante As Integer
    Public Property NombreCompletoEstudiante As String
    Public Property Calificaciones As List(Of Calificacion)

    Public Sub New()
        Me.Calificaciones = New List(Of Calificacion)()
    End Sub
End Class

