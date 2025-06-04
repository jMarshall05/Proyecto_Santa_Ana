using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Campus.Abstracciones.ModelosUI
{
    public class GruposDto
    {
        [Key]
        [Display(Name = "ID de Grupo")]
        public int id_grupo { get; set; }
        [Display(Name = "Nombre del Grupo")]
        [Required(ErrorMessage = "El nombre del grupo es obligatorio.")]
        public string nombre_grupo { get; set; }
        [Display(Name = "Descripción del Grupo")]
        [Required(ErrorMessage = "La descripción del grupo es obligatoria.")]
        public string descripcion { get; set; }
        [Display(Name = "Creador")]
        public string creado_por { get; set; }
        [Display(Name = "Estado")]
        public bool estado { get; set; }

    }
}
