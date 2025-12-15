using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Documentos
{
    public class EditarDocumentoAD : IEditarDocumentoAD
    {
        private readonly Contexto _ElContexto;
        public EditarDocumentoAD()
        {
            _ElContexto = new Contexto();
        }

        public async Task<bool> EditarDocumento(int idDocumento, DocumentosDto documento)
        {
            var documentoExistente = await _ElContexto.Documentos.FindAsync(idDocumento);
            if (documentoExistente != null)
            {
                documentoExistente.Titulo = documento.Titulo;
                documentoExistente.Descripcion = documento.Descripcion;
                documentoExistente.RutaArchivo = documento.RutaArchivo;
                documentoExistente.Categoria = documento.Categoria;
                documentoExistente.FechaRegistro = documento.FechaRegistro;
                _ElContexto.Entry(documentoExistente);
                var resultado= await _ElContexto.SaveChangesAsync().ContinueWith(e=>e.Result>0);
                return resultado;
            }
            return false;
        }
    }
}
