using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoAD
{
    public interface IModificarEstadoCursoAD
    {
        Task<bool> ModificarEstadoCurso(int idCurso, bool estado);
    }
}
