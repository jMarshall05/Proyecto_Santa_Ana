using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.EditarTelefono;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Telefonos.EditarTelefonoAD
{
    public class EditarTelefonoAD : IEditarTelefonoAD
    {
        private readonly Contexto _elContexto;
        public EditarTelefonoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarTelefono(int id, TelefonoDto telefono)
        {
            var telefonoExistente = _elContexto.Telefonos.FirstOrDefault(t => t.Id == id);
            if (telefonoExistente != null)
            {
                telefonoExistente.Telefono = telefono.Telefono;
                telefonoExistente.Tipo = telefono.Tipo;
                telefonoExistente.Codigo = telefono.Codigo;
                telefonoExistente.Estado = telefono.Estado;

                EntityState estado = _elContexto.Entry(telefonoExistente).State = EntityState.Modified;
                int resultado = await _elContexto.SaveChangesAsync();
                return resultado;
            }
            else
            {
                throw new Exception("El teléfono no existe.");

            }
        }
    }
}
