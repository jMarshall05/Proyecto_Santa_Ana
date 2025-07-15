using System.Threading.Tasks;


namespace Campus.Abstracciones.AccesoDatos.calificaciones.eliminarCalificacionDA
{
    public interface IEliminarCalificacion
    {
        Task<int> EliminarCalificacion(int id_calificacion);
    }
}
