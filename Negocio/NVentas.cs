using Datos;
using Modelos;
using System.Collections.Generic;

namespace Negocio
{
    public class NVentas
    {
        public static List<Ventas> GetAll()
        {
            return DVentas.GetAll();
        }

        public static Ventas GetById(int id)
        {
            return DVentas.GetById(id);
        }
    }
}
