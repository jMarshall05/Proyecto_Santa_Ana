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
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(tarea.Titulo))
                    throw new ArgumentException("El título de la tarea es requerido");

                if (tarea.FechaEntrega < DateTime.Now)
                    throw new ArgumentException("La fecha de entrega no puede ser en el pasado");

                if (tarea.Id_grupo <= 0)
                    throw new ArgumentException("El ID de grupo no es válido");

                tarea.FechaModificacion = DateTime.Now;

                return await _editarTareaAD.EditarTarea(id, tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la tarea: " + ex.Message, ex);
            }
        }
    }
}
