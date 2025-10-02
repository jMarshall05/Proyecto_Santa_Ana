using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class TelefonoDto
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [Required]
        [DisplayName("IdUsuario")]
        public string IdUsuario { get; set; }
        [Required]
        [DisplayName("Código")]
        public string Codigo { get; set; }
        [Required]
        [DisplayName("Telefono")]
        public string Telefono { get; set; }
        [Required]
        [DisplayName("Tipo")]
        public string Tipo { get; set; }
        [Required]
        [DisplayName("Tipo")]
        public bool Estado {  get; set; } = true;
    }
}
