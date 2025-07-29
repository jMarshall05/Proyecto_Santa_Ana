using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.EliminarEventoAD;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EliminarEventoLN;
using Campus.AccesoDatos.Eventos.EliminarEventoAD;


namespace Campus.LogicaDeNegocio.Eventos.EliminarEventoLN
{
    public class EliminarEventoLN : IEliminarEventoLN
    {
        private readonly IEliminarEventoAD _eliminarEvento;

        public EliminarEventoLN()
        {
            _eliminarEvento = new EliminarEventoAD();
        }

        public async Task<int> EliminarEvento(int id)
        {
            try
            {
                return await _eliminarEvento.EliminarEvento(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el evento", ex);
            }
        }
    }
}
