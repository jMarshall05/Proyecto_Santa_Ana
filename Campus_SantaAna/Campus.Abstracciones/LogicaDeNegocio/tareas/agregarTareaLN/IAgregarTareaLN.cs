using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN
{
    public interface IAgregarTareaLN
    {
        Task<int> AgregarTarea(TareaDto tarea);
    }

}
