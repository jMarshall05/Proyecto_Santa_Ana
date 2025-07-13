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
                    id_grupo = t.IdGrupo, // ← si lo tenés en el DTO
                    nombre_grupo = t.Grupo != null ? t.Grupo.nombre_grupo : "Sin grupo"
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
                ArchivoAdjunto = tarea.ArchivoAdjunto,
                nombre_grupo = tarea.Grupo != null ? tarea.Grupo.nombre_grupo : "Sin grupo"
            };
        }



        // Implementación para listar grupos
        // NUEVO MÉTODO para traer solo los grupos directamente de la tabla Grupos
        public async Task<IEnumerable<GruposDto>> ListarGruposAsync()
        {
            return await _contexto.Grupos
                .Select(g => new GruposDto
                {
                    id_grupo = g.id_grupo,
                    nombre_grupo = g.nombre_grupo
                })
                .ToListAsync();
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
                    ArchivoAdjunto = t.ArchivoAdjunto,
                    nombre_grupo = t.Grupo != null ? t.Grupo.nombre_grupo : "Sin grupo"
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
                    id_grupo = t.IdGrupo,
                    nombre_grupo = t.Grupo != null ? t.Grupo.nombre_grupo : "Sin grupo"
                })
                .ToListAsync();

            return tareas;
        }



    }
}
