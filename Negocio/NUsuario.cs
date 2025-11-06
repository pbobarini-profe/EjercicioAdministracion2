using System.Collections.Generic;
using Modelos;
using Datos;

namespace Negocio
{
    public static class NUsuario
    {
        public static List<Usuarios> GetAll()
        {
            return DUsuario.GetAll();
        }

        public static Usuarios GetById(int id)
        {
            return DUsuario.GetById(id);
        }

       
    }
}
