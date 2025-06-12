using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN
{
    public interface IBuscarEstudianteGrupoPorILN
    {
        Task <int> BuscarEstudianteGrupoPorEstudianteId(string idEstudiante);
        Task<int> BuscarEstudianteGrupoPorGrupoId(int idGrupo);
    }
}
