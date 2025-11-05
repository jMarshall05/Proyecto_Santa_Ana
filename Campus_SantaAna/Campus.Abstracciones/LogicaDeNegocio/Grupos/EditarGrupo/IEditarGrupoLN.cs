using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Grupos.EditarGrupo
{
    public interface IEditarGrupoLN
    {
        Task<int> EditarGrupo(int id, GruposDto grupo);
    }
}
