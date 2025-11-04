using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DConsumosInsumos
    {
        public static List<ConsumosInsumos> GetAll()
        {
            List<ConsumosInsumos> lista = new List<ConsumosInsumos>();
            string sql = @"Select * from ConsumosInsumos Order By fecha DESC";
            List<Insumos> insumos = DInsumos.GetAll();
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            

                            int idInsumo = reader.GetInt32(3);

                            ConsumosInsumos c = new ConsumosInsumos
                            {
                                id = reader.GetInt32(0),
                                fecha = reader.GetDateTime(1),
                                cantidad = reader.GetDecimal(2),
                                insumo = insumos.FirstOrDefault(i => i.id == idInsumo)
                            };
                            lista.Add(c);
                        }
                    }
                }
            }
            return lista;
        }

        public static List<ConsumosInsumos> GetByInsumo(Insumos insumo)
        {
            List<ConsumosInsumos> lista = new List<ConsumosInsumos>();
            string sql = $@"Select * from ConsumosInsumos where idInsumo = {insumo.id} Order By fecha DESC";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ConsumosInsumos c = new ConsumosInsumos
                            {
                                id = reader.GetInt32(0),
                                fecha = reader.GetDateTime(1),
                                cantidad = reader.GetDecimal(2),
                                insumo = insumo
                            };
                            lista.Add(c);
                        }
                    }
                }
            }
            return lista;
        }

        public static void Create(ConsumosInsumos c)
        {
            string sql = @"
                    INSERT INTO ConsumosInsumos (fecha, cantidad, idInsumo)
                    VALUES (@fecha, @cantidad, @idInsumo)
                ";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = c.fecha;
                    cmd.Parameters.Add("@cantidad", System.Data.SqlDbType.Decimal).Value = c.cantidad;
                    cmd.Parameters.Add("@idInsumo", System.Data.SqlDbType.Int).Value = c.insumo.id;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            string sql = @"Delete from ConsumosInsumos Where id = @id";
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

        public static void Update(ConsumosInsumos c)
        {
            string sql = @"
                    UPDATE ConsumosInsumos SET
                    fecha = @fecha,
                    cantidad = @cantidad,
                    idInsumo = @idInsumo
                    WHERE id = @id
                ";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = c.id;
                    cmd.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = c.fecha;
                    cmd.Parameters.Add("@cantidad", System.Data.SqlDbType.Decimal).Value = c.cantidad;
                    cmd.Parameters.Add("@idInsumo", System.Data.SqlDbType.Int).Value = c.insumo.id;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}