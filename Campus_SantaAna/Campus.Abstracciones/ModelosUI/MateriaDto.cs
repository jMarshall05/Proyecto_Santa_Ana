using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class MateriaDto
{
    [Key]
    [DisplayName("ID de Materia")]
    public int IdMateria { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DisplayName("Nombre")]
    public string Nombre { get; set; }

    [DisplayName("Descripción")]
    public string Descripcion { get; set; }
}
