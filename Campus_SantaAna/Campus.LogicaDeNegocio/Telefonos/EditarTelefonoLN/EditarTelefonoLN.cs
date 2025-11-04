using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.EditarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.EditarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Telefonos.EditarTelefonoAD;

namespace Campus.LogicaDeNegocio.Telefonos.EditarTelefonoLN
{
    public class EditarTelefonoLN : IEditarTelefonoLN
    {
        private readonly IEditarTelefonoAD _editarTelefono;
        public EditarTelefonoLN()
        {
            _editarTelefono = new EditarTelefonoAD();
        }
        public Task<int> EditarTelefono(List<TelefonoDto> telefonos)
        {
            return _editarTelefono.EditarTelefono(telefonos);
        }
    }
}
