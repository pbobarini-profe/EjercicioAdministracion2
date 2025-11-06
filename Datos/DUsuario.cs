using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DUsuario
    {
        public static List<Usuarios> GetAll()
        {
            List<Usuarios> lista = new List<Usuarios>();
            string sql = @"SELECT id, nombre, apellido, nombreUsuario, contrasena, mail, fechaAlta FROM Usuarios";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuarios u = new Usuarios
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                apellido = reader.GetString(2),
                                nombreUsuario = reader.GetString(3),
                                contrasena = reader.GetString(4),
                                mail = reader.GetString(5),
                                fechaAlta = reader.GetDateTime(6)
                            };

                            lista.Add(u);
                        }
                    }
                }
            }

            return lista;
        }
        public static Usuarios GetById(int id)
        {
            Usuarios usuario = null;
            string sql = "SELECT id, nombre, usuario, contrasena FROM Usuarios WHERE id = @id";

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
                            usuario = new Usuarios
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                contrasena = reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return usuario;
        }
    }
}
