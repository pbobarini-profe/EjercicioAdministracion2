using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DProduccionProductos
    {
        private string connectionString = "Server=DESKTOP-52JVQL9;Database=ProduccionDB;Integrated Security=true;";


        public bool Insertar(ProduccionProductos produccion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ProduccionProductos (fecha, producto_id, cantidad) VALUES (@fecha, @producto_id, @cantidad)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fecha", produccion.fecha);
                    cmd.Parameters.AddWithValue("@producto_id", produccion.producto.id);
                    cmd.Parameters.AddWithValue("@cantidad", produccion.cantidad);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public List<ProduccionProductos> Listar()
        {
            List<ProduccionProductos> lista = new List<ProduccionProductos>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT pp.id, pp.fecha, pp.cantidad, pp.producto_id, p.descripcion 
                                   FROM ProduccionProductos pp 
                                   INNER JOIN Productos p ON pp.producto_id = p.id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProduccionProductos produccion = new ProduccionProductos
                        {
                            id = Convert.ToInt32(reader["id"]),
                            fecha = Convert.ToDateTime(reader["fecha"]),
                            cantidad = Convert.ToDecimal(reader["cantidad"]),
                            producto = new Productos
                            {
                                id = Convert.ToInt32(reader["producto_id"]),
                                descripcion = reader["descripcion"].ToString()
                            }
                        };
                        lista.Add(produccion);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                lista = new List<ProduccionProductos>();
            }
            return lista;
        }

        
        public ProduccionProductos BuscarPorId(int id)
        {
            ProduccionProductos produccion = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT pp.id, pp.fecha, pp.cantidad, pp.producto_id, p.descripcion 
                                   FROM ProduccionProductos pp 
                                   INNER JOIN Productos p ON pp.producto_id = p.id 
                                   WHERE pp.id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        produccion = new ProduccionProductos
                        {
                            id = Convert.ToInt32(reader["id"]),
                            fecha = Convert.ToDateTime(reader["fecha"]),
                            cantidad = Convert.ToDecimal(reader["cantidad"]),
                            producto = new Productos
                            {
                                id = Convert.ToInt32(reader["producto_id"]),
                                descripcion = reader["descripcion"].ToString()
                            }
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                produccion = null;
            }
            return produccion;
        }


        public bool Actualizar(ProduccionProductos produccion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ProduccionProductos SET fecha = @fecha, producto_id = @producto_id, cantidad = @cantidad WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", produccion.id);
                    cmd.Parameters.AddWithValue("@fecha", produccion.fecha);
                    cmd.Parameters.AddWithValue("@producto_id", produccion.producto.id);
                    cmd.Parameters.AddWithValue("@cantidad", produccion.cantidad);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ProduccionProductos WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}