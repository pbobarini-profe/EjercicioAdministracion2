using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ProduccionProductos
    {
        //Rodrigo
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Productos producto { get; set; }
        public decimal cantidad { get; set; }
    }
}
