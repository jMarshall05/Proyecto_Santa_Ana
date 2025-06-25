using Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(t => t.Grupo)
                .Select(t => new TareaDto
                {
                    IdTarea = t.IdTarea,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaEntrega = t.FechaEntrega,
                    FechaPublicacion = t.FechaPublicacion,
                    ArchivoAdjunto = t.ArchivoAdjunto,
                    id_grupo = t.IdGrupo
                })

                .ToListAsync();
        }

        public async Task<TareaDto> ObtenerPorIdAsync(int idTarea)
        {
            var tarea = await _contexto.Tareas
                .Include(t => t.Grupo)
                .FirstOrDefaultAsync(t => t.IdTarea == idTarea);

            if (tarea == null) return null;

            return new TareaDto
            {
                IdTarea = tarea.IdTarea,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaEntrega = tarea.FechaEntrega,
                FechaPublicacion = tarea.FechaPublicacion,
                ArchivoAdjunto = tarea.ArchivoAdjunto
            };
        }

        public async Task<IEnumerable<TareaDto>> ListarTareasPorGrupoAsync(int idGrupo)
        {
            return await _contexto.Tareas
                .Include(t => t.Grupo)
                .Where(t => t.IdGrupo == idGrupo)
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


    }
}
