using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Tareas.AgregarTareaAD;

namespace Campus.LogicaDeNegocio.tareas.agregarTareaLN
{
    public class AgregarTareaLN : IAgregarTareaLN
    {
        private IAgregarTarea _agregarTarea;

        public AgregarTareaLN()
        {
            _agregarTarea = new AgregarTareaAD();
        }

        public async Task<int> AgregarTarea(TareaDto tarea)
        {
            try
            {
                return await _agregarTarea.AgregarTarea(tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la tarea", ex);
            }
        }
    }
}
