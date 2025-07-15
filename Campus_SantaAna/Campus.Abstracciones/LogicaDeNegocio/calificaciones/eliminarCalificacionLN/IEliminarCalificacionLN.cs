using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN
{
    public interface IEliminarCalificacionLN
    {
        Task<int> EliminarCalificacion(int id_calificacion);
    }
}
