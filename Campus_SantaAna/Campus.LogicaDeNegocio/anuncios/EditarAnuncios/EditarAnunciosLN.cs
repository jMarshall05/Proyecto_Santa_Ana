using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Anuncios.EditarAnunciosAD;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EditarAnunciosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Anuncios.EditarAnunciosAD;

namespace Campus.LogicaDeNegocio.Anuncios.EditarAnuncios
{
    public class EditarAnunciosLN : IEditarAnunciosLN
    {
        private readonly IEditarAnunciosAD _editarAnuncios;

        public EditarAnunciosLN()
        {
            _editarAnuncios = new EditarAnunciosAD();
        }

        public async Task<bool> EditarAnuncio(AnuncioDto anuncio)
        {
            try
            {
                return await _editarAnuncios.EditarAnuncio(anuncio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el anuncio", ex);
            }
        }
    }
}
