using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Eventos
{
    public interface IEventoAD
    {
        Task<int> AgregarEvento(EventoDto evento);
        Task<List<EventoDto>> ListarEventos();
        Task<int> EditarEvento(EventoDto evento);
        Task<int> EliminarEvento(int id);

    }
}
