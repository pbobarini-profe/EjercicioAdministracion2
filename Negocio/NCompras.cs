using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NCompras
    {
        public static void Create(Compras c)
        {
            try
            {
                DCompras.Create(c);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Update(Compras c, int id)
        {
            try
            {
                DCompras.Update(c, id);
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
                DCompras.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Compras> Get()
        {
            try
            {
                return DCompras.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
