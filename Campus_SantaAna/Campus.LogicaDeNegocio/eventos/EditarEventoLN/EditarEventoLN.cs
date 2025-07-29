using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.EditarEventoAD;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EditarEventoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Eventos.EditarEventoAD;

namespace Campus.LogicaDeNegocio.Eventos.EditarEventoLN
{
    public class EditarEventoLN : IEditarEventoLN
    {
        private readonly IEditarEventoAD _editarEvento;

        public EditarEventoLN()
        {
            _editarEvento = new EditarEventoAD();
        }

        public async Task<int> EditarEvento(EventoDto evento)
        {
            try
            {
                return await _editarEvento.EditarEvento(evento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el evento", ex);
            }
        }
    }
}
