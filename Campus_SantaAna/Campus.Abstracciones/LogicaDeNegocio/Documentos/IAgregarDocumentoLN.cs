using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Documentos
{
    public interface IAgregarDocumentoLN
    {
        int AgregarDocumento(DocumentosDto documento);
    }
}
