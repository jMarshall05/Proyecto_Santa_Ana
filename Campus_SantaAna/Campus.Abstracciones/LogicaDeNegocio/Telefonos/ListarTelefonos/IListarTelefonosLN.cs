using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Telefonos.ListarTelefonos
{
    public interface IListarTelefonosLN
    {
        IEnumerable<TelefonoDto> ListarTelefono();

    }
}
