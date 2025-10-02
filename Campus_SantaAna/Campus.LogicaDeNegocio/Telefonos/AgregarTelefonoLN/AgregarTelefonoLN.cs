using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.AgregarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.AgregarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Telefonos.AgregarTelefonoAD;

namespace Campus.LogicaDeNegocio.Telefonos.AgregarTelefonoLN
{
    internal class AgregarTelefonoLN : IAgregarTelefonoLN
    {
        private readonly IAgregarTelefonoAD _agregarTelefono;
        public AgregarTelefonoLN()
        {
            _agregarTelefono = new AgregarTelefonoAD();
        }
        public int AgregarTelefono(TelefonoDto telefono)
        {
            return _agregarTelefono.AgregarTelefono(telefono);
        }
    }
}
