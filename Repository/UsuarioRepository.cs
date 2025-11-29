using Microsoft.Data.Sqlite;
using tl2_tp8_2025_NahuelCondori99.Interfaces;
using tl2_tp8_2025_NahuelCondori99.Models;

namespace tl2_tp8_2025_NahuelCondori99.Repository
{
    public class UsuarioRepository : IUserRepository
    {
        private readonly string cadena = "Data Source = Tienda.db;";
        public Usuario GetUsuario(string usuario, string contrasena)
        {
            Usuario user = null;
            const string sql = @"
                SELECT Id, Nombre, User, Pass, Rol
                FROM Usuarios
                WHERE User = @Usuario AND Pass = @Contrasena";

            using var conexion = new SqliteConnection(cadena);
            conexion.Open();

            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@Usuario", usuario);
            comando.Parameters.AddWithValue("@Contrasena", contrasena);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                user = new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    User = reader.GetString(2),
                    Pass = reader.GetString(3),
                    Rol = reader.GetString(4)
                };
            }
            return user;
        }
    }
}