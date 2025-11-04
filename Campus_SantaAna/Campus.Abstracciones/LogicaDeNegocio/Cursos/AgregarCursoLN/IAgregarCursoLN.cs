using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoLN
{
    public interface IAgregarCursoLN
    {
        Task<int> AgregarCurso(CursoDto curso);
    }
}
