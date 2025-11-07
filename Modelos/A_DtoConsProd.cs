using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class A_DtoConsProd
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public int InsumoId { get; set; }
        public string Insumo { get; set; }
        public string Observaciones { get; set; }
    }
}
