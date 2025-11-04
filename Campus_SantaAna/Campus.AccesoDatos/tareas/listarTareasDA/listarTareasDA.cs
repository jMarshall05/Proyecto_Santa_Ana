using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
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
                .Include(t => t.Grupo)
                .Select(t => new TareaDto
                {
                    IdTarea = t.IdTarea,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaEntrega = t.FechaEntrega,
                    FechaPublicacion = t.FechaPublicacion,
                    ArchivoAdjunto = t.ArchivoAdjunto,
                    Id_grupo = t.IdGrupo,
                    Nombre_grupo = t.Grupo.nombre_grupo,
                    IdMateria = t.id_materia,
                    Estado = t.Estado
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
                IdMateria = tarea.id_materia,
                FechaEntrega = tarea.FechaEntrega,
                FechaPublicacion = tarea.FechaPublicacion,
                FechaModificacion = tarea.FechaModificacion,
                ArchivoAdjunto = tarea.ArchivoAdjunto,
                Id_grupo = tarea.IdGrupo,
                Nombre_grupo = tarea.Grupo.nombre_grupo,
                asignado_por = tarea.asignado_por,
                Estado = tarea.Estado

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

        public async Task<List<TareaDto>> ListarTareasPorEstudiante(string idEstudiante)
        {
            var gruposEstudiante = await _contexto.EstudianteGrupos
                .Where(eg => eg.EstudianteId == idEstudiante)
                .Select(eg => eg.GrupoId)
                .ToListAsync();

            if (gruposEstudiante == null || !gruposEstudiante.Any())
                return new List<TareaDto>();

            var tareas = await _contexto.Tareas
                .Include(t => t.Grupo)
                .Where(t => gruposEstudiante.Contains(t.IdGrupo))
                .Select(t => new TareaDto
                {
                    IdTarea = t.IdTarea,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaEntrega = t.FechaEntrega,
                    FechaPublicacion = t.FechaPublicacion,
                    ArchivoAdjunto = t.ArchivoAdjunto,
                    Id_grupo = t.IdGrupo,
                    IdMateria = t.id_materia,
                    Nombre_grupo = t.Grupo != null ? t.Grupo.nombre_grupo : "Sin grupo",
                    Calificacion = _contexto.Entregas
                        .Where(e => e.IdTarea == t.IdTarea && e.IdEstudiante == idEstudiante)
                        .SelectMany(e => _contexto.Calificaciones
                            .Where(c => c.IdEntrega == e.IdEntrega) 
                            .Select(c => new CalificacionesDto
                            {
                                id_calificacion = c.IdCalificacion, 
                                id_entrega = c.IdEntrega,
                                calificacion = c.Calificacion,
                                comentario = c.Comentario,
                                fecha_calificacion = c.FechaCalificacion
                            }))
                        .FirstOrDefault()
                })
                .ToListAsync();

            return tareas;
        }



    }
}
