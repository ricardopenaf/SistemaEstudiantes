Imports System.Data.SqlClient

Imports System.Data

Public Class CursoDAL

    ''' <summary>
    ''' Obtiene todos los cursos de la base de datos ejecutando el stored procedure SP_ListaCursos.
    ''' </summary>
    Public Function ObtenerTodos() As List(Of Curso)
        Dim lista As New List(Of Curso)

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_ListaCursos", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    conexion.Open()
                    Using reader As SqlDataReader = comando.ExecuteReader()
                        While reader.Read()
                            lista.Add(MapearCurso(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error en la capa DAL al obtener los cursos: " & ex.Message, ex)
        End Try

        Return lista
    End Function

    Private Function MapearCurso(reader As SqlDataReader) As Curso

        Dim curso As New Curso With {
            .IdCurso = Convert.ToInt32(reader("IdCurso")),
            .Descripcion = reader("Descripcion").ToString()
        }

        Return curso

    End Function
End Class