using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Modelos;

namespace Datos
{
    public class DDetalleVenta
    {
        public static List<DetalleVentas> GetAll()
        {
            List<DetalleVentas> lista = new List<DetalleVentas>();
            string sql = @"SELECT * FROM DetalleVentas";

            List<Ventas> ventas = DVentas.GetAll();
            List<Productos> productos = DProducto.GetAll();
            List<Usuarios> usuarios = DUsuario.GetAll();

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            

                            int idVenta = reader.GetInt32(1);
                            int idProducto = reader.GetInt32(3);
                            int idUsuario = reader.GetInt32(5);

                            DetalleVentas detalle = new DetalleVentas
                            {
                                id = reader.GetInt32(0),
                                venta = ventas.FirstOrDefault(v => v.id == idVenta),
                                cantidad = reader.GetDecimal(2),
                                producto = productos.FirstOrDefault(p => p.id == idProducto),
                                precioUnitario = reader.GetDecimal(4),
                                usuario = usuarios.FirstOrDefault(u => u.id == idUsuario)
                            };

                            lista.Add(detalle);
                        }
                    }
                }
            }

            return lista;
        }
        public static List<DetalleVentas> GetById(Ventas venta)
        {
            List<DetalleVentas> lista = new List<DetalleVentas>();
            try
            {
                string sql = $@"SELECT * FROM DetalleVentas WHERE ventaId = '{venta.id}'";

                List<Productos> productos = DProducto.GetAll();
                List<Usuarios> usuarios = DUsuario.GetAll();

                using (SqlConnection cn = Db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productoId = reader.GetInt32(3);
                                int usuarioId = reader.GetInt32(5);

                                DetalleVentas d = new DetalleVentas
                                {
                                    id = reader.GetInt32(0),
                                    venta = venta,
                                    cantidad = reader.GetDecimal(2),
                                    producto = productos.FirstOrDefault(p => p.id == productoId),
                                    precioUnitario = reader.GetDecimal(4),
                                    usuario = usuarios.FirstOrDefault(u => u.id == usuarioId)
                                };

                                lista.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception e) { 
             throw e;
            }

            return lista;
        }
        public static void Create(DetalleVentas detalle)
        {
            string sql = @"INSERT INTO DetalleVentas 
                           (ventaId, cantidad, productoId, precioUnitario, usuarioId)
                           VALUES (@ventaId, @cantidad, @productoId, @precioUnitario, @usuarioId)";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@ventaId", detalle.venta.id);
                    cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                    cmd.Parameters.AddWithValue("@productoId", detalle.producto.id);
                    cmd.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                    cmd.Parameters.AddWithValue("@usuarioId", detalle.usuario.id);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void Update(DetalleVentas detalle)
        {
            string sql = @"UPDATE DetalleVentas 
                   SET cantidad = @cantidad, 
                       productoId = @productoId, 
                       precioUnitario = @precioUnitario,
                       usuarioId = @usuarioId
                   WHERE id = @id";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                    cmd.Parameters.AddWithValue("@productoId", detalle.producto.id);
                    cmd.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                    cmd.Parameters.AddWithValue("@usuarioId", detalle.usuario.id);
                    cmd.Parameters.AddWithValue("@id", detalle.id);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            string sql = @"DELETE FROM DetalleVentas WHERE id = @id";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
