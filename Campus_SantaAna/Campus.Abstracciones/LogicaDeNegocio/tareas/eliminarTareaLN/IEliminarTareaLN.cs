using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN
{
    public interface IEliminarTareaLN
    {
        Task<int> EliminarTarea(int idTarea);
    }
}
