using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN
{
    public interface IListarCursoLN
    {
        List<CursoDto> ListarCursos();
        CursoDto ObtenerPorId(int idCurso);
    }
}
