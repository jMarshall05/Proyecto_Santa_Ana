using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN
{
    public interface IObtenerUsuariosPorIdLN
    {
        UsuariosDto ObtenerUsuarioPorId(string idUsuario);
    }
}
