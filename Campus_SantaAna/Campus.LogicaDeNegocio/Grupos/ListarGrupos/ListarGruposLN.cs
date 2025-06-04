using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Grupos.ListarGrupos;

namespace Campus.LogicaDeNegocio.Grupos.ListarGrupos
{
    public class ListarGruposLN : IListarGruposLN
    {
        private IListarGruposAD _listarGrupos;
        public ListarGruposLN()
        {
            _listarGrupos = new ListarGruposAD();
        }

        public GruposDto BuscarGruposPorUsuario(int idGrupo)
        {
            return _listarGrupos.BuscarGruposPorUsuario(idGrupo);
        }

        public List<GruposDto> ListarGrupos()
        {
            return _listarGrupos.ListarGrupos();
        }
    }
}
