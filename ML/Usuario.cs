using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [DisplayName("Nombre de usuaio")]
        [Required(ErrorMessage = "Nombre de usuario Obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){6,18}[a-zA-Z0-9]$", ErrorMessage = " Nombre de usuario solo se acepta letras")]
        public string UserName { set; get; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo seve llevar letras")]
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido paterno solo seve llevar letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido paterno solo seve llevar letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Correo")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Ingrese un correo electronico valido")]
        public string Email { get; set; }
        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [RegularExpression(@"\d{1,2}\/\d{1,2}\/\d{2,4}", ErrorMessage = "Ingrese un formato de fecha valido")]
        public string FechaNacimiento { set; get; }
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contreseña es obligatoria")]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener mínimo 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial")]
        public string Password { set; get; }
        public string Sexo { set; get; }
        [DisplayName("Num. Telefono")]
        [Required(ErrorMessage = "El telefono no puede ser vacio")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Ingrese un formato telefonico valido")]
        public string Telefono { set; get; }
        [DisplayName("Num. Celular")]
        [Required(ErrorMessage = "El telefono no puede ser vacio")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Ingrese un formato telefonico valido")]
        public string Celular { set; get; }
        public bool Estatus { set; get; }
        [DisplayName("CURP")]
        [Required(ErrorMessage = "El CURP es obligatoria")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "Ingrese un formato de CURP valido")]
        public string CURP { set; get; }
        public byte[] Imagen { set; get; }
        public string ImagenBase64 { set; get; }
        public ML.Rol Rol { get; set; } 
        public ML.Direccion Direccion { set; get; }
        public List<object> Usuarios { get; set; }
    }
}
