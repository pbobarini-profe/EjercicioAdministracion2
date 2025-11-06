using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DProducto
    {
        public static List<Productos> GetAll()
        {
            List<Productos> lista = new List<Productos>();
            string sql = @"SELECT id, descripcion FROM Productos";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Productos p = new Productos
                            {
                                id = reader.GetInt32(0),
                                descripcion = reader.GetString(1)
                            };

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
