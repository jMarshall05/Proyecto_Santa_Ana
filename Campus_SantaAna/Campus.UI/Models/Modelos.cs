using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Campus.Abstracciones.ModelosUI;

public class CursoViewModel
{
    public int IdCurso { get; set; }
    public string MateriaNombre { get; set; }
    public string GrupoNombre { get; set; }
    public string ProfesorNombre { get; set; }
}
public class EditarUsuario {
    [DisplayName("Apellido")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Nombre { get; set; }
    [DisplayName("Apellido")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Apellido { get; set; }

    public List<TelefonoDto> Telefonos { get; set; }

}