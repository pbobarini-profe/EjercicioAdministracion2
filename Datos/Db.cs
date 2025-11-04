using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Db
    {

        public static SqlConnection GetConnection()
        {
            string cn = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            return new SqlConnection(cn);
        }
    }
}
