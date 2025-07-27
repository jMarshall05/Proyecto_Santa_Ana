using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Eventos.AgregarEventoLN
{
    public interface IAgregarEventoLN
    {
        Task<int> AgregarEvento(EventoDto evento);
    }
}
