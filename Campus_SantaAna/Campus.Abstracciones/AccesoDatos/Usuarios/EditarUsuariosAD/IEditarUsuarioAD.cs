using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Usuarios.EditarUsuariosAD
{
    public interface IEditarUsuarioAD
    {
        Task<int> EditarUsuarioAdmin(string id, UsuariosDto usuario);
        Task<int> EditarUsuario(string id, UsuariosDto usuario);

    }
}
