using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.Sqlite;
using tl2_tp8_2025_NahuelCondori99.Interfaces;

public class ProductoRepository : IProductoRepository
{
    private string cadenaDeConexion = "Data Source = Tienda.db;";

    //Listar los productos
    public List<Productos> GetAll()
    {
        List<Productos> lista = new List<Productos>();
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "SELECT * FROM Productos";
            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            using (SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Productos p = new Productos
                    (
                        Convert.ToInt32(lector["IdProducto"]),
                        lector["Descripcion"].ToString(),
                        Convert.ToInt32(lector["Precio"])
                    );
                    lista.Add(p);
                }
            }
            return lista;
        }
    }

    //Obtener un producto por ID
    public Productos GetById(int id)
    {
        Productos p = null;
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "SELECT * FROM Productos WHERE idProducto=@id";
            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                using (SqliteDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        p = new Productos
                        (
                            Convert.ToInt32(lector["idProducto"]),
                            lector["descripcion"].ToString(),
                            Convert.ToInt32(lector["precio"])
                        );
                    }
                }
            }
        }
        return p;
    }


    //Requerido para el TP9
    public void Create(Productos p)
    {
        Alta(p);
    }
    //Crear un producto por ID
    public void Alta(Productos p)
    {
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "INSERT INTO Productos (descripcion, precio) VALUES (@descripcion, @precio)";
            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", p.Descripcion);
                comando.Parameters.AddWithValue("@precio", p.Precio);
                comando.ExecuteNonQuery();
            }
        }
    }

    //Modificar un producto existente
    public bool Modificar(int id, Productos p)
    {
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "UPDATE Productos SET descripcion = @descripcion, precio = @precio WHERE idProducto = @id";
            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", p.Descripcion);
                comando.Parameters.AddWithValue("@precio", p.Precio);
                comando.Parameters.AddWithValue("@id", id);

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
    
    //Eliminar un producto existente por ID
    public bool Eliminar(int id)
    {
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "DELETE FROM Productos WHERE idProducto = @id";
            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
}