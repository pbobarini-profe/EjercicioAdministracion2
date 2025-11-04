using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NTipoComprobantes
    {
        public static void Create(TipoComprobante tcomp)
        {
            try
            {
                DTipoComprobante.Create(tcomp);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void UpDate(TipoComprobante tcomp)
        {
            try
            {
                DTipoComprobante.Update(tcomp);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Delete(int id)
        {
            try
            {
                DTipoComprobante.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<TipoComprobante> Get()
        {
            return DTipoComprobante.GetAll();

        }
    }
}
