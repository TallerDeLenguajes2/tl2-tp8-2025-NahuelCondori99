using System.Runtime.InteropServices;
using Microsoft.Data.Sqlite;

public class PresupuestosRepository
{
    private string cadenaDeConexion = "Data Source = Tienda.db;";

    //Crear un nuevo presupuesto
    public void crearPresupuesto(Presupuestos p)
    {
        using (var conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            string sql = "INSERT INTO Presupuestos (nombreDestinatario, fechaCreacion) VALUES (@nombre, @fecha);";

            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", p.NombreDestinatario);
                comando.Parameters.AddWithValue("@fecha", p.FechaCreacion);
                comando.ExecuteNonQuery();
            }

        }
    }

    //Listar todos los presupuestos
    public List<Presupuestos> GetAll()
    {
        List<Presupuestos> listaPresupuestos = new List<Presupuestos>();

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            string sql = "SELECT * FROM Presupuestos";

            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            using (SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    var p = new Presupuestos
                    (
                        Convert.ToInt32(lector["idPresupuesto"]),
                        lector["nombreDestinatario"].ToString(),
                        Convert.ToDateTime(lector["fechaCreacion"]),
                        new List<PresupuestoDetalles>()
                    );
                    listaPresupuestos.Add(p);
                }
            }
        }
        return listaPresupuestos;
    }

    //Obtener un presupuesto por ID
    public Presupuestos GetById(int id)
    {
        Presupuestos presupuestos = null;

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            string sql = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";

            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);

                using (SqliteDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        presupuestos = new Presupuestos
                        (
                            Convert.ToInt32(lector["idPresupuesto"]),
                            lector["nombreDestinatario"].ToString(),
                            Convert.ToDateTime(lector["fechaCreacion"]),
                            new List<PresupuestoDetalles>()
                        );
                    }
                }
            }
            if (presupuestos != null)
            {
                string sqlDetalle = @"
                    SELECT pd.cantidad, pr.idProducto, pr.descripcion, pr.precio FROM presupuestoDetalles pd
                    INNER JOIN Productos pr ON pd.idProducto = pr.idProducto
                    WHERE pd.idPresupuesto = @id";

                using (SqliteCommand comando = new SqliteCommand(sqlDetalle, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    using (SqliteDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var producto = new Productos(
                                Convert.ToInt32(lector["idProducto"]),
                                lector["descripcion"].ToString(),
                                Convert.ToInt32(lector["precio"])
                            );
                            var detalle = new PresupuestoDetalles(producto, Convert.ToInt32(lector["cantidad"]));
                            presupuestos.Detalles.Add(detalle);
                        }
                    }
                }
            }
        }
        return presupuestos;
    }

    //Agregar un producto y una cantidad a un presupuesto (recibe un Id)
    public void AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            string sql = "INSERT INTO PresupuestoDetalles (idPresupuesto, idProducto, cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";

            using (SqliteCommand comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.ExecuteNonQuery();
            }
        }
    }

    //Eliminar un presupuesto por ID
    public bool Eliminar(int id)
    {
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();

            string sqlDetalle = "DELETE FROM presupuestoDetalles WHERE idPresupuesto = @id";

            using (SqliteCommand comando = new SqliteCommand(sqlDetalle, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }

            string sqlPresupuesto = "DELETE FROM presupuestos WHERE idPresupuesto = @id";

            using (SqliteCommand comando = new SqliteCommand(sqlPresupuesto, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
}

