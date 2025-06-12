using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class UsuariosDto
    {
        [Key]
        [DisplayName("Id de Usuario")]
        public string IdUsuario { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }
        [DisplayName("Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Apellido { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Telefono { get; set; }
        [DisplayName("Fecha de Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaDeNacimiento { get; set; }
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cedula { get; set; }
        [DisplayName("Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }
        [DisplayName("Fecha de Modificación")]
        public DateTime? FechaDeModificacion { get; set; }
        [DisplayName("Rol")]
        public string Rol { get; set; }
        [DisplayName("Estado")]
        public bool Estado { get; set; }
        [DisplayName("Id de Grupo")]
        public int? Id_grupo { get; set; }

    }
}
