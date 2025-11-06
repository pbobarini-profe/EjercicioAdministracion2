using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Usuarios
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public string mail { get; set; }
        public DateTime fechaAlta { get; set; }

        public override string ToString()
        {
            return $"{nombre} {apellido} ({nombreUsuario})";
        }
    }
}
