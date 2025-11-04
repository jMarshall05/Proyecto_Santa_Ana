using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.AgregarUsuariosLN
{
    public interface IAgregarUsuariosLN
    {
        Task<int> AgregarUsuario(UsuariosDto usuario);
    }
}
