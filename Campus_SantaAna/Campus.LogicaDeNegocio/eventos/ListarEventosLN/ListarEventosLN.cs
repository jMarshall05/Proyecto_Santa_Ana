using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.ListarEventosad;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.ListarEventosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Eventos.ListarEventosAD;
namespace Campus.LogicaDeNegocio.Eventos.ListarEventosLN
{
    public class ListarEventosLN : IListarEventosLN
    {
        private readonly IListarEventosAD _listarEventos;

        public ListarEventosLN()
        {
            _listarEventos = new ListarEventosAD();
        }

        public async Task<List<EventoDto>> ListarEventos()
        {
            try
            {
                return await _listarEventos.ListarEventos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los eventos", ex);
            }
        }
    }
}
