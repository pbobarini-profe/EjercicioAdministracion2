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
        public static List<A_DtoConsProd> GetDto()
        {
            List<A_DtoConsProd> lista = new List<A_DtoConsProd>();
            string sql = @"
                SELECT 
                    cp.id,
                    pp.id AS ProductoId,
                    p.descripcion AS Producto,
                    ci.id AS InsumoId,
                    i.descripcion AS Insumo,
                    cp.observaciones
                FROM ConsumoProduccion cp
                INNER JOIN ProduccionProductos pp ON cp.produccionProductoId = pp.id
                INNER JOIN Productos p ON pp.productoId = p.id
                INNER JOIN ConsumosInsumos ci ON cp.consumoInsumoId = ci.id
                INNER JOIN Insumos i ON ci.insumoId = i.id";

            using (SqlConnection cn = Db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        A_DtoConsProd dto = new A_DtoConsProd
                        {
                            Id = reader.GetInt32(0),
                            ProductoId = reader.GetInt32(1),
                            Producto = reader.GetString(2),
                            InsumoId = reader.GetInt32(3),
                            Insumo = reader.GetString(4),
                            Observaciones = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        };
                        lista.Add(dto);
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
        public static List<A_DtoRConsProd> GetReporte()
        {
            List<A_DtoRConsProd> lista = new List<A_DtoRConsProd>();
            string sql = @"
                    SELECT 
                        pp.id AS Id,
                        pp.productoId AS ProductoId,
                        p.descripcion AS Producto,
                        pp.cantidad AS CantidadProducida,
                        ci.id AS InsumoId,
                        i.descripcion AS Insumo,
                        ci.cantidad AS CantidadConsumida,
                        ISNULL(cp.observaciones, 'Sin observaciones') AS Observaciones
                    FROM ConsumoProduccion cp
                    INNER JOIN ProduccionProductos pp ON cp.produccionProductoId = pp.id
                    INNER JOIN Productos p ON pp.productoId = p.id
                    INNER JOIN ConsumosInsumos ci ON cp.consumoInsumoId = ci.id
                    INNER JOIN Insumos i ON ci.insumoId = i.id
        ";
            using (SqlConnection cn = Db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new A_DtoRConsProd
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ProductoId = Convert.ToInt32(dr["ProductoId"]),
                        Producto = dr["Producto"].ToString(),
                        CantidadProducida = Convert.ToDecimal(dr["CantidadProducida"]),
                        InsumoId = Convert.ToInt32(dr["InsumoId"]),
                        Insumo = dr["Insumo"].ToString(),
                        CantidadConsumida = Convert.ToDecimal(dr["CantidadConsumida"]),
                        Observaciones = dr["Observaciones"].ToString()
                    });
                }
                dr.Close();
            }

            return lista;
        }

    }
}
