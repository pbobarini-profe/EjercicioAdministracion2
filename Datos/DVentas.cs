using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Modelos;

namespace Datos
{
    public class DVentas
    {
        public static List<Ventas> GetAll()
        {
            List<Ventas> lista = new List<Ventas>();
            string sql = @"SELECT id, fecha, clienteId, tipoComprobanteId, puntoVenta, numero, usuarioId FROM Ventas";

            List<Clientes> clientes = DCliente.GetAll();
            List<TipoComprobante> tiposComprobante = DTipoComprobante.GetAll();
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
                            int idCliente = reader.GetInt32(2);
                            int idTipoComprobante = reader.GetInt32(3);
                            int idUsuario = reader.GetInt32(6);

                            Ventas venta = new Ventas
                            {
                                id = reader.GetInt32(0),
                                fecha = reader.GetDateTime(1),
                                cliente = clientes.FirstOrDefault(c => c.id == idCliente),
                                tipoComprobante = tiposComprobante.FirstOrDefault(tc => tc.id == idTipoComprobante),
                                puntoVenta = reader.GetString(4),
                                numero = reader.GetString(5),
                                usuario = usuarios.FirstOrDefault(u => u.id == idUsuario)
                            };

                            lista.Add(venta);
                        }
                    }
                }
            }

            return lista;
        }
        public static Ventas GetById(int id)
        {
            Ventas venta = null;
            string sql = @"SELECT id, fecha, clienteId, tipoComprobanteId, puntoVenta, numero, usuarioId 
                   FROM Ventas WHERE id = @id";

            List<Clientes> clientes = DCliente.GetAll();
            List<TipoComprobante> tiposComprobante = DTipoComprobante.GetAll();
            List<Usuarios> usuarios = DUsuario.GetAll();

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idCliente = reader.GetInt32(2);
                            int idTipoComprobante = reader.GetInt32(3);
                            int idUsuario = reader.GetInt32(6);

                            venta = new Ventas
                            {
                                id = reader.GetInt32(0),
                                fecha = reader.GetDateTime(1),
                                cliente = clientes.FirstOrDefault(c => c.id == idCliente),
                                tipoComprobante = tiposComprobante.FirstOrDefault(tc => tc.id == idTipoComprobante),
                                puntoVenta = reader.GetString(4),
                                numero = reader.GetString(5),
                                usuario = usuarios.FirstOrDefault(u => u.id == idUsuario)
                            };
                        }
                    }
                }
            }

            return venta;
        }

    }
}
