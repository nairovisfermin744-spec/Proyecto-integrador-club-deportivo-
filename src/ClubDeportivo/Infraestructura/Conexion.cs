using MySql.Data.MySqlClient;

namespace ClubDeportivo.Infraestructura;

public static class Conexion
{
    // Ajustar servidor, usuario y contraseña según la instalación local.
    private const string Cadena =
        "Server=localhost;Port=3306;Database=club_deportivo;Uid=root;Pwd=;";

    public static MySqlConnection Crear()
    {
        return new MySqlConnection(Cadena);
    }
}
