using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Cursos.AgregarCursoAD
{
    public class AgregarCursoAD : IAgregarCursoAD
    {
        private readonly Contexto _elContexto;
        public AgregarCursoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarCurso(CursoDto curso)
        {
            var CursoAD = ConvertirAD(curso);
            _elContexto.Cursos.Add(CursoAD);
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }

        private CursosAD ConvertirAD(CursoDto curso)
        {
            return new CursosAD
            {
                IdCurso = curso.IdCurso,
                IdProfesor = curso.ProfesorId,
                GrupoId = curso.GrupoId,
                MateriaId = curso.MateriaId,
                Estado = true
            };
        }
    }
}
