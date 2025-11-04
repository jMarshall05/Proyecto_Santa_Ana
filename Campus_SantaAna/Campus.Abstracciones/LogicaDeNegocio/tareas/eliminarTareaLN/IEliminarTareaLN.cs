using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN
{
    public interface IEliminarTareaLN
    {
        Task<int> EliminarTarea(int idTarea);
    }
}
