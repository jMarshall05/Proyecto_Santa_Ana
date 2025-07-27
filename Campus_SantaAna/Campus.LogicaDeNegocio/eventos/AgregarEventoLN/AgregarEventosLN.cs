using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.AgregarEventoAD;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.AgregarEventoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Eventos.AgregarEventoAD;

namespace Campus.LogicaDeNegocio.Eventos.AgregarEventoLN
{
    public class AgregarEventoLN : IAgregarEventoLN
    {
        private readonly IAgregarEventoAD _agregarEvento;

        public AgregarEventoLN()
        {
            _agregarEvento = new AgregarEventoAD();
        }

        public async Task<int> AgregarEvento(EventoDto evento)
        {
            try
            {
                return await _agregarEvento.AgregarEvento(evento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el evento", ex);
            }
        }
    }
}
