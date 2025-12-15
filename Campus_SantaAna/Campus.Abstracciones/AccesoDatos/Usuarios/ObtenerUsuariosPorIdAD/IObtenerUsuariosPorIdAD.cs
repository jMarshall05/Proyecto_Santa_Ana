using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD
{
    public interface IObtenerUsuariosPorIdAD
    {
        UsuariosDto ObtenerUsuarioPorId(string idUsuario);
    }
}
