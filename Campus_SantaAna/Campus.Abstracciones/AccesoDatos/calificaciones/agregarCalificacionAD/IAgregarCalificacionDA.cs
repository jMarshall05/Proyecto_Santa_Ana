using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.calificaciones.agregarCalificacionAD
{
    public interface IAgregarCalificacion
    {
        Task<int> AgregarCalificacion(CalificacionesDto calificacion);
    }
}
