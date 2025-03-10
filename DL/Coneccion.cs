using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Coneccion
    {
        public static string GetConeccion()
        {
            return ConfigurationManager.ConnectionStrings["BTecuapachoProgramacionNCapas"].ToString();
        }
    }
}
