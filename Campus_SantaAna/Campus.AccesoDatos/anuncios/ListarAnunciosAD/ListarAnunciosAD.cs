using System.Collections.Generic;
using System.Linq;                       //  👈 asegúrate de tenerlo
using Campus.Abstracciones.AccesoDatos.Anuncios.ListarAnunciosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Anuncios.ListarAnunciosAD
{
    public class ListarAnunciosAD : IListarAnunciosAD
    {
        private readonly Contexto _elContexto;

        public ListarAnunciosAD()
        {
            _elContexto = new Contexto();
        }

        public List<AnuncioDto> ListarAnuncios()
        {
            return (from anuncio in _elContexto.Anuncios
                    select new AnuncioDto
                    {
                        IdAnuncio = anuncio.IdAnuncio,
                        Titulo = anuncio.Titulo,
                        Descripcion = anuncio.Descripcion,
                        FechaEvento = anuncio.FechaEvento,
                        FechaPublicacion = anuncio.FechaPublicacion
                    }).ToList();
        }

        public AnuncioDto ObtenerAnuncioPorId(int id)
        {
            var anuncio = _elContexto.Anuncios
                                     .FirstOrDefault(a => a.IdAnuncio == id);

            if (anuncio == null) return null;

            return new AnuncioDto
            {
                IdAnuncio = anuncio.IdAnuncio,
                Titulo = anuncio.Titulo,
                Descripcion = anuncio.Descripcion,
                FechaEvento = anuncio.FechaEvento,
                FechaPublicacion = anuncio.FechaPublicacion
            };
        }
    }
}
