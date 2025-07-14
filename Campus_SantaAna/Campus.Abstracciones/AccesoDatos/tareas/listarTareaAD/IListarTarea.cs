using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD
{
    public interface IListarTarea
    {
        Task<IEnumerable<TareaDto>> ListarTareasAsync();
        Task<TareaDto> ObtenerPorIdAsync(int idTarea);
        Task<IEnumerable<GruposDto>> ListarGruposAsync();
        Task<List<TareaDto>> ListarTareasPorEstudiante(string idEstudiante);
    }
}
