using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.AgregarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.AgregarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Telefonos.AgregarTelefonoAD;

namespace Campus.LogicaDeNegocio.Telefonos.AgregarTelefonoLN
{
    public class AgregarTelefonoLN : IAgregarTelefonoLN
    {
        private readonly IAgregarTelefonoAD _agregarTelefono;
        public AgregarTelefonoLN()
        {
            _agregarTelefono = new AgregarTelefonoAD();
        }
        public Task<int> AgregarTelefono(TelefonoDto telefono)
        {
            if (telefono.Tipo == "Hogar" || telefono.Tipo == "Personal" || telefono.Tipo == "Otro" || telefono.Tipo == "Trabajo" || telefono.Tipo == "Encargado")
            {
               return _agregarTelefono.AgregarTelefono(telefono);
            }
            else { 
                throw new Exception("El tipo de telefono no es valido");
            }

        }
    }
}
