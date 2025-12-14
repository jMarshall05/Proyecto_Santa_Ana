using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Documentos
{
    public interface IEditarDocumentoLN
    {
        bool EditarDocumento(int idDocumento, DocumentosDto documento);
    }
}
