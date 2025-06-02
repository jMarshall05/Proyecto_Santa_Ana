
using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Anuncios.ListarAnunciosLN
{
    public interface IListarAnunciosLN
    {
        List<AnuncioDto> ListarAnuncios();

        AnuncioDto ObtenerAnuncioPorId(int id);
    }
}
