using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN
{
    public interface IObtenerUsuariosPorIdLN
    {
        UsuariosDto ObtenerUsuarioPorId(string idUsuario);
    }
}
