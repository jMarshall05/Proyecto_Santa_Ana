using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.tareas.listarTareaAD
{
    public class ListarTareaAD : IListarTarea
    {
        private readonly Contexto _contexto;

        public ListarTareaAD()
        {
            _contexto = new Contexto();
        }

        public async Task<IEnumerable<TareaDto>> ListarTareasAsync()
        {
            return await _contexto.Tareas
                .Select(t => new TareaDto
                {
                    IdTarea = t.IdTarea,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaEntrega = t.FechaEntrega,
                    FechaPublicacion = t.FechaPublicacion,
                    ArchivoAdjunto = t.ArchivoAdjunto
                })
                .ToListAsync();
        }

        public async Task<TareaDto> ObtenerPorIdAsync(int idTarea)
        {
            var tarea = await _contexto.Tareas.FirstOrDefaultAsync(t => t.IdTarea == idTarea);

            if (tarea == null)
                return null;

            return new TareaDto
            {
                IdTarea = tarea.IdTarea,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaEntrega = tarea.FechaEntrega,
                FechaPublicacion = tarea.FechaPublicacion
            };
        }
    }
}
