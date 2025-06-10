using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.ListaDeUsuariosPorGrupoAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Usuarios.ListaDeUsuariosPorGrupoAD
{
    public class ListaDeUsuariosPorGrupoAD : IListaDeUsuariosPorGrupoAD
    {
        private Contexto _elContexto;
        public ListaDeUsuariosPorGrupoAD()
        {
            _elContexto = new Contexto();
        }

        public List<UsuariosDto> ObtenerUsuariosPorGrupo(int idGrupo)
        {
           List<UsuariosDto> listaUsuarios = (from usuario in _elContexto.Usuarios
                                              where usuario.Id_grupo == idGrupo
                                              select new UsuariosDto
                                              {
                                                  Nombre = usuario.Nombre,
                                                  Apellido = usuario.Apellido,
                                                  Email = usuario.Email,
                                                  Telefono = usuario.Telefono,
                                                  Cedula = usuario.Cedula,
                                                  Rol = usuario.Rol,
                                              }).ToList();
            return listaUsuarios;
        }
    }
}
