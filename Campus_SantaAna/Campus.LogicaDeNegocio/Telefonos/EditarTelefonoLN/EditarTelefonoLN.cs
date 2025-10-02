using System;
using Campus.Abstracciones.AccesoDatos.Telefonos.EditarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.EditarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Telefonos.EditarTelefonoAD;

namespace Campus.LogicaDeNegocio.Telefonos.EditarTelefonoLN
{
    internal class EditarTelefonoLN : IEditarTelefonoLN
    {
        private readonly IEditarTelefonoAD _editarTelefono;
        public EditarTelefonoLN()
        {
            _editarTelefono = new EditarTelefonoAD();
        }
        public int EditarTelefono(int id, TelefonoDto telefono)
        {
           return _editarTelefono.EditarTelefono(id, telefono);
        }
    }
}
