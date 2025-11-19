using System.Collections.Generic;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Documentos
{
    public class ListarDocumentosAD : IListarDocumentoAD
    {
        private readonly Contexto _elContexto;
        public ListarDocumentosAD()
        {
            _elContexto = new Contexto();
        }

        public IEnumerable<DocumentosDto> ListarDocumentos()
        {
            var lista = _elContexto.Documentos.Select(d => new DocumentosDto
            {
                Id = d.Id,
                Titulo = d.Titulo,
                Descripcion = d.Descripcion,
                RutaArchivo = d.RutaArchivo,
                Categoria = d.Categoria,
                FechaRegistro = d.FechaRegistro
            }).ToList();
            return lista;
        }
    }
}
