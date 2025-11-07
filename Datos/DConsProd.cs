using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DConsProd
    {
        public static void Create(ConsumoProduccion p)
        {
            string sql = @"
                INSERT INTO ConsumoProduccion (consumoInsumoId, produccionProductoId, observaciones)
                VALUES (@consumoInsumoId, @produccionProductoId, @observaciones)
            ";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@consumoInsumoId", SqlDbType.Int).Value = p.consumoInsumo.id;
                    cmd.Parameters.Add("@produccionProductoId", SqlDbType.Int).Value = p.produccionProducto.id;
                    cmd.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = p.observaciones;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ConsumoProduccion> GetAll() 
        { 
            List<ConsumoProduccion> lista = new List<ConsumoProduccion>(); 
            string sql = @"SELECT * FROM ConsumoProduccion"; 
            using (SqlConnection cn = Db.GetConnection()) 
            { 
                using (SqlCommand cmd = new SqlCommand(sql, cn)) 
                { 
                    cn.Open(); 
                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    { 
                        while (reader.Read()) 
                        { 
                            ConsumoProduccion p = new ConsumoProduccion 
                            { 
                                id = reader.GetInt32(0), 
                                consumoInsumo = new ConsumosInsumos 
                                { 
                                    id = reader.GetInt32(1) 
                                }, 
                                produccionProducto = new ProduccionProductos 
                                { 
                                    id = reader.GetInt32(2) 
                                }, 
                                observaciones = reader.IsDBNull(3) ? "" : reader.GetString(3) 
                            }; 
                            lista.Add(p); 
                        } 
                    } 
                } 
            } 
            return lista; 
        }

        public static void Delete(int id)
        {
            string sql = @"DELETE FROM ConsumoProduccion WHERE id = @id";

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
        public static void Update(ConsumoProduccion p)
        {
            string sql = @"
                UPDATE ConsumoProduccion SET 
                consumoInsumoId = @consumoInsumoId,
                produccionProductoId = @produccionProductoId,
                observaciones = @observaciones
                WHERE id = @id
            ";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = p.id;
                    cmd.Parameters.Add("@consumoInsumoId", System.Data.SqlDbType.Int).Value = p.consumoInsumo.id;
                    cmd.Parameters.Add("@produccionProductoId", System.Data.SqlDbType.Int).Value = p.produccionProducto.id;
                    cmd.Parameters.Add("@observaciones", System.Data.SqlDbType.NVarChar).Value = p.observaciones;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
