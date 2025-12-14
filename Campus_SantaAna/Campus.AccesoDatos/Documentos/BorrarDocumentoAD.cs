using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Documentos;

namespace Campus.AccesoDatos.Documentos
{
    public class BorrarDocumentoAD : IBorrarDocumentoAD
    {
        private readonly Contexto _elContexto;
        public BorrarDocumentoAD()
        {
            _elContexto = new Contexto();
        }
        public async Task<bool> BorrarDocumento(int idDocumento)
        {
            var documentoExistente = await _elContexto.Documentos.FindAsync(idDocumento);
            if (documentoExistente != null)
            {
                _elContexto.Documentos.Remove(documentoExistente);
                var resultado = await _elContexto.SaveChangesAsync().ContinueWith(e => e.Result > 0);
                return resultado;
            }
            return false;

        }
    }
}
