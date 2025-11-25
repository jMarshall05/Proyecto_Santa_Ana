using System.Collections.Generic;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosAD;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Cursos.ListarCursosAD;

namespace Campus.LogicaDeNegocio.Cursos.ListarCursosLN
{
    public class ListarCursosLN : IListarCursoLN
    {
        private readonly IListarCursoAD _listarCursoAD;
        public ListarCursosLN()
        {
            _listarCursoAD = new ListarCursosAD();
        }
        public List<CursoDto> ListarCursos()
        {
            return _listarCursoAD.ListarCursos();
        }

        public CursoDto ObtenerPorId(int idCurso)
        {
            return _listarCursoAD.ObtenerPorId(idCurso);
        }

    }
}
