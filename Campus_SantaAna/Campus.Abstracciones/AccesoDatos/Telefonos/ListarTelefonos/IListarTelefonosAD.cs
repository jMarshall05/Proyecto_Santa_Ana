using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Telefonos.ListarTelefonos
{
    public interface IListarTelefonosAD
    {
        IEnumerable<TelefonoDto> ListarTelefonos();

    }
}
