using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupoLN
{
    public interface IListarEstudianteGrupoLN
    {
        List<EstudianteGrupoDto> ListarEstudianteGrupo();
        List<EstudianteGrupoDto> ListarEstudiantesPorIdGrupo(int idGrupo);
        List<EstudianteGrupoDto> ListarGruposPorIdEstudiante(string idUsuario);
    }
}
