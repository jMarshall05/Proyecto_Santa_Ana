using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Telefonos.AgregarTelefono
{
    public interface IAgregarTelefonoAD
    {
        Task<int> AgregarTelefono(List<TelefonoDto> telefono); 
    }
}
