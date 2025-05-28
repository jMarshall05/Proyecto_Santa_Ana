using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Campus.Abstracciones.ModelosUI
{
    public class AnuncioDto
    {
        [Key]
        [DisplayName("ID del Anuncio")]
        public int IdAnuncio { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(150, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        public string Titulo { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha del Evento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaEvento { get; set; }

        [DisplayName("Fecha de Publicación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaPublicacion { get; set; }
    }
}
