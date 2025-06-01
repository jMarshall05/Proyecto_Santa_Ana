using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.eliminarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN;
using Campus.AccesoDatos.tareas.eliminarTareaAD;

namespace Campus.LogicaDeNegocio.Tareas.EliminarTareaLN
{
    public class EliminarTareaLN : IEliminarTareaLN
    {
        private readonly IEliminarTarea _eliminarTareaAD;

        public EliminarTareaLN()
        {
            _eliminarTareaAD = new EliminarTareaAD();
        }

        public async Task<int> EliminarTarea(int idTarea)
        {
            try
            {
                return await _eliminarTareaAD.EliminarTarea(idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la tarea", ex);
            }
        }
    }
}
