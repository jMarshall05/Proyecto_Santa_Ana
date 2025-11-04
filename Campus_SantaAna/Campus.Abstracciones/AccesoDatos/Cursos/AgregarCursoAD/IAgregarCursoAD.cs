using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoAD
{
    public interface IAgregarCursoAD
    {
        Task<int> AgregarCurso(CursoDto curso);
    }
}
