using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.Documentos
{
    public interface IBorrarDocumentoAD
    {
        Task<bool> BorrarDocumento(int idDocumento);
    }
}
