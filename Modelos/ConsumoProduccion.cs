using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ConsumoProduccion
    {
        public int id { get; set; }
        public ConsumosInsumos consumoInsumo { get; set; }
        public ProduccionProductos produccionProducto { get; set; }
        public string observaciones { get; set; } //varchar max
    }
}
