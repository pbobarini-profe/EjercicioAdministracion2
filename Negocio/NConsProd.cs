using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NConsProd
    {
        public static void Create(ConsumoProduccion p)
        {
            try
            {
                DConsProd.Create(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Update(ConsumoProduccion p)
        {
            try
            {
                DConsProd.Update(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int id)
        {
            try
            {
                DConsProd.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<A_DtoConsProd> GetDto()
        {
            try
            {
                return DConsProd.GetDto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<A_DtoRConsProd> GetReporte()
        {
            try
            {
                return DConsProd.GetReporte();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}