using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD
{
    public interface IAgregarTarea
    {
        Task<int> AgregarTarea(TareaDto tarea);
    }
}
