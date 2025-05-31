using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TareaDto
{
    [Key]
    [DisplayName("ID de Tarea")]
    public int IdTarea { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DisplayName("Título")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DisplayName("Descripción")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DisplayName("Fecha de Entrega")]
    public DateTime FechaEntrega { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DisplayName("ID de Materia")]
    [ForeignKey(nameof(Materia))]
    public int IdMateria { get; set; }

    [DisplayName("Archivo Adjunto")]
    public string ArchivoAdjunto { get; set; }

    [Required]
    [DisplayName("Fecha de Creación")]
    public DateTime FechaCreacion { get; set; }

    [DisplayName("Fecha de Modificación")]
    public DateTime FechaModificacion { get; set; }

    [DisplayName("Fecha de Publicación")]
    public DateTime FechaPublicacion { get; set; }

    public virtual MateriaDto Materia { get; set; }
}
