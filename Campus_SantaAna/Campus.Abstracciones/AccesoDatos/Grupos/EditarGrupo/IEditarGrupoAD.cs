using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Grupos.EditarGrupo
{
    public interface IEditarGrupoAD
    {
        Task<int> EditarGrupo(int id, GruposDto grupo);
    }
}
