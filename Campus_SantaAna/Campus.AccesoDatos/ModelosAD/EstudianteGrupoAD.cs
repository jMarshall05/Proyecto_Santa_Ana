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
        [Column("Id")]
        public int Id { get; set; }
        [Column("Estudiante_id")]
        public string Estudiante_id { get; set; }
        [Column("Grupo_id")]
        public int Grupo_id { get; set; }

    }
}
