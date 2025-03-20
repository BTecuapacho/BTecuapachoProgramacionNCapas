using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [DisplayName("Nombre de la calle")]
        [Required(ErrorMessage = "La calle no puede ser vacia")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Ingrese un nombre de calle valido")]
        public string Calle { get; set; }
        [DisplayName("Num. Interior")]
        [Required(ErrorMessage = "El numero interior no puede ser vacio")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string NumeroInterior { get; set; }
        [DisplayName("Num. Exterior")]
        [Required(ErrorMessage = "El numero exterior no puede ser vacio")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public List<object> Direcciones { get; set; }
    }
}
