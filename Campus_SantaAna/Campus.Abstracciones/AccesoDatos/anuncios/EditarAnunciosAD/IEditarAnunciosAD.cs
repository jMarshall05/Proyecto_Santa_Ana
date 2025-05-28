using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Anuncios.EditarAnunciosAD
{
    public interface IEditarAnunciosAD
    {
        Task<bool> EditarAnuncio(AnuncioDto anuncio);
    }
}

