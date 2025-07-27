using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class CursoDto
    {
        [DisplayName("Id del Curso")]
        public int IdCurso { get; set; }
        [DisplayName("Materia")]
        public int MateriaId { get; set; }
        [DisplayName("Grupo")]
        public int GrupoId { get; set; }
        [DisplayName("Profesor")]
        public string ProfesorId { get; set; }

        public string NombreProfesor { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreMateria { get; set; }
    }
}
