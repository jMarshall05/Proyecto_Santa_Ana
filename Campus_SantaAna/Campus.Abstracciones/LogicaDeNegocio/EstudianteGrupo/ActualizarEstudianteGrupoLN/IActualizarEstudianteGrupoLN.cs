using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN
{
    public interface IActualizarEstudianteGrupoLN
    {
        Task<int> ActualizarEstudianteGrupo(EstudianteGrupoDto estudianteGrupo);
    }
}
