using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIdAD
{
    public interface IBuscarEstudianteGrupoPorIAD
    {
        EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante);
        List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo);
    }
}
