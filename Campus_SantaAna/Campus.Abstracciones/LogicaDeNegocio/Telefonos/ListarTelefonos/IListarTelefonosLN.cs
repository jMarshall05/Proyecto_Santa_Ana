using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Telefonos.ListarTelefonos
{
    public interface IListarTelefonosLN
    {
        IEnumerable<TelefonoDto> ListarTelefono();
        IEnumerable<TelefonoDto> ObtenerTelefonosUsuario(bool? estado, string id);
    }
}
