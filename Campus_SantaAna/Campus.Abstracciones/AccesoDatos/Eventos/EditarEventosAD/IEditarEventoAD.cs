using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Eventos.EditarEventoAD { 
    public interface IEditarEventoAD
    {
        Task<int> EditarEvento(EventoDto evento);
    }
}
