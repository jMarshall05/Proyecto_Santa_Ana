using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN
{
    public interface IListarUsuariosLN
    {
        List<UsuariosDto> ListarUsuarios();
    }
}
