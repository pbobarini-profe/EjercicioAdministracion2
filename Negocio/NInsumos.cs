using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NInsumos
    {
        public static void Create(Insumos ins)
        {
            try
            {
                DInsumos.Create(ins);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Update(Insumos ins)
        {
            try
            {
                DInsumos.Update(ins);
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
                DInsumos.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Insumos> Get()
        {
            try
            {
                return DInsumos.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}