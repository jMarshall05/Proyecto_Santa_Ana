using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.tareas.eliminarTareaAD
{
    public interface IEliminarTareaAD
    {
        Task<int> EliminarTarea(int idTarea);
    }
}
