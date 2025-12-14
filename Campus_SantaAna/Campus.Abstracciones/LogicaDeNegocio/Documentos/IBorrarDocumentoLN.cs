using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.Documentos
{
    public interface IBorrarDocumentoLN
    {
        Task<bool> BorrarDocumento(int idDocumento);
    }
}
