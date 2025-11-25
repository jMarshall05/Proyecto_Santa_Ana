using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD
{
    public interface IAgregarTarea
    {
        Task<int> AgregarTarea(TareaDto tarea);
    }
}
