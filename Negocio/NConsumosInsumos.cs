using Datos;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NConsumosInsumos
    {
        public static void Create(ConsumosInsumos c)
        {
            try
            {
                DConsumosInsumos.Create(c);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Update(ConsumosInsumos c)
        {
            try
            {
                DConsumosInsumos.Update(c);
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
                DConsumosInsumos.Delete(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ConsumosInsumos> Get()
        {
            try
            {
                return DConsumosInsumos.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ConsumosInsumos> GetByInsumo(Insumos insumo)
        {
            try
            {
                return DConsumosInsumos.GetByInsumo(insumo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}