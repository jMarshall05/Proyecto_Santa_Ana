using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN
{
    public interface IEditarCalificacionLN
    {
        Task<int> EditarCalificacion(CalificacionesDto calificacion);
    }
}
