using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.agregarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.tareas.listarTareaAD;
using Campus.AccesoDatos.Tareas.AgregarTareaAD;

namespace Campus.LogicaDeNegocio.tareas.agregarTareaLN
{
    public class AgregarTareaLN : IAgregarTareaLN
    {
        private readonly IAgregarTarea _agregarTarea;

        public AgregarTareaLN()
        {
            _agregarTarea = new AgregarTareaAD();
        }

        public async Task<int> AgregarTarea(TareaDto tarea)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(tarea.Titulo))
                    throw new ArgumentException("El título de la tarea es requerido");

                if (tarea.FechaEntrega < DateTime.Now)
                    throw new ArgumentException("La fecha de entrega no puede ser en el pasado");

                return await _agregarTarea.AgregarTarea(tarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la tarea: " + ex.Message, ex);
            }
        }
    }
}