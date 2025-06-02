using System.Collections.Generic;
using Campus.Abstracciones.AccesoDatos.Anuncios.ListarAnunciosAD;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.ListarAnunciosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Anuncios.ListarAnunciosAD;

namespace Campus.LogicaDeNegocio.Anuncios.ListarAnuncios
{
    public class ListarAnunciosLN : IListarAnunciosLN
    {
        private readonly IListarAnunciosAD _listarAnuncios;

        public ListarAnunciosLN()
        {
            _listarAnuncios = new ListarAnunciosAD();
        }

        public List<AnuncioDto> ListarAnuncios()
        {
            return _listarAnuncios.ListarAnuncios();
        }

        public AnuncioDto ObtenerAnuncioPorId(int id)
        {
            return _listarAnuncios.ObtenerAnuncioPorId(id);
        }
    }
}
