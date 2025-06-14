using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
