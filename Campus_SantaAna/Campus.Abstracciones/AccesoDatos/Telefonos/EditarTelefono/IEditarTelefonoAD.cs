using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Telefonos.EditarTelefono
{
    public interface IEditarTelefonoAD
    {
        Task <int> EditarTelefono(List<TelefonoDto> telefonos);
    }
}
