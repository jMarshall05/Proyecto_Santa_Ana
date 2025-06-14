using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

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

    }
}
