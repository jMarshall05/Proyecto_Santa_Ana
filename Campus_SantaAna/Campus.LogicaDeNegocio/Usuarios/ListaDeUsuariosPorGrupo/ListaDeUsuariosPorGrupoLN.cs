using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.ListaDeUsuariosPorGrupoAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListaDeUsuariosPorGrupoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Usuarios.ListaDeUsuariosPorGrupoAD;

namespace Campus.LogicaDeNegocio.Usuarios.ListaDeUsuariosPorGrupo
{
    public class ListaDeUsuariosPorGrupoLN : IListaDeUsuariosPorGrupoLN
    {
        private IListaDeUsuariosPorGrupoAD _listaDeUsuariosPorGrupo;
        public ListaDeUsuariosPorGrupoLN()
        {
            _listaDeUsuariosPorGrupo = new ListaDeUsuariosPorGrupoAD();
        }
        public List<UsuariosDto> ObtenerUsuariosPorGrupo(int idGrupo)
        {
            return _listaDeUsuariosPorGrupo.ObtenerUsuariosPorGrupo(idGrupo);
        }
    }
}
