Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Capa de Acceso a Datos para Calificaciones
''' </summary>
Public Class CalificacionDAL

    ''' <summary>
    ''' Obtiene todas las calificaciones registradas
    ''' </summary>
    Public Function ObtenerTodos() As List(Of Calificacion)
        Dim lista As New List(Of Calificacion)

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
            Using comando As New SqlCommand("SP_Calificacion_SeleccionarTodos", conexion)
                comando.CommandType = CommandType.StoredProcedure
                conexion.Open()

                Using reader As SqlDataReader = comando.ExecuteReader()
                    While reader.Read()
                        lista.Add(MapearCalificacion(reader))
                    End While
                End Using
            End Using
        End Using

        Return lista
    End Function

    ''' <summary>
    ''' Obtiene una calificación por su ID
    ''' </summary>
    Public Function ObtenerPorId(idCalificacion As Integer) As Calificacion
        Dim calificacion As Calificacion = Nothing

        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
            Using comando As New SqlCommand("SP_Calificacion_SeleccionarPorId", conexion)
                comando.CommandType = CommandType.StoredProcedure
                comando.Parameters.AddWithValue("@IdCalificacion", idCalificacion)
                conexion.Open()

                Using reader As SqlDataReader = comando.ExecuteReader()
                    If reader.Read() Then
                        calificacion = MapearCalificacion(reader)
                    End If
                End Using
            End Using
        End Using

        Return calificacion
    End Function

    ''' <summary>
    ''' Obtiene todas las calificaciones de un estudiante específico
    ''' </summary>
    Public Function ObtenerPorEstudiante(idEstudiante As Integer) As List(Of CalificacionDetalle)
        Dim lista As New List(Of CalificacionDetalle)
        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
            Using comando As New SqlCommand("SP_Calificacion_SeleccionarPorEstudiante", conexion)
                comando.CommandType = CommandType.StoredProcedure
                comando.Parameters.AddWithValue("@IdEstudiante", idEstudiante)
                conexion.Open()
                Using reader As SqlDataReader = comando.ExecuteReader()
                    While reader.Read()
                        Dim cal As New CalificacionDetalle With {
                        .IdCalificacion = CInt(reader("IdCalificacion")),
                        .IdEstudiante = CInt(reader("IdEstudiante")),
                        .IdMateria = CInt(reader("IdMateria")),
                        .DescripcionMateria = reader("DescripcionMateria").ToString(),
                        .Periodo = reader("Periodo").ToString(),
                        .Nota = CDec(reader("Nota")),
                        .Observaciones = If(IsDBNull(reader("Observaciones")), "", reader("Observaciones").ToString()),
                        .FechaCalificacion = CDate(reader("FechaCalificacion"))
                    }
                        lista.Add(cal)
                    End While
                End Using
            End Using
        End Using
        Return lista
    End Function


    ''' <summary>
    ''' Inserta una nueva calificación en la base de datos, incluyendo Periodo y Observaciones.
    ''' </summary>
    Public Function Insertar(calificacion As Calificacion) As Integer
        ' Nota: La clase Calificacion ya tiene las propiedades Periodo (String) y Observaciones (String).
        Dim resultado As Integer = 0

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Calificacion_Insertar", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.AddWithValue("@IdEstudiante", calificacion.IdEstudiante)
                    comando.Parameters.AddWithValue("@IdMateria", calificacion.IdMateria)
                    comando.Parameters.AddWithValue("@Periodo", calificacion.Periodo)
                    comando.Parameters.AddWithValue("@Nota", calificacion.Nota)
                    comando.Parameters.AddWithValue("@Observaciones", calificacion.Observaciones)

                    Dim paramSalida As New SqlParameter("@IdNotas", SqlDbType.Int)
                    paramSalida.Direction = ParameterDirection.Output
                    comando.Parameters.Add(paramSalida)

                    conexion.Open()
                    comando.ExecuteNonQuery()

                    resultado = Convert.ToInt32(paramSalida.Value)
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error al insertar la calificación: " & ex.Message, ex)
        End Try
        Return resultado
    End Function

    ''' <summary>
    ''' Actualiza una calificación existente
    ''' </summary>
    Public Function Actualizar(calificacion As Calificacion) As Boolean
        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
            Using comando As New SqlCommand("SP_Calificacion_Actualizar", conexion)
                comando.CommandType = CommandType.StoredProcedure
                comando.Parameters.AddWithValue("@IdCalificacion", calificacion.IdCalificacion)
                comando.Parameters.AddWithValue("@IdEstudiante", calificacion.IdEstudiante)
                comando.Parameters.AddWithValue("@IdMateria", calificacion.IdMateria)
                comando.Parameters.AddWithValue("@Periodo", calificacion.Periodo)
                comando.Parameters.AddWithValue("@Nota", calificacion.Nota)

                conexion.Open()
                Dim filasAfectadas As Integer = comando.ExecuteNonQuery()
                Return filasAfectadas > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Elimina una calificación por su ID
    ''' </summary>
    Public Function Eliminar(idCalificacion As Integer) As Boolean
        Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
            Using comando As New SqlCommand("SP_Calificacion_Eliminar", conexion)
                comando.CommandType = CommandType.StoredProcedure
                comando.Parameters.AddWithValue("@IdCalificacion", idCalificacion)

                conexion.Open()
                Dim filasAfectadas As Integer = comando.ExecuteNonQuery()
                Return filasAfectadas > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Método privado para mapear los datos del lector a un objeto Calificacion
    ''' </summary>
    Private Function MapearCalificacion(reader As SqlDataReader) As Calificacion
        Dim cal As New Calificacion With {
            .IdCalificacion = Convert.ToInt32(reader("IdCalificacion")),
            .IdEstudiante = Convert.ToInt32(reader("IdEstudiante")),
            .IdMateria = Convert.ToInt32(reader("IdMateria")),
            .Periodo = reader("Periodo").ToString(),
            .Nota = Convert.ToDecimal(reader("Nota"))
        }



        Return cal
    End Function

End Class

