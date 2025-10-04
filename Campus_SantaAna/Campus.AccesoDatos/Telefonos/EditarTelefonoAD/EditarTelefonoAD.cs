using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.EditarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Telefonos.EditarTelefonoAD
{
    public class EditarTelefonoAD : IEditarTelefonoAD
    {
        private readonly Contexto _elContexto;
        public EditarTelefonoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarTelefono(List<TelefonoDto> telefonos)
        {
            int cambios = 0;

            foreach (var telefono in telefonos)
            {
                var telefonoExistente = await _elContexto.Telefonos
                    .FirstOrDefaultAsync(t => t.Id == telefono.Id);
                if (telefonoExistente != null &&
                      telefonoExistente.Codigo == telefono.Codigo &&
                      telefonoExistente.Telefono == telefono.Telefono &&
                      telefonoExistente.Tipo == telefono.Tipo &&
                      telefonoExistente.Estado == telefono.Estado)
                {
                }
                else if (telefonoExistente != null)
                {
                    telefonoExistente.Codigo = telefono.Codigo;
                    telefonoExistente.Telefono = telefono.Telefono;
                    telefonoExistente.Tipo = telefono.Tipo;
                    telefonoExistente.Estado = telefono.Estado;
                    _elContexto.Entry(telefonoExistente).State = EntityState.Modified;

                }
            }

            cambios = await _elContexto.SaveChangesAsync();
            return cambios;
        }
    }
}
