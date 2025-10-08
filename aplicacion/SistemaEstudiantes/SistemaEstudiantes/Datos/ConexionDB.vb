Imports System.Data.SqlClient

Public Class ConexionDB
    ' Nombre de la cadena de conexión en el web.config
    Private Const CONNECTION_STRING_NAME As String = "ConexionEstudiantes"

    ''' <summary>
    ''' Obtiene la cadena de conexión nombrada del archivo de configuración.
    ''' </summary>
    ''' <returns>La cadena de conexión como String.</returns>
    Private Shared Function ObtenerCadenaConexion() As String
        Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(CONNECTION_STRING_NAME)

        If settings Is Nothing OrElse String.IsNullOrEmpty(settings.ConnectionString) Then
            Throw New InvalidOperationException($"La cadena de conexión '{CONNECTION_STRING_NAME}' no se encontró o está vacía en el archivo de configuración.")
        End If

        Return settings.ConnectionString
    End Function

    ''' <summary>
    ''' Crea y devuelve un nuevo objeto SqlConnection utilizando la cadena de conexión del web.config.
    ''' </summary>
    ''' <returns>Un objeto SqlConnection listo para ser abierto.</returns>
    Public Shared Function ObtenerConexion() As SqlConnection
        Dim cadenaConexion As String = ObtenerCadenaConexion()

        Return New SqlConnection(cadenaConexion)
    End Function
End Class
