using System.Collections.Generic;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Telefonos.ListarTelefonos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Telefonos.ListarTelefonosAD
{
    public class ListarTelefonosAD : IListarTelefonosAD
    {
        private readonly Contexto _elContexto;
        public ListarTelefonosAD()
        {
            _elContexto = new Contexto();
        }
        public IEnumerable<TelefonoDto> ListarTelefonos()
        {
            var telefonos = (from Telefonos in _elContexto.Telefonos
                             select new TelefonoDto
                             {
                                 Id = Telefonos.Id,
                                 Codigo = Telefonos.Codigo,
                                 Telefono = Telefonos.Telefono,
                                 Tipo = Telefonos.Tipo,
                                 Estado = Telefonos.Estado,
                                 IdUsuario = Telefonos.IdUsuario
                             }).ToList();
            return telefonos;
        }
    }
}
