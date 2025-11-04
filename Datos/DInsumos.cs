using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DInsumos
    {
        public static void Create(Insumos ins)
        {
            string sql = @"
                    INSERT INTO Insumos (descripcion)
                    VALUES (@descripcion)
                ";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar).Value = ins.descripcion;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Insumos> GetAll()
        {
            List<Insumos> lista = new List<Insumos>();
            string sql = @"Select * from Insumos Order By descripcion";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Insumos ins = new Insumos
                            {
                                id = reader.GetInt32(0),
                                descripcion = reader.GetString(1)
                            };
                            lista.Add(ins);
                        }
                    }
                }
            }
            return lista;
        }

        public static void Delete(int id)
        {
            string sql = @"Delete from Insumos Where id = @id";
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

        public static void Update(Insumos ins)
        {
            string sql = @"
                    UPDATE Insumos SET
                    descripcion = @descripcion
                    WHERE id = @id
                ";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = ins.id;
                    cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar).Value = ins.descripcion;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}