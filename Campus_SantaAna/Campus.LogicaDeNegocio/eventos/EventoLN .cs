using Campus.Abstracciones.AccesoDatos.Eventos;
using Campus.Abstracciones.LogicaDeNegocio.Eventos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Eventos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campus.LogicaDeNegocio.Eventos
{
    public class EventoLN : IEventoLN
    {
        private readonly IEventoAD _eventoAD;

        public EventoLN()
        {
            _eventoAD = new EventoAccesoDatos(); 
        }

        public async Task<int> AgregarEvento(EventoDto evento)
        {
            return await _eventoAD.AgregarEvento(evento);
        }

        public async Task<List<EventoDto>> ListarEventos()
        {
            return await _eventoAD.ListarEventos();
        }
        public async Task<int> EditarEvento(EventoDto evento)
        {
            return await _eventoAD.EditarEvento(evento);
        }

        public async Task<int> EliminarEvento(int id)
        {
            return await _eventoAD.EliminarEvento(id);
        }

    }
}
