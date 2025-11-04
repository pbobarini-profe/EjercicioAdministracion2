using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NProveedores
    {
        public static void Create(Proveedores p)
        {
            try
            {
                DProveedores.Create(p);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void UpDate(Proveedores p)
        {
            try
            {
                DProveedores.Update(p);
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
                DProveedores.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<Proveedores> Get()
        {
            return DProveedores.GetAll();

        }
    }
}
