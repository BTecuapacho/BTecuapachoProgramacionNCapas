using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { set; get; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { set; get; }
        public string Password { set; get; }
        public string Sexo { set; get; }
        public string Telefono { set; get; }
        public string Celular { set; get; }
        public bool Estatus { set; get; }
        public string CURP { set; get; }
        public byte[] Imagen { set; get; }
        public ML.Rol Rol { get; set; } 
        //public ML.Estado Estado { get; set; } 
        //public ML.Municipio Municipio { get; set; } 
        public ML.Direccion Direccion { set; get; }
        public List<object> Usuarios { get; set; }
    }
}
