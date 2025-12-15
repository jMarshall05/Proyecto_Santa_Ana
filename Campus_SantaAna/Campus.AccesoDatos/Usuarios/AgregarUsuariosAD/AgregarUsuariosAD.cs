using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.AgregarUsuariosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Usuarios.AgregarUsuariosAD
{
    public class AgregarUsuariosAD : IAgregarUsuariosAD
    {
        private readonly Contexto _elContexto;
        public AgregarUsuariosAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarUsuario(UsuariosDto usuario)
        {
            var UsuarioTranformado = ConvertirAD(usuario);
            _elContexto.Usuarios.Add(UsuarioTranformado);
            _ = _elContexto.Entry(UsuarioTranformado).State = System.Data.Entity.EntityState.Added;
            int Resultado = await _elContexto.SaveChangesAsync();
            return Resultado;

        }

        private UsuariosAD ConvertirAD(UsuariosDto usuario)
        {
            return new UsuariosAD
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                FechaDeNacimiento = usuario.FechaDeNacimiento,
                Identificacion = usuario.Identificacion,
                FechaDeRegistro = usuario.FechaDeRegistro,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                TipoIdentificacion = usuario.TipoIdentificacion

            };
        }


    }
}
