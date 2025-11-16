using Campus.Abstracciones.AccesoDatos.Documentos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Documentos
{
    public class AgregarDocumentosAD : IAgregarDocumentoAD
    {
        private readonly Contexto _elContexto;
        public AgregarDocumentosAD()
        {
            _elContexto = new Contexto();
        }

        public int AgregarDocumento(DocumentosDto documento)
        {
            var documentoAD = ConvertirAD(documento);
            _elContexto.Documentos.Add(documentoAD);
            _elContexto.SaveChanges();
            return documentoAD.Id;

        }

        private DocumentosAD ConvertirAD(DocumentosDto documento)
        {
            var documentoAD = new DocumentosAD
            {
                Titulo = documento.Titulo,
                Descripcion = documento.Descripcion,
                RutaArchivo = documento.RutaArchivo,
                Categoria = documento.Categoria,
                FechaRegistro = documento.FechaRegistro
            };
            return documentoAD;
        }
    }
}
