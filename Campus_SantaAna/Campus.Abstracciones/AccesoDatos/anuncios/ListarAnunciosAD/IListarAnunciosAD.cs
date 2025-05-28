

using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Anuncios.ListarAnunciosAD
{
    public interface IListarAnunciosAD
    {
        List<AnuncioDto> ListarAnuncios();


        AnuncioDto ObtenerAnuncioPorId(int id);
    }
}
