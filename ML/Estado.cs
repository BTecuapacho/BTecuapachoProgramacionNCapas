using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        [DisplayName("Estados")]
        [Required(ErrorMessage = "Seleccione un estado")]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public List<object> Estados { get; set; }
    }
}
