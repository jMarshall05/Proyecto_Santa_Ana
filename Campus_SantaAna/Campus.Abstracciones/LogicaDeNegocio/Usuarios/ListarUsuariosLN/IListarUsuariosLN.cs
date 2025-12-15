using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN
{
    public interface IListarUsuariosLN
    {
        List<UsuariosDto> ListarUsuarios();
    }
}
