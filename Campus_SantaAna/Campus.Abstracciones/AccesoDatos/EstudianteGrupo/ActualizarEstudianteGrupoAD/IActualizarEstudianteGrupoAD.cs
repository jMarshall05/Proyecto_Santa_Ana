using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD
{
    public interface IActualizarEstudianteGrupoAD
    {
        Task<int> ActualizarEstudianteGrupo(EstudianteGrupoDto estudiante);
    }
}
