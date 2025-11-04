using Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DProveedores
    {
        public static void Create(Proveedores p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            p.identificacionTributaria = LimpiarCuit(p.identificacionTributaria);
            string sql = $@"INSERT INTO dbo.Proveedores (nombreCompleto, identificacionTributaria) 
                         VALUES (@nombreCompleto, @identificacionTributaria);
                         SELECT CAST(SCOPE_IDENTITY() AS int);";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@nombreCompleto", System.Data.SqlDbType.VarChar).Value = p.nombreCompleto;
                    cmd.Parameters.Add("@identificacionTributaria", System.Data.SqlDbType.VarChar).Value = p.identificacionTributaria;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Proveedores> GetAll()
        {
            List<Proveedores> listaProv = new List<Proveedores>();
            string sql = @"SELECT * FROM dbo.Proveedores";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader red = cmd.ExecuteReader())
                    {
                        while (red.Read())
                        {
                            listaProv.Add(new Proveedores
                            {
                                id = red.GetInt32(0),
                                nombreCompleto = red.GetString(1),
                                identificacionTributaria = red.GetString(2),
                            });
                        }
                    }
                }
                return listaProv;
            }
        }

        public static void Delete(int id)
        {
            string sql = @"DELETE FROM dbo.Proveedores WHERE id = @id";
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

        public static void Update(Proveedores p)
        {
            p.identificacionTributaria = LimpiarCuit(p.identificacionTributaria);
            string sql = @"UPDATE dbo.Proveedores SET nombreCompleto = @nombreCompleto, identificacionTributaria = @identificacionTributaria WHERE id = @id";
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = p.id;
                    cmd.Parameters.Add("@nombreCompleto", System.Data.SqlDbType.VarChar).Value = p.nombreCompleto;
                    cmd.Parameters.Add("@identificacionTributaria", System.Data.SqlDbType.VarChar).Value = p.identificacionTributaria;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static string LimpiarCuit(string cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit))
                throw new ArgumentException("El campo CUIT no puede estar vacío.");

            string limpio = new string(cuit.Where(char.IsDigit).ToArray());

            if (limpio.Length != 11)
                throw new ArgumentException("El CUIT debe tener exactamente 11 dígitos numéricos.");

            return limpio;
        }
    }
}
