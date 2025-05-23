using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD
{
    public class ObtenerUsuariosPorIdAD : IObtenerUsuariosPorIdAD
    {
        private Contexto _elContexto;
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
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    FechaDeNacimiento = usuario.FechaDeNacimiento,
                    Cedula = usuario.Cedula,
                    FechaDeRegistro = usuario.FechaDeRegistro,
                    FechaDeModificacion = usuario.FechaDeModificacion,
                    Rol = usuario.Rol,
                    Estado = usuario.Estado

                };
            }
            return null;
        }

    }
}
