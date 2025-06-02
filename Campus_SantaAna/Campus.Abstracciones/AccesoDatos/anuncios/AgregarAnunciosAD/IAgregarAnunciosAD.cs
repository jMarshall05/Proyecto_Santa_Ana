
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Anuncios.AgregarAnunciosAD
{
    public interface IAgregarAnunciosAD
    {
        Task<int> AgregarAnuncio(AnuncioDto anuncio);
    }
}

