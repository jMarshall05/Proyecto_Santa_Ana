using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Documentos
{
    public interface IListarDocumentoAD
    {
        IEnumerable<DocumentosDto> ListarDocumentos();
    }
}
