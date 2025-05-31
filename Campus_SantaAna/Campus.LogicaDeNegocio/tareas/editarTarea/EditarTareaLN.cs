using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.editarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.editarTareaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Tareas.EditarTareaAD;

namespace Campus.LogicaDeNegocio.Tareas.EditarTareaLN
{
    public class EditarTareaLN : IEditarTareaLN
    {
        private readonly IEditarTarea _editarTareaAD;

        public EditarTareaLN()
        {
            _editarTareaAD = new EditarTareaAD();
        }

        public async Task<int> EditarTarea(int id, TareaDto tarea)
        {
            try
            {
                return await _editarTareaAD.EditarTarea(id, tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la tarea", ex);
            }
        }
    }
}
