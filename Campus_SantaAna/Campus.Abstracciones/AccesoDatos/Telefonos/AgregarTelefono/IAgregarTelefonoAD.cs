using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Telefonos.AgregarTelefono
{
    public interface IAgregarTelefonoAD
    {
        Task<int> AgregarTelefono(List<TelefonoDto> telefono);
    }
}
