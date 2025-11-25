using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Grupos.ListarGrupos
{
    public interface IListarGruposAD
    {
        List<GruposDto> ListarGrupos();
        GruposDto BuscarGruposPorId(int idGrupo);
    }
}
