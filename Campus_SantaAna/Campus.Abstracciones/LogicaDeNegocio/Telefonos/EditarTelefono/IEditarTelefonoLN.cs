using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Telefonos.EditarTelefono
{
    public interface IEditarTelefonoLN
    {
        Task<int> EditarTelefono(List<TelefonoDto> telefonos);
    }
}
