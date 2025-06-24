using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("EstudianteGrupo")]
    public class EstudianteGrupoAD
    {
        [Key]
        [Column("IdEstudianteGrupo")]
        public int IdEstudianteGrupo { get; set; }

        [Column("EstudianteId")]
        public string EstudianteId { get; set; }

        [Column("GrupoId")]
        public int GrupoId { get; set; }

        // Propiedad de navegación corregida
        [ForeignKey("GrupoId")]
        public virtual GruposAD Grupo { get; set; } // Cambiado de GrupoAD a GruposAD
    }
}