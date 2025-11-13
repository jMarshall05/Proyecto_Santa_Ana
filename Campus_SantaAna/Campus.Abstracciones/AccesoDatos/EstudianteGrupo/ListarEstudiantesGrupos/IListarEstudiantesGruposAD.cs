using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ListarEstudiantesGrupos
{
    public interface IListarEstudiantesGruposAD
    {
        List<EstudianteGrupoDto> ListarEstudiantesGrupos();
        List<EstudianteGrupoDto> ListarEstudiantesPorIdGrupo(int idGrupo);
        List<EstudianteGrupoDto> ListarGruposPorIdEstudiante(string idUsuario);

    }
}
