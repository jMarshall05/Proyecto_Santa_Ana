using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.ListarTelefonos;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.ListarTelefonos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Telefonos.ListarTelefonosAD;

namespace Campus.LogicaDeNegocio.Telefonos.ListarTelefonosLN
{
    public class ListarTelefonosLN : IListarTelefonosLN
    {
        private readonly IListarTelefonosAD _listarTelefonos;
        public ListarTelefonosLN()
        {
            _listarTelefonos = new ListarTelefonosAD();
        }

        public IEnumerable<TelefonoDto> ListarTelefono()
        {
            return _listarTelefonos.ListarTelefonos();
        }

   
    }
}
