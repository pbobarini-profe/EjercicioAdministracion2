using Datos;
using Modelos;
using System.Collections.Generic;

namespace Negocio
{
    public class NDetalleVenta
    {
        public static List<DetalleVentas> GetAll()
        {
            return DDetalleVenta.GetAll();
        }

        public static List<DetalleVentas> GetById(Ventas venta)
        {
            return DDetalleVenta.GetById(venta);
        }

        public static void Create(DetalleVentas detalle)
        {
            DDetalleVenta.Create(detalle);
        }

        public static void Update(DetalleVentas detalle)
        {
            DDetalleVenta.Update(detalle);
        }

        public static void Delete(int id)
        {
            DDetalleVenta.Delete(id);
        }
    }
}
