using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN
{
    public interface IEditarUsuarioLN
    {
        Task<int> EditarUsuario(string id, UsuariosDto usuario);
    }
}
