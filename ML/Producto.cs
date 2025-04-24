using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo seve llevar letras")]
        public string Nombre { get; set; }
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }
        [DisplayName("Precio")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Ingrese un formato de numerico valido")]
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public ML.SubCategoria SubCategoria { get; set; }
        public List<object> Productos { get; set; }
    }
}
