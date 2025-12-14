using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class EditarDocumentoLN : IEditarDocumentoLN
    {
        private readonly IEditarDocumentoLN _editarDocumentoLN;
        public EditarDocumentoLN()
        {
            _editarDocumentoLN = new EditarDocumentoLN();
        }
        public bool EditarDocumento(int idDocumento, DocumentosDto documento)
        {
           var resultado = _editarDocumentoLN.EditarDocumento(idDocumento, documento);
            return resultado;
        }
    }
}
