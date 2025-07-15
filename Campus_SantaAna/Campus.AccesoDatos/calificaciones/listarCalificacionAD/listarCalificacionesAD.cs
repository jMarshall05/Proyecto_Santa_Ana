using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.listarCalificacionDA;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.calificaciones.listarCalificacionAD
{
    public class ListarCalificacionesAD : IListarCalificaciones
    {
        private readonly Contexto _elContexto;

        public ListarCalificacionesAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<List<CalificacionesDto>> ListarCalificaciones()
        {
            var lista = await _elContexto.Calificaciones
                .Select(e => new CalificacionesDto
                {
                    id_entrega = e.IdEntrega,
                    calificacion = e.Calificacion,
                    fecha_calificacion = e.FechaCalificacion,
                })
                .ToListAsync();

            return lista;
        }

     

        public async Task<List<CalificacionesDto>> ListarCalificacionesPorGrupo(int idGrupo)
        {
            // Obtener IDs de tareas del grupo
            var tareasGrupo = await _elContexto.Tareas
                .Where(t => t.IdGrupo == idGrupo)
                .Select(t => t.IdTarea)
                .ToListAsync();

            // Obtener calificaciones relacionadas a entregas de esas tareas
            var calificacionesGrupo = await _elContexto.Calificaciones
                .Where(c => tareasGrupo.Contains(c.IdEntrega))
                .Select(c => new CalificacionesDto
                {
                    id_calificacion = c.IdCalificacion,
                    id_entrega = c.IdEntrega,
                    calificacion = c.Calificacion,
                    comentario = c.Comentario,
                    fecha_calificacion = c.FechaCalificacion
                })
                .ToListAsync();

            return calificacionesGrupo;
        }

        public async Task<List<CalificacionesDto>> ListarCalificacionesPorEstudiante(string idEstudiante)
        {
            // Obtener lista de entregas del estudiante
            var entregasEstudiante = await _elContexto.Entregas
                .Where(e => e.IdEstudiante == idEstudiante)
                .Select(e => e.IdEntrega)
                .ToListAsync();

            // Obtener calificaciones que correspondan a esas entregas
            var lista = await _elContexto.Calificaciones
                .Where(c => entregasEstudiante.Contains(c.IdEntrega))
                .Select(c => new CalificacionesDto
                {
                    id_calificacion = c.IdCalificacion,
                    id_entrega = c.IdEntrega,
                    calificacion = c.Calificacion,
                    comentario = c.Comentario,
                    fecha_calificacion = c.FechaCalificacion,
                })
                .ToListAsync();

            return lista;
        }
    }
    }

