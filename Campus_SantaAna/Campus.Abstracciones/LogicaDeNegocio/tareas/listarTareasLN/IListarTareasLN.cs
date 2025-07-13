using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN
{
    public interface IListarTareaLN
    {
        Task<IEnumerable<TareaDto>> ListarTareasAsync();
        Task<IEnumerable<TareaDto>> ListarTareasPorGrupoAsync(int idGrupo);
        Task<TareaDto> ObtenerPorIdAsync(int idTarea);
        Task<IEnumerable<GruposDto>> ListarGruposAsync();
        Task<List<TareaDto>> ListarTareasPorEstudiante(string idEstudiante);



    }
}
