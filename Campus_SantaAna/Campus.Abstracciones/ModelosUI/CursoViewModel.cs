using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class CursoViewModel
    {
        public CursoDto Curso { get; set; }
        public String NombreProfesor { get; set; }
        public String NombreMateria { get; set; }
        public String NombreGrupo { get; set; }
    }
}
