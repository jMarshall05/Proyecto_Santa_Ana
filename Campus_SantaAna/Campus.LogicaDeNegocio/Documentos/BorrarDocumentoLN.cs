using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.AccesoDatos.Documentos;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class BorrarDocumentoLN : IBorrarDocumentoLN
    {
        private readonly IBorrarDocumentoAD _borrarDocumentoLN;
        public BorrarDocumentoLN()
        {
            _borrarDocumentoLN = new BorrarDocumentoAD();
        }
        public Task<bool> BorrarDocumento(int idDocumento)
        {
            var resultado = _borrarDocumentoLN.BorrarDocumento(idDocumento);
            return resultado;
        }
    }
}
