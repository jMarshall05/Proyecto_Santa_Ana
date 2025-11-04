using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.ListarEventosad;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.ListarEventosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos;
using Campus.AccesoDatos.Eventos.ListarEventosAD;

namespace Campus.LogicaDeNegocio.Eventos.ListarEventosLN
{
    public class ListarEventosLN : IListarEventosLN
    {
        private readonly IListarEventosAD _listarEventosAD;


        public ListarEventosLN()
        {
            _listarEventosAD = new ListarEventosAD(new Contexto());
        }


        public ListarEventosLN(IListarEventosAD listarEventosAD)
        {
            _listarEventosAD = listarEventosAD;
        }


        public async Task<List<EventoDto>> ListarEventos(string idUsuario)
        {
            try
            {
                return await _listarEventosAD.ListarEventos(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los eventos", ex);
            }
        }
    }
}
