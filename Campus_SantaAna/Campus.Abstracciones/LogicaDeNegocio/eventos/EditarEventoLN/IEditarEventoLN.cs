using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Eventos.EditarEventoLN
{
    public interface IEditarEventoLN
    {
        Task<int> EditarEvento(EventoDto evento);
    }
}
