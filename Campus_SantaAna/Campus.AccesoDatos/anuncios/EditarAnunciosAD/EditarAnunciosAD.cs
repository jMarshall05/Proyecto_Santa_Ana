using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Anuncios.EditarAnunciosAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Anuncios.EditarAnunciosAD
{
    public class EditarAnunciosAD : IEditarAnunciosAD
    {
        private readonly Contexto _elContexto;

        public EditarAnunciosAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<bool> EditarAnuncio(AnuncioDto anuncio)
        {
            var anuncioExistente = await _elContexto.Anuncios.FindAsync(anuncio.IdAnuncio);
            if (anuncioExistente == null)
            {
                return false;
            }

            anuncioExistente.Titulo = anuncio.Titulo;
            anuncioExistente.Descripcion = anuncio.Descripcion;
            anuncioExistente.FechaEvento = anuncio.FechaEvento;
            anuncioExistente.FechaPublicacion = DateTime.UtcNow;
            anuncioExistente.Estado = anuncio.Estado;
            anuncioExistente.ImagenRuta = anuncio.ImagenRuta;
            try
            {
                _elContexto.Entry(anuncioExistente).State = EntityState.Modified;
                await _elContexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el anuncio", ex);
            }

            return true;
        }
    }
}
