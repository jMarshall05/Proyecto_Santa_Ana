using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Anuncios.EditarAnunciosLN
{
    public interface IEditarAnunciosLN
    {
        Task<bool> EditarAnuncio(AnuncioDto anuncio);
    }
}
