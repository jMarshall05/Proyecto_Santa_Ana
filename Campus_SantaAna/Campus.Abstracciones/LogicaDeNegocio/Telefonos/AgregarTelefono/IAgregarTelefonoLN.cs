using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Telefonos.AgregarTelefono
{
    public interface IAgregarTelefonoLN
    {
        Task<int> AgregarTelefono(List<TelefonoDto> telefono);
    }
}
