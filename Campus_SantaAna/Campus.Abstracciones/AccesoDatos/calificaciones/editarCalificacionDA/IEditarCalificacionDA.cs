using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.calificaciones.editarCalificacionAD
{
    public interface IEditarCalificacion
    {
        Task<int> EditarCalificacion(CalificacionesDto calificacion);
    }
}
