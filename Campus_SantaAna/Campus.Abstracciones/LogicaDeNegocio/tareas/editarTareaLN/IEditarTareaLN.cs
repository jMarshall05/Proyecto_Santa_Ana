using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.editarTareaLN
{
    public interface IEditarTareaLN
    {
        Task<int> EditarTarea(int id, TareaDto tarea);
    }

}
