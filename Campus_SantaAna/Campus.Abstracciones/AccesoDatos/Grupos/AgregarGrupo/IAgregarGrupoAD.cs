using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Grupos.AgregarGrupo
{
    public interface IAgregarGrupoAD
    {
        Task<int> AgregarGrupo(GruposDto grupo);
    }
}
