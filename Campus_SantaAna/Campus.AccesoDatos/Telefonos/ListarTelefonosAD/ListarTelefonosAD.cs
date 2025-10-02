using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                 Telefono = Telefonos.Telefono,
                                 Tipo = Telefonos.Tipo,
                                 Estado = Telefonos.Estado,
                                 IdUsuario = Telefonos.IdUsuario
                             }).ToList();
            return telefonos;
        }

        public IEnumerable<TelefonoDto> ObtenerTelefonosUsuario(bool? estado, string id)
        {
            if (estado.HasValue)
            {
                var telefonos = (from Telefonos in _elContexto.Telefonos
                                 where Telefonos.IdUsuario == id && Telefonos.Estado == estado
                                 select new TelefonoDto
                                 {
                                     Id = Telefonos.Id,
                                     Telefono = Telefonos.Telefono,
                                     Tipo = Telefonos.Tipo,
                                     Estado = Telefonos.Estado,
                                     IdUsuario = Telefonos.IdUsuario
                                 }).ToList();
                return telefonos;
            }
            else
            {
                var telefonos = (from Telefonos in _elContexto.Telefonos
                                 where Telefonos.IdUsuario == id && Telefonos.Estado == estado
                                 select new TelefonoDto
                                 {
                                     Id = Telefonos.Id,
                                     Telefono = Telefonos.Telefono,
                                     Tipo = Telefonos.Tipo,
                                     Estado = Telefonos.Estado,
                                     IdUsuario = Telefonos.IdUsuario
                                 }).ToList();
                return telefonos;
            }

        }
    }
}
