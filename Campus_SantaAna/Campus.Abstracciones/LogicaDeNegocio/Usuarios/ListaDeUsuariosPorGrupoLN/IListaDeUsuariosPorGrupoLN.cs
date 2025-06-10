using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListaDeUsuariosPorGrupoLN
{
    public interface IListaDeUsuariosPorGrupoLN
    {
        List<UsuariosDto> ObtenerUsuariosPorGrupo(int idGrupo);
    }
}
