using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.editarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

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

            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.IdMateria = tarea.IdMateria;

            _elContexto.Entry(tareaExistente).State = System.Data.Entity.EntityState.Modified;
            return await _elContexto.SaveChangesAsync();
        }
    }
}
