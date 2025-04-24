using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class SubCategoria
    {
        [DisplayName("SubCategorias")]
        [Required(ErrorMessage = "Seleccione una SubCategoria")]
        public int IdSubCategoria { get; set; }
        public string Nombre { get; set; }
        public List<object> SubCategorias { get; set; }
        public ML.Categoria Categoria { get; set; }
    }
}
