using System.Collections.Generic;
using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Documentos;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class ListarDocumentosLN : IListarDocumentosLN
    {
        private readonly IListarDocumentoAD _listarDocumentoAD;
        public ListarDocumentosLN()
        {
            _listarDocumentoAD = new ListarDocumentosAD();
        }
        public IEnumerable<DocumentosDto> ListarDocumentos()
        {
            return _listarDocumentoAD.ListarDocumentos();
        }
    }
}
