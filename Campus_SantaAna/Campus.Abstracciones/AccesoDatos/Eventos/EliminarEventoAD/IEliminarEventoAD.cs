using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.Eventos.EliminarEventoAD

{
    public interface IEliminarEventoAD
    {
        Task<int> EliminarEvento(int id);
    }
}
