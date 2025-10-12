using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("Cursos")]
    public class CursosAD
    {
        [Key]
        [Column("IdCurso")]
        public int IdCurso { get; set; }
        [Required]
        [Column("MateriaId")]
        public int MateriaId { get; set; }
        [Required]
        [Column("IdProfesor")]
        public string IdProfesor { get; set; }
        [Required]
        [Column("GrupoId")]
        public int GrupoId { get; set; }

        [Column("estado")]
        public bool Estado { get; set; }

    }
}
