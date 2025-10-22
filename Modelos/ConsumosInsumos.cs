using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ConsumosInsumos
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public decimal cantidad { get; set; }
        public Insumos insumo { get; set; }
    }
}
