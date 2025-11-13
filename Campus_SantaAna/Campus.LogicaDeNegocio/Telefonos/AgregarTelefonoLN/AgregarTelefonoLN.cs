using System;
using System.Collections.Generic;
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
        public Task<int> AgregarTelefono(List<TelefonoDto> telefonos)
        {
            List<TelefonoDto> telefonosValidos = new List<TelefonoDto>();
            foreach (var telefono in telefonos)
            {
                if (telefono.Tipo != "Hogar" && telefono.Tipo != "Personal" && telefono.Tipo != "Otro" && telefono.Tipo != "Trabajo" && telefono.Tipo != "Encargado")
                {
                    throw new Exception("El tipo de telefono no es valido");
                }
                else
                {
                    telefonosValidos.Add(telefono);

                }
            }
            return _agregarTelefono.AgregarTelefono(telefonosValidos);


        }
    }
}
