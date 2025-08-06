using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Eventos.AgregarEventoAD
{
    public interface IAgregarEventoAD
    {
        Task<int> AgregarEvento(EventoDto evento);
    }
}
