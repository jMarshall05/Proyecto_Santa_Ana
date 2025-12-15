using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Documentos;

namespace Campus.LogicaDeNegocio.Documentos
{
    public class AgregarDocumentoLN : IAgregarDocumentoLN
    {
        private readonly IAgregarDocumentoAD _agregarDocumentoAD;
        public AgregarDocumentoLN()
        {
            _agregarDocumentoAD = new AgregarDocumentosAD();
        }
        public int AgregarDocumento(DocumentosDto documento)
        {
            return _agregarDocumentoAD.AgregarDocumento(documento);
        }
    }
}
