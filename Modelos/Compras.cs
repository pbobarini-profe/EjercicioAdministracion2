using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Compras
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Proveedores proveedor { get; set; }
        public TipoComprobante tipoComprobante { get; set; }
        public string puntoVenta { get; set; }
        public string numero { get; set; }
        public decimal netoTotal { get; set; }
        public decimal ivaTotal { get; set; }
        public decimal noGravado { get; set; }
        public decimal otrosTributos { get; set; }
        public Usuarios usuario { get; set; }
    }
}
