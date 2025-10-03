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

        public async Task<int> AgregarTelefono(TelefonoDto telefono)
        {
            var telefonoAD = ConvertirAD(telefono);
            _elContexto.Telefonos.Add(telefonoAD);
            var resultado =await _elContexto.SaveChangesAsync();
            return resultado;
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
