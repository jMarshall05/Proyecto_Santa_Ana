using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Telefonos.EditarTelefono
{
    public interface IEditarTelefonoLN
    {
        int EditarTelefono(int id, TelefonoDto telefono);
    }
}
