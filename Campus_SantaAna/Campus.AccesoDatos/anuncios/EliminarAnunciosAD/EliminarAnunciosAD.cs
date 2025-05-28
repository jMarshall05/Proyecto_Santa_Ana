using System;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Anuncios.EliminarAnunciosAD;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Anuncios.EliminarAnunciosAD
{
    public class EliminarAnunciosAD : IEliminarAnunciosAD
    {
        private Contexto _elContexto;

        public EliminarAnunciosAD()
        {
            _elContexto = new Contexto();
        }

        public void EliminarAnuncio(int anuncioId)
        {
            var anuncioAEliminar = _elContexto.Anuncios.FirstOrDefault(a => a.IdAnuncio == anuncioId);
            if (anuncioAEliminar != null)
            {
                _elContexto.Anuncios.Remove(anuncioAEliminar);
                _elContexto.SaveChanges();
            }
            else
            {
                throw new Exception("El anuncio no fue encontrado.");
            }
        }
    }
}
