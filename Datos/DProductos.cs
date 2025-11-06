using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos
{
    public class DProductos
    {
        private string connectionString = "Data Source=.DESKTOP-52JVQL9\\R;Initial Catalog=EjercicioAdministracion;Integrated Security=True";


        public bool Insertar(Producto producto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Productos (Nombre, Descripcion, PrecioUnitario, Stock, StockMinimo, Activo, FechaCreacion) 
                                   VALUES (@Nombre, @Descripcion, @PrecioUnitario, @Stock, @StockMinimo, @Activo, @FechaCreacion)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
                        cmd.Parameters.AddWithValue("@Activo", producto.Activo);
                        cmd.Parameters.AddWithValue("@FechaCreacion", producto.FechaCreacion);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar producto: " + ex.Message);
            }
        }


        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Productos ORDER BY IdProducto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new Producto
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    StockMinimo = Convert.ToInt32(reader["StockMinimo"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }


        public Producto ObtenerPorId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Productos WHERE IdProducto = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Producto
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    StockMinimo = Convert.ToInt32(reader["StockMinimo"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }

            return null;
        }

        public bool Actualizar(Producto producto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Productos 
                                   SET Nombre = @Nombre, 
                                       Descripcion = @Descripcion, 
                                       PrecioUnitario = @PrecioUnitario, 
                                       Stock = @Stock, 
                                       StockMinimo = @StockMinimo, 
                                       Activo = @Activo 
                                   WHERE IdProducto = @IdProducto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
                        cmd.Parameters.AddWithValue("@Activo", producto.Activo);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }


        public bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Productos WHERE IdProducto = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }


        public List<Producto> BuscarPorNombre(string nombre)
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Productos WHERE Nombre LIKE @Nombre ORDER BY Nombre";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new Producto
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    StockMinimo = Convert.ToInt32(reader["StockMinimo"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos: " + ex.Message);
            }

            return productos;
        }
    }
}