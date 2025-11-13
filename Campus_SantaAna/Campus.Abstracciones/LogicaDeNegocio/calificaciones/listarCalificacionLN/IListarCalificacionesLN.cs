using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN
{
    public interface IListarCalificacionesLN
    {
        Task<List<CalificacionesDto>> ListarCalificaciones();
        Task<List<CalificacionesDto>> ListarCalificacionesPorGrupoAsync(int idGrupo);
        Task<List<CalificacionesDto>> ListarCalificacionesPorEstudianteAsync(string idEstudiante);

    }
}
