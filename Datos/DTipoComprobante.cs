using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DTipoComprobante
    {
        public static List<TipoComprobante> GetAll()
        {
            List<TipoComprobante> lista = new List<TipoComprobante>();
            string sql = @"SELECT id, descripcion FROM TipoComprobante";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoComprobante t = new TipoComprobante
                            {
                                id = reader.GetInt32(0),
                                descripcion = reader.GetString(1)
                            };

                            lista.Add(t);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
