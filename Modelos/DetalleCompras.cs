using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class DetalleCompras
    {
        public int id { get; set; }
        public Compras compra { get; set; }
        public decimal cantidad { get; set; }
        public Insumos insumo { get; set; }
        public decimal precioUnitario { get; set; }
        public Usuarios usuario { get; set; }
    }
}
