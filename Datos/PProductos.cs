using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DProductos
    {
        private string connectionString = "Server=DESKTOP-52JVQL9;Database=ProduccionDB;Integrated Security=true;";


        public bool Insertar(Productos producto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Productos (descripcion) VALUES (@descripcion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@descripcion", producto.descripcion);

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


        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, descripcion FROM Productos";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Productos producto = new Productos
                        {
                            id = Convert.ToInt32(reader["id"]),
                            descripcion = reader["descripcion"].ToString()
                        };
                        lista.Add(producto);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                lista = new List<Productos>();
            }
            return lista;
        }


        public Productos BuscarPorId(int id)
        {
            Productos producto = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, descripcion FROM Productos WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        producto = new Productos
                        {
                            id = Convert.ToInt32(reader["id"]),
                            descripcion = reader["descripcion"].ToString()
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                producto = null;
            }
            return producto;
        }

        public bool Actualizar(Productos producto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Productos SET descripcion = @descripcion WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", producto.id);
                    cmd.Parameters.AddWithValue("@descripcion", producto.descripcion);

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
                    string query = "DELETE FROM Productos WHERE id = @id";
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