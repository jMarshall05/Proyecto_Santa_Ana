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
