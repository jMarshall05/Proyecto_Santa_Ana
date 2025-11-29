using System.Linq;
using Campus.Abstracciones.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD
{
    public class ObtenerUsuariosPorIdAD : IObtenerUsuariosPorIdAD
    {
        private readonly Contexto _elContexto;
        public ObtenerUsuariosPorIdAD()
        {
            _elContexto = new Contexto();
        }

        public UsuariosDto ObtenerUsuarioPorId(string idUsuario)
        {
            var usuario = _elContexto.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario != null)
            {
                return new UsuariosDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    FechaDeNacimiento = usuario.FechaDeNacimiento,
                    Identificacion = usuario.Identificacion,
                    FechaDeRegistro = usuario.FechaDeRegistro,
                    FechaDeModificacion = usuario.FechaDeModificacion,
                    Rol = usuario.Rol,
                    Estado = usuario.Estado,
                    TipoIdentificacion = usuario.TipoIdentificacion
                };
            }
            return null;
        }

    }
}
