using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN
{
    public interface IAgregarTareaLN
    {
        Task<int> AgregarTarea(TareaDto tarea);
    }

}
