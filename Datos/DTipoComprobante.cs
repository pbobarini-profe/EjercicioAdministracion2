using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DTipoComprobante
    {
        public static void Create(TipoComprobante tcomp)
        {
            string sql = @"INSERT INTO dbo.TipoComprobante (description) " +
                         "VALUES ( @description)";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = tcomp.description;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            string sql = @"DELETE FROM dbo.TipoComprobante WHERE id = @id";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void Update(TipoComprobante tcomp)
        {
            string sql = @"UPDATE dbo.TipoComprobante SET description = @description WHERE id = @id";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = tcomp.id;
                    cmd.Parameters.Add("@description", System.Data.SqlDbType.VarChar).Value = tcomp.description;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<TipoComprobante> GetAll()
        {
            List<TipoComprobante> listaTComp = new List<TipoComprobante>();
            string sql = @"SELECT * FROM dbo.TipoComprobante";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader red = cmd.ExecuteReader())
                    {
                        while (red.Read())
                        {
                            TipoComprobante tcomp = new TipoComprobante();
                            {
                                tcomp.id = red.GetInt32(0);
                                tcomp.description = red.GetString(1);
                            }
                            listaTComp.Add(tcomp);
                        }
                    }
                }
            }
            return listaTComp;
        }
    }
}
