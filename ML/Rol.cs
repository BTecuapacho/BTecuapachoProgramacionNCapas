using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [DisplayName("Roles")]
        [Required(ErrorMessage = "Seleccione un rol")]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public List<object> Roles { get; set; }
    }
}
