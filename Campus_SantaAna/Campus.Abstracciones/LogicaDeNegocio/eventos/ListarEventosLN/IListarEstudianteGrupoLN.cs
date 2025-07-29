using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Eventos.ListarEventosLN
{
    public interface IListarEventosLN
    {
        Task<List<EventoDto>> ListarEventos();
    }
}
