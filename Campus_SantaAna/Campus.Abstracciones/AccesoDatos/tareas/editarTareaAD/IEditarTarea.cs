using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Campus.Abstracciones.ModelosUI;
namespace Campus.Abstracciones.AccesoDatos.tareas.editarTareaAD
{
    public interface IEditarTarea
    {
        Task<int> EditarTarea(int id, TareaDto tarea);
    }
}
