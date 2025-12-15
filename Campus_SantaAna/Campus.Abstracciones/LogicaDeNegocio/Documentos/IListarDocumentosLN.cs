using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Documentos
{
    public interface IListarDocumentosLN
    {
        IEnumerable<DocumentosDto> ListarDocumentos();

    }
}
