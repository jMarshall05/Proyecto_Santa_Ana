using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoLN
{
    public interface IModificarEstadoCursoLN
    {
        Task<bool> ModificarEstadoCurso(int idCurso, bool estado);
    }
}
