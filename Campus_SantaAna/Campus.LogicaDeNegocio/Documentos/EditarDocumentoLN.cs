using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Documentos;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class EditarDocumentoLN : IEditarDocumentoLN
    {
        private readonly IEditarDocumentoAD _editarDocumentoAD;
        public EditarDocumentoLN()
        {
            _editarDocumentoAD = new EditarDocumentoAD();
        }
        public async Task<bool> EditarDocumento(int idDocumento, DocumentosDto documento)
        {
           var resultado = await _editarDocumentoAD.EditarDocumento(idDocumento, documento);
            return resultado;
        }
    }
}
