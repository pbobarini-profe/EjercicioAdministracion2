using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Negocio
{
    public class nDetalleCompra
    {
        private dDetalleCompra repo = new dDetalleCompra();

        public void Create(DetalleCompras d)
        {
            if (d.cantidad <= 0) throw new Exception("La cantidad debe ser > 0.");
            repo.Create(d);
        }

        

        public void Update(DetalleCompras d)
        {
            try
            {
                repo.Update(d);
            }
            catch (IOException e)
            {
                throw e;
            }

        }

        public void Delete(int id)
        {
            if (id <= 0) throw new Exception("ID inválido.");
            repo.Delete(id);
        }

        public static List<DetalleCompras> Get()
        {
            try
            {
                return dDetalleCompra.GetAll();
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public List<DetalleCompras> GetAll()
        {
            List<DetalleCompras> Lista = new List<DetalleCompras>();

            
            string sql = "SELECT * FROM DetalleCompras";

            
            using (SqlConnection cn = Db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetalleCompras c = new DetalleCompras
                            {
                                
                                id = reader.GetInt32(0),

                                // compra (Referencia a Compras)
                                compra = new Compras { id = reader.GetInt32(1) },

                                
                                cantidad = reader.GetDecimal(2),

                                // insumo (Referencia a Insumos)
                                insumo = new Insumos { id = reader.GetInt32(3) },

                                
                                precioUnitario = reader.GetDecimal(4),

                                // usuario (Referencia a Usuarios)
                                usuario = new Usuarios { id = reader.GetInt32(5) }
                            };

                           

                            Lista.Add(c);
                        }
                    }
                }
            }
            return Lista;
        }
    }
}

