using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.EliminarCursoLN
{
    public interface IEliminarCursoLN
    {
        Task<bool> EliminarCurso(int idCurso);
    }
}
