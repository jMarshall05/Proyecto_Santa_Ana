using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.editarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;

namespace Campus.AccesoDatos.Tareas.EditarTareaAD
{
    public class EditarTareaAD : IEditarTarea
    {
        private readonly Contexto _elContexto;

        public EditarTareaAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarTarea(int id, TareaDto tarea)
        {
            var tareaExistente = await _elContexto.Tareas.FindAsync(id);
            if (tareaExistente == null) return 0;

            // Actualiza todos los campos incluyendo las fechas
            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.FechaEntrega = tarea.FechaEntrega;
            tareaExistente.ArchivoAdjunto = tarea.ArchivoAdjunto;
            tareaExistente.FechaModificacion = tarea.FechaModificacion;
            tareaExistente.FechaPublicacion = tarea.FechaPublicacion;

            // Mantenemos la fecha original de creación
            // tareaExistente.FechaCreacion no se modifica

            _elContexto.Entry(tareaExistente).State = EntityState.Modified;
            return await _elContexto.SaveChangesAsync();
        }
    }
}