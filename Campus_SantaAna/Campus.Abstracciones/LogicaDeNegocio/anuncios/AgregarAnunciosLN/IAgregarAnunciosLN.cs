using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Anuncios.AgregarAnunciosLN
{
    public interface IAgregarAnunciosLN
    {
        Task<int> AgregarAnuncio(AnuncioDto anuncio);
    }
}
