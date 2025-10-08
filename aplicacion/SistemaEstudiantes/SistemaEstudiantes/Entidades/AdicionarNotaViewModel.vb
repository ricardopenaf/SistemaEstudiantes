' En la carpeta Models
Public Class AdicionarNotaViewModel
    Public Property Estudiante As Estudiante
    Public Property Cursos As List(Of Curso)
    Public Property Materias As List(Of Materia)

    ' Constructor 
    Public Sub New()
        Me.Cursos = New List(Of Curso)()
        Me.Materias = New List(Of Materia)()
    End Sub
End Class