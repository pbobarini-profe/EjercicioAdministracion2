using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelos;

namespace Datos
{
    public class DCliente
    {
        public static List<Clientes> GetAll()
        {
            List<Clientes> lista = new List<Clientes>();
            string sql = @"SELECT id, nombreCompleto, identificacionTributaria FROM Clientes";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clientes c = new Clientes
                            {
                                id = reader.GetInt32(0),
                                nombreCompleto = reader.GetString(1),
                                identificacionTributaria = reader.GetString(2)
                            };

                            lista.Add(c);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
