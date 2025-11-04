using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN
{
    public interface IBuscarEstudianteGrupoPorIdLN
    {
        EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante);
        List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo);
    }
}
