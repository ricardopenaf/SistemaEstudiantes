Imports System.Data.SqlClient

Public Class EstudianteDAL
    ''' <summary>
    ''' Obtiene todos los estudiantes activos
    ''' </summary>
    Public Function ObtenerTodos() As List(Of Estudiante)
        Dim lista As New List(Of Estudiante)

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_Seleccionar", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    conexion.Open()
                    Using reader As SqlDataReader = comando.ExecuteReader()
                        While reader.Read()
                            lista.Add(MapearEstudiante(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener estudiantes: " & ex.Message, ex)
        End Try

        Return lista
    End Function

    ''' <summary>
    ''' Obtiene un estudiante por su ID
    ''' </summary>
    Public Function ObtenerPorId(idEstudiante As Integer) As Estudiante
        Dim estudiante As Estudiante = Nothing

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_SeleccionarPorId", conexion)
                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.AddWithValue("@IdEstudiante", idEstudiante)

                    conexion.Open()
                    Using reader As SqlDataReader = comando.ExecuteReader()
                        If reader.Read() Then
                            estudiante = MapearEstudiante(reader)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener estudiante: " & ex.Message, ex)
        End Try

        Return estudiante
    End Function

    ''' <summary>
    ''' Obtiene estudiantes con nota promedio mayor o igual a la especificada
    ''' </summary>
    Public Function ObtenerPorNota(notaMinima As Decimal) As List(Of Estudiante)
        Dim lista As New List(Of Estudiante)

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_SeleccionarPorNota", conexion)
                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.AddWithValue("@NotaMinima", notaMinima)

                    conexion.Open()
                    Using reader As SqlDataReader = comando.ExecuteReader()
                        While reader.Read()
                            ' Creamos el objeto Curso asociado
                            Dim cursoDelEstudiante As New Curso With {
                            .IdCurso = Convert.ToInt32(reader("IdCurso")),
                            .NombreCurso = reader("NombreCurso").ToString(),
                            .Nivel = reader("Nivel").ToString()
                        }

                            ' Creamos el objeto Estudiante
                            Dim est As New Estudiante With {
                            .IdEstudiante = Convert.ToInt32(reader("IdEstudiante")),
                            .Nombres = reader("Nombres").ToString(),
                            .Apellidos = reader("Apellidos").ToString(),
                            .Edad = Convert.ToInt32(reader("Edad")),
                            .Sexo = Convert.ToChar(reader("Sexo")),
                            .CursoAsociado = cursoDelEstudiante
                        }

                            lista.Add(est)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener estudiantes por nota: " & ex.Message, ex)
        End Try

        Return lista
    End Function


    ''' <summary>
    ''' Inserta un nuevo estudiante
    ''' </summary>
    Public Function Insertar(estudiante As Estudiante) As Integer
        Dim idGenerado As Integer = 0

        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_Insertar", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.AddWithValue("@Nombres", estudiante.Nombres)
                    comando.Parameters.AddWithValue("@Apellidos", estudiante.Apellidos)
                    comando.Parameters.AddWithValue("@Edad", estudiante.Edad)
                    comando.Parameters.AddWithValue("@Sexo", estudiante.Sexo)
                    comando.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento)
                    comando.Parameters.AddWithValue("@Direccion", If(String.IsNullOrEmpty(estudiante.Direccion), DBNull.Value, estudiante.Direccion))
                    comando.Parameters.AddWithValue("@Telefono", If(String.IsNullOrEmpty(estudiante.Telefono), DBNull.Value, estudiante.Telefono))
                    comando.Parameters.AddWithValue("@Email", If(String.IsNullOrEmpty(estudiante.Email), DBNull.Value, estudiante.Email))
                    comando.Parameters.AddWithValue("@IdCurso", estudiante.IdCurso)

                    Dim paramSalida As New SqlParameter("@IdEstudiante", SqlDbType.Int)
                    paramSalida.Direction = ParameterDirection.Output
                    comando.Parameters.Add(paramSalida)

                    conexion.Open()
                    comando.ExecuteNonQuery()

                    idGenerado = Convert.ToInt32(paramSalida.Value)
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al insertar estudiante: " & ex.Message, ex)
        End Try

        Return idGenerado
    End Function

    ''' <summary>
    ''' Actualiza un estudiante existente
    ''' </summary>
    Public Function Actualizar(estudiante As Estudiante) As Boolean
        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_Actualizar", conexion)
                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.AddWithValue("@IdEstudiante", estudiante.IdEstudiante)
                    comando.Parameters.AddWithValue("@Nombres", estudiante.Nombres)
                    comando.Parameters.AddWithValue("@Apellidos", estudiante.Apellidos)
                    comando.Parameters.AddWithValue("@Edad", estudiante.Edad)
                    comando.Parameters.AddWithValue("@Sexo", estudiante.Sexo)
                    comando.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento)
                    comando.Parameters.AddWithValue("@Direccion", If(String.IsNullOrEmpty(estudiante.Direccion), DBNull.Value, estudiante.Direccion))
                    comando.Parameters.AddWithValue("@Telefono", If(String.IsNullOrEmpty(estudiante.Telefono), DBNull.Value, estudiante.Telefono))
                    comando.Parameters.AddWithValue("@Email", If(String.IsNullOrEmpty(estudiante.Email), DBNull.Value, estudiante.Email))
                    comando.Parameters.AddWithValue("@IdCurso", estudiante.IdCurso)
                    comando.Parameters.AddWithValue("@Activo", estudiante.Activo)

                    conexion.Open()
                    Dim filasAfectadas As Integer = comando.ExecuteNonQuery()

                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al actualizar estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Elimina (lógicamente) un estudiante
    ''' </summary>
    Public Function Eliminar(idEstudiante As Integer) As Boolean
        Try
            Using conexion As SqlConnection = ConexionDB.ObtenerConexion()
                Using comando As New SqlCommand("SP_Estudiante_Eliminar", conexion)
                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.AddWithValue("@IdEstudiante", idEstudiante)

                    conexion.Open()
                    Dim filasAfectadas As Integer = comando.ExecuteNonQuery()

                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar estudiante: " & ex.Message, ex)
        End Try
    End Function

    ''' <summary>
    ''' Mapea un SqlDataReader a un objeto Estudiante
    ''' </summary>
    Private Function MapearEstudiante(reader As SqlDataReader) As Estudiante
        ' Creamos y mapeamos el objeto Curso
        Dim cursoDelEstudiante As New Curso With {
            .IdCurso = Convert.ToInt32(reader("IdCurso")),
            .NombreCurso = reader("NombreCurso").ToString(),
            .Nivel = reader("Nivel").ToString()
        }

        ' Mapeamos el objeto Estudiante, incluyendo el Curso
        Return New Estudiante With {
            .IdEstudiante = Convert.ToInt32(reader("IdEstudiante")),
            .Nombres = reader("Nombres").ToString(),
            .Apellidos = reader("Apellidos").ToString(),
            .Edad = Convert.ToInt32(reader("Edad")),
            .Sexo = Convert.ToChar(reader("Sexo")),
            .FechaNacimiento = Convert.ToDateTime(reader("FechaNacimiento")),
            .Direccion = If(IsDBNull(reader("Direccion")), String.Empty, reader("Direccion").ToString()),
            .Telefono = If(IsDBNull(reader("Telefono")), String.Empty, reader("Telefono").ToString()),
            .Email = If(IsDBNull(reader("Email")), String.Empty, reader("Email").ToString()),
            .IdCurso = Convert.ToInt32(reader("IdCurso")),
            .CursoAsociado = cursoDelEstudiante,
            .FechaRegistro = Convert.ToDateTime(reader("FechaRegistro")),
            .Activo = Convert.ToBoolean(reader("Activo"))
        }

    End Function
End Class
