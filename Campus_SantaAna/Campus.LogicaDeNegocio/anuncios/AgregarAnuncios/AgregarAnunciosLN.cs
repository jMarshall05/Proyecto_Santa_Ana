using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Anuncios.AgregarAnunciosAD;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.AgregarAnunciosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Anuncios.AgregarAnunciosAD;

namespace Campus.LogicaDeNegocio.Anuncios.AgregarAnuncios
{
    public class AgregarAnunciosLN : IAgregarAnunciosLN
    {
        private IAgregarAnunciosAD _agregarAnuncios;

        public AgregarAnunciosLN()
        {
            _agregarAnuncios = new AgregarAnunciosAD();
        }

        public async Task<int> AgregarAnuncio(AnuncioDto anuncio)
        {
            try
            {
                return await _agregarAnuncios.AgregarAnuncio(anuncio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el anuncio", ex);
            }
        }
    }
}
