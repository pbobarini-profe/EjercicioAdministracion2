using Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dDetalleCompra
    {
        private string connectionString = @"Data Source=WIN-1A306FL320H;Initial Catalog=Admnistracion;Integrated Security=True;";

        public void Create(DetalleCompras d)
        {
            string sql = @"
            INSERT INTO DetalleCompras (compra, cantidad, insumo, precioUnitario, usuario)
            VALUES (@compra, @cantidad, @insumo, @precioUnitario, @usuario)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@compra", SqlDbType.Int).Value = d.compra.id;
                cmd.Parameters.Add("@cantidad", SqlDbType.Decimal).Value = d.cantidad;
                cmd.Parameters.Add("@insumo", SqlDbType.Int).Value = d.insumo.id;
                cmd.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = d.precioUnitario;
                cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario.id;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<DetalleCompras> GetAll()
        {
            List<DetalleCompras> lista = new List<DetalleCompras>();

            string sql = @"
            SELECT DC.id, DC.cantidad, DC.precioUnitario,
                   C.id AS compra,
                   I.id AS insumo, I.descripcion,
                   U.id AS usuario, U.nombre
            FROM DetalleCompras DC
            INNER JOIN Compras C ON DC.compra = C.id
            INNER JOIN Insumos I ON DC.insumo = I.id
            INNER JOIN Usuarios U ON DC.usuario = U.id";

            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleCompras
                            {
                                id = dr.GetInt32(0),
                                cantidad = dr.GetDecimal(1),
                                precioUnitario = dr.GetDecimal(2),
                                compra = new Compras { id = dr.GetInt32(3) },
                                insumo = new Insumos { id = dr.GetInt32(4), descripcion = dr.GetString(5) },
                                usuario = new Usuarios { id = dr.GetInt32(6), nombre = dr.GetString(7) }
                            });
                        }
                    }
                }
                return lista;
            }
        }

        public void Update(DetalleCompras d)
        {
            string sql = @"
                            UPDATE DetalleCompras SET
                            compra = @compra,           
                            cantidad = @cantidad,
                            insumo = @insumo,          
                            precioUnitario = @precioUnitario,
                            usuario = @usuario         
                            WHERE id = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@cantidad", SqlDbType.Decimal).Value = d.cantidad;
                cmd.Parameters.Add("@insumo", SqlDbType.Int).Value = d.insumo.id;
                cmd.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = d.precioUnitario;
                cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario.id;
                cmd.Parameters.Add("@compra", SqlDbType.Int).Value = d.compra.id;
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = d.id;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM DetalleCompras WHERE id=@id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}