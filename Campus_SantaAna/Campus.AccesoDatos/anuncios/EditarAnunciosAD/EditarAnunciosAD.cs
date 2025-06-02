using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Anuncios.EditarAnunciosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

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
            anuncioExistente.FechaPublicacion = anuncio.FechaPublicacion;

            _elContexto.Entry(anuncioExistente).State = System.Data.Entity.EntityState.Modified;
            await _elContexto.SaveChangesAsync();

            return true;
        }
    }
}
