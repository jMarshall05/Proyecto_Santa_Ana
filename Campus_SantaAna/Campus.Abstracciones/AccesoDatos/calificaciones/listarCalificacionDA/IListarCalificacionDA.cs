using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.calificaciones.listarCalificacionDA
{
    public interface IListarCalificaciones
    {
        Task<List<CalificacionesDto>> ListarCalificaciones();
        Task<List<CalificacionesDto>> ListarCalificacionesPorGrupo(int idGrupo);
        Task<List<CalificacionesDto>> ListarCalificacionesPorEstudiante(string idEstudiante);
    }
}
