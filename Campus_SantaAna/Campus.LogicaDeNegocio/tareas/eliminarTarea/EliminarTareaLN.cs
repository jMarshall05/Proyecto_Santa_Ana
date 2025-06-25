using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.eliminarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN;

namespace Campus.LogicaDeNegocio.Tareas.EliminarTareaLN
{
    public class EliminarTareaLN : IEliminarTareaLN
    {
        private readonly IEliminarTarea _eliminarTareaAD;

        public EliminarTareaLN(IEliminarTarea eliminarTareaAD)
        {
            _eliminarTareaAD = eliminarTareaAD;
        }

        public async Task<int> EliminarTarea(int idTarea)
        {
            try
            {
                if (idTarea <= 0)
                    throw new ArgumentException("ID de tarea no válido");

                return await _eliminarTareaAD.EliminarTarea(idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la tarea: " + ex.Message, ex);
            }
        }
    }
}