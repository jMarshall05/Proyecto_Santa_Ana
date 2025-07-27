using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Eventos.ListarEventosad
{
    public interface IListarEventosAD
    {
        Task<List<EventoDto>> ListarEventos();
    }
}
