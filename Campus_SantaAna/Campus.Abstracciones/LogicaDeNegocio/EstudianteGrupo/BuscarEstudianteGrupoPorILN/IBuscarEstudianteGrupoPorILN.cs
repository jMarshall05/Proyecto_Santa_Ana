using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN
{
    public interface IBuscarEstudianteGrupoPorIdLN
    {
        EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante);
        List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo);
    }
}
