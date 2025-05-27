using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.EditarUsuariosAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Usuarios.EditarUsuariosAD;

namespace Campus.LogicaDeNegocio.Usuarios.EditarUsuarios
{
    public class EditarUsuariosLN : IEditarUsuarioLN
    {
        private IEditarUsuarioAD _editarUsuario;
        public EditarUsuariosLN()
        {
            _editarUsuario = new EditarUsuarioAD();
        }
        public Task<int> EditarUsuario(string id, UsuariosDto usuario)
        {
            return _editarUsuario.EditarUsuario(id, usuario);
        }
    }
}
