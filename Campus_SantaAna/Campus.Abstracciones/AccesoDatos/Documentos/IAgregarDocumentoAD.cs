using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Documentos
{
    public interface IAgregarDocumentoAD
    {
        int AgregarDocumento(DocumentosDto documento);
    }
}
