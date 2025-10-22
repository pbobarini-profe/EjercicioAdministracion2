using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class DetalleVentas
    {
        public int id { get; set; }
        public Ventas venta { get; set; }
        public decimal cantidad { get; set; }
        public Productos producto { get; set; }
        public decimal precioUnitario { get; set; }
        public Usuarios usuario { get; set; }
    }
}
