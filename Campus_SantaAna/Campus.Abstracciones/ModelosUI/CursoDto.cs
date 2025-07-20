using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class CursoDto
    {
        public Guid IdCurso { get; set; }
        public int MateriaId { get; set; }
        public int GrupoId { get; set; }
        public Guid IdProfesorId { get; set; }
    }
}
