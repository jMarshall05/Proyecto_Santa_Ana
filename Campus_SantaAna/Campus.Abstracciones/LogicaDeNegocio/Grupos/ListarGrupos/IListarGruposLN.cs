using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos
{
    public interface IListarGruposLN
    {
        IEnumerable<GruposDto> ListarGrupos();
        GruposDto BuscarGruposPorId(int idGrupo);

    }
}
