using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Telefonos.AgregarTelefono;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Telefonos.AgregarTelefonoAD
{
    public class AgregarTelefonoAD : IAgregarTelefonoAD
    {
        private readonly Contexto _elContexto;
        public AgregarTelefonoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarTelefono(List<TelefonoDto> telefono)
        {
            int cambios = 0;
            foreach (var tel in telefono)
            {
                var telefonoAD = ConvertirAD(tel);
                
                _elContexto.Telefonos.Add(telefonoAD);
            }

            cambios = await _elContexto.SaveChangesAsync();
            return cambios;
        }

        private TelefonoAD ConvertirAD(TelefonoDto telefono)
        {
            return new TelefonoAD
            {
                IdUsuario = telefono.IdUsuario,
                Telefono = telefono.Telefono,
                Codigo = telefono.Codigo,
                Tipo = telefono.Tipo,
                Estado = telefono.Estado
            };
        }
    }
}
