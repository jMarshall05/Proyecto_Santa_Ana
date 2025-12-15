using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosAD
{
    public interface IListarCursoAD
    {
        List<CursoDto> ListarCursos();
        CursoDto ObtenerPorId(int idCurso);
    }
}
