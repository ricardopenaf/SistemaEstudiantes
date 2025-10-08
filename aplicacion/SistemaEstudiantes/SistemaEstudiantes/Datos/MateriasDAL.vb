Imports System.Data.SqlClient

Public Class MateriasDAL
    Public Function ObtenerTodos() As List(Of Materia)
        Dim lista As New List(Of Materia)

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_ListaMaterias", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    conexion.Open()
                    Using reader As SqlDataReader = comando.ExecuteReader()
                        While reader.Read()
                            lista.Add(MapearMaterias(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error en la capa DAL al obtener las materias: " & ex.Message, ex)
        End Try

        Return lista
    End Function
    Private Function MapearMaterias(reader As SqlDataReader) As Materia

        Dim materias As New Materia With {
            .IdMateria = Convert.ToInt32(reader("IdMateria")),
            .Descripcion = reader("Descripcion").ToString()
        }

        Return materias

    End Function
End Class
