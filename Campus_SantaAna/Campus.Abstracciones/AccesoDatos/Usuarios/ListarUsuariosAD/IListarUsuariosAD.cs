using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Usuarios.ListarUsuariosAD
{
    public interface IListarUsuariosAD
    {
        List<UsuariosDto> ListarUsuarios();
    }
}
