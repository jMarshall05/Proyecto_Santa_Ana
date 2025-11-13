using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN
{
    public interface IEditarUsuarioLN
    {
        Task<int> EditarUsuarioAdmin(string id, UsuariosDto usuario);
        Task<int> EditarUsuario(string id, UsuariosDto usuario);

    }
}
