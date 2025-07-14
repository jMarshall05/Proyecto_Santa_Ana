using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN
{
    public interface IListarEntregasLN
    {
        Task<List<EntregasDto>> ListarEntregas();
        Task<List<EntregasDto>> ListarEntregasPorGrupoAsync(int idGrupo);
        Task<List<EntregasDto>> ListarEntregasPorEstudianteAsync(string idEstudiante);

    }
}
