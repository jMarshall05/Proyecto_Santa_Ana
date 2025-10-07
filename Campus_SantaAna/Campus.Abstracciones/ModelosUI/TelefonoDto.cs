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
        [DisplayName("IdUsuario")]
        public string IdUsuario { get; set; }
        [DisplayName("Código")]
        public int Codigo { get; set; }
        [DisplayName("Telefono")]
        public int Telefono { get; set; }
        [DisplayName("Tipo")]
        public string Tipo { get; set; }
        [DisplayName("Estado")]
        public bool Estado {  get; set; } = true;
    }
}
