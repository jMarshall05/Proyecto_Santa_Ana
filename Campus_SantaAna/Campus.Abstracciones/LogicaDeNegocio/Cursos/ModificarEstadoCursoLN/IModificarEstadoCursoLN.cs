using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoLN
{
    public interface IModificarEstadoCursoLN
    {
        Task<bool> ModificarEstadoCurso(int idCurso, bool estado);
    }
}
