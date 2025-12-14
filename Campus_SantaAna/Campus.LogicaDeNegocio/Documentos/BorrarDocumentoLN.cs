using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class BorrarDocumentoLN : IBorrarDocumentoLN
    {
        private readonly IBorrarDocumentoLN _borrarDocumentoLN;
        public BorrarDocumentoLN()
        {
            _borrarDocumentoLN = new BorrarDocumentoLN();
        }
        public Task<bool> BorrarDocumento(int idDocumento)
        {
            var resultado = _borrarDocumentoLN.BorrarDocumento(idDocumento);
            return resultado;
        }
    }
}
