using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.eliminarTareaAD;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.tareas.eliminarTareaAD
{
    public class EliminarTareaAD : IEliminarTareaAD
    {
        private readonly Contexto _contexto;

        public EliminarTareaAD()
        {
            _contexto = new Contexto();
        }

        public async Task<int> EliminarTarea(int idTarea)
        {
            var tarea = _contexto.Tareas.FirstOrDefault(t => t.IdTarea == idTarea);
            if (tarea == null)
            {
                throw new Exception("La tarea no fue encontrada.");
            }
            tarea.Estado = false;
            _contexto.Entry(tarea).State = EntityState.Modified;
            return await _contexto.SaveChangesAsync();
        }
    }
}
