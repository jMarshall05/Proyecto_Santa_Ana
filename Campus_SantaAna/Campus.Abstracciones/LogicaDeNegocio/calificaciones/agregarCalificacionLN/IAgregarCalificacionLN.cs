using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.calificaciones.agregarCalificacionLN
{
    public interface IAgregarCalificacionLN
    {
        Task<int> AgregarCalificacion(CalificacionesDto calificacion);
    }
}
