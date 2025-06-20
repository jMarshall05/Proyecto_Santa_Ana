using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN
{
    public interface IActualizarEstudianteGrupoLN 
    {
        Task<int> ActualizarEstudianteGrupo(EstudianteGrupoDto estudianteGrupo);
    }
}
