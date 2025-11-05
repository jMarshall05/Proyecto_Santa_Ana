using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos
{
    public interface IListarGruposLN
    {
        IEnumerable<GruposDto> ListarGrupos();
        GruposDto BuscarGruposPorId(int idGrupo);

    }
}
