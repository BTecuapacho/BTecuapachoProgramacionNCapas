using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        [DisplayName("Municipios")]
        [Required(ErrorMessage = "Seleccione un municipio")]
        public int IdMunicipio { get; set; }
        public string Nombre { get; set; }
        public ML.Estado Estado { get; set; }
        public List<object> Municipios { get; set; }
    }
}
