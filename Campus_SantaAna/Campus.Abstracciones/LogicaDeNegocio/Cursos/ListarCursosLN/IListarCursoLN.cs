using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN
{
    public interface IListarCursoLN
    {
        List<CursoDto> ListarCursos();
        CursoDto ObtenerPorId(int idCurso);
    }
}
