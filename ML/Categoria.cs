using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Categoria
    {
        [DisplayName("Categorias")]
        [Required(ErrorMessage = "Seleccione una Categoria")]
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public List<object> Categorias { get; set; }
    }
}
