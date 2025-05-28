using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Anuncios.AgregarAnunciosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Anuncios.AgregarAnunciosAD
{
    public class AgregarAnunciosAD : IAgregarAnunciosAD
    {
        private Contexto _elContexto;

        public AgregarAnunciosAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarAnuncio(AnuncioDto anuncio)
        {
            var anuncioTransformado = ConvertirAD(anuncio);
            _elContexto.Anuncios.Add(anuncioTransformado);
            _elContexto.Entry(anuncioTransformado).State = System.Data.Entity.EntityState.Added;
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }

        private AnunciosAD ConvertirAD(AnuncioDto anuncio)
        {
            return new AnunciosAD
            {
                Titulo = anuncio.Titulo,
                Descripcion = anuncio.Descripcion,
                FechaEvento = anuncio.FechaEvento,
                FechaPublicacion = anuncio.FechaPublicacion
            };
        }
    }
}
