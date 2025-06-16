using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIdAD
{
    public interface IBuscarEstudianteGrupoPorIAD
    {
        EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante);
        List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo);
    }
}
