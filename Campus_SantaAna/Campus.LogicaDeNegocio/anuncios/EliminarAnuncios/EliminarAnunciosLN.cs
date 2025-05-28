using Campus.Abstracciones.AccesoDatos.Anuncios.EliminarAnunciosAD;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EliminarAnunciosLN;
using Campus.AccesoDatos.Anuncios.EliminarAnunciosAD;

namespace Campus.LogicaDeNegocio.Anuncios.EliminarAnuncios
{
    public class EliminarAnunciosLN : IEliminarAnunciosLN
    {
        private readonly IEliminarAnunciosAD _eliminarAnunciosAD;

        public EliminarAnunciosLN()
        {
            _eliminarAnunciosAD = new EliminarAnunciosAD();
        }

        public void EliminarAnuncio(int anuncioId)
        {
            _eliminarAnunciosAD.EliminarAnuncio(anuncioId);
        }
    }
}
