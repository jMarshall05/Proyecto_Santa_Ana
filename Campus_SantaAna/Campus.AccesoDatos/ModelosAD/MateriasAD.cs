using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("materias")]
    public class MateriasAD
    {
        [Key]
        [Column("id_materia")]
        public int IdMateria { get; set; }

        [Column("nombre")]
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        
    }
}
