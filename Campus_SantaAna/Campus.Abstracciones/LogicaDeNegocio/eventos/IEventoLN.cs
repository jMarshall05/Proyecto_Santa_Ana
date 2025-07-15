using Campus.Abstracciones.ModelosUI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.Eventos
{
    public interface IEventoLN
    {
        Task<int> AgregarEvento(EventoDto evento);
        Task<List<EventoDto>> ListarEventos();
        Task<int> EditarEvento(EventoDto evento);
        Task<int> EliminarEvento(int id);

    }
}