using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Cursos.ListarCursosAD
{
    public class ListarCursosAD : IListarCursoAD
    {
        private readonly Contexto _elContexto;
        public ListarCursosAD()
        {
            _elContexto = new Contexto();
        }

        public List<CursoDto> ListarCursos()
        {
            List<CursoDto> ListaDeCursos = (from Cursos in _elContexto.Cursos
                                            select new CursoDto
                                            {
                                                IdCurso = Cursos.IdCurso,
                                                ProfesorId = Cursos.IdProfesor,
                                                GrupoId = Cursos.GrupoId,
                                                MateriaId = Cursos.MateriaId,
                                                Estado = Cursos.Estado 
                                            }).ToList();
            return ListaDeCursos;
        }

        public CursoDto ObtenerPorId(int idCurso)
        {
            var Curso = _elContexto.Cursos.Where(c => c.IdCurso == idCurso);
            if(Curso != null)
            {
                return new CursoDto
                {
                    IdCurso = Curso.FirstOrDefault().IdCurso,
                    ProfesorId = Curso.FirstOrDefault().IdProfesor,
                    GrupoId = Curso.FirstOrDefault().GrupoId,
                    MateriaId = Curso.FirstOrDefault().MateriaId,
                    Estado = Curso.FirstOrDefault().Estado

                };
            }
            return null;
        }
    }
}
