using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.entregas.listarEntregaAD
{
    public interface IListarEntregas
    {
        Task<List<EntregasDto>> ListarEntregas();
        Task<List<EntregasDto>> ListarEntregasPorGrupo(int idGrupo);
        Task<List<EntregasDto>> ListarEntregasPorEstudiante(string idEstudiante);

    }
}
