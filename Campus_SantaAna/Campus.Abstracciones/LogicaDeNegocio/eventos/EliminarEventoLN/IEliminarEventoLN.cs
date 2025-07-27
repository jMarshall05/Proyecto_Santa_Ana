using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.Eventos.EliminarEventoLN
{
    public interface IEliminarEventoLN
    {
        Task<int> EliminarEvento(int id);
    }
}
