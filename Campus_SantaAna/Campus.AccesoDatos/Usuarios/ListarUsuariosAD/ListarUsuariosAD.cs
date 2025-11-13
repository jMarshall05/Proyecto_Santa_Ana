using System.Collections.Generic;
using System.Linq;
using Campus.Abstracciones.AccesoDatos.Usuarios.ListarUsuariosAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Usuarios.ListarUsuariosAD
{
    public class ListarUsuariosAD : IListarUsuariosAD
    {
        private readonly Contexto _elContexto;
        public ListarUsuariosAD()
        {
            _elContexto = new Contexto();
        }

        public List<UsuariosDto> ListarUsuarios()
        {
            List<UsuariosDto> ListaDeUsuarios = (from Usuarios in _elContexto.Usuarios
                                                 select new UsuariosDto
                                                 {
                                                     IdUsuario = Usuarios.IdUsuario,
                                                     Nombre = Usuarios.Nombre,
                                                     Apellido = Usuarios.Apellido,
                                                     Email = Usuarios.Email,
                                                     FechaDeNacimiento = Usuarios.FechaDeNacimiento,
                                                     Cedula = Usuarios.Cedula,
                                                     FechaDeRegistro = Usuarios.FechaDeRegistro,
                                                     FechaDeModificacion = Usuarios.FechaDeModificacion,
                                                     Rol = Usuarios.Rol,
                                                     Estado = Usuarios.Estado
                                                 }).ToList();
            return ListaDeUsuarios;
        }
    }
}
