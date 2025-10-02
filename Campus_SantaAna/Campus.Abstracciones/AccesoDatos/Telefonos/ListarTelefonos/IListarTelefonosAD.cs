using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Telefonos.ListarTelefonos
{
    public interface IListarTelefonosAD
    {
        IEnumerable<TelefonoDto> ListarTelefonos();
        IEnumerable<TelefonoDto> ObtenerTelefonosUsuario(bool? estado, string id);

    }
}
