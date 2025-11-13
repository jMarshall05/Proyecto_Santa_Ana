using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.listarEntregaAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Entregas.ListarEntregaAD
{
    public class ListarEntregasAD : IListarEntregas
    {
        private readonly Contexto _elContexto;

        public ListarEntregasAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<List<EntregasDto>> ListarEntregas()
        {
            var lista = await _elContexto.Entregas
                .Select(e => new EntregasDto
                {
                    id_entrega = e.IdEntrega,
                    id_tarea = e.IdTarea,
                    id_estudiante = e.IdEstudiante,
                    archivo_entregado = e.ArchivoEntregado,
                    fecha_entrega = e.FechaEntrega,
                    estado = e.Estado
                })
                .ToListAsync();

            return lista;
        }

        public async Task<List<EntregasDto>> ListarEntregasPorGrupo(int idGrupo)
        {
            var tareasGrupo = await _elContexto.Tareas
                .Where(t => t.IdGrupo == idGrupo)
                .Select(t => t.IdTarea)
                .ToListAsync();

            var entregasGrupo = await _elContexto.Entregas
                .Where(e => tareasGrupo.Contains(e.IdTarea))
                .Select(e => new EntregasDto
                {
                    id_entrega = e.IdEntrega,
                    id_tarea = e.IdTarea,
                    id_estudiante = e.IdEstudiante,
                    archivo_entregado = e.ArchivoEntregado,
                    fecha_entrega = e.FechaEntrega,
                    estado = e.Estado
                })
                .ToListAsync();

            return entregasGrupo;
        }
        public async Task<List<EntregasDto>> ListarEntregasPorEstudiante(string idEstudiante)
        {
            var lista = await _elContexto.Entregas
                .Where(e => e.IdEstudiante == idEstudiante)
                .Select(e => new EntregasDto
                {
                    id_entrega = e.IdEntrega,
                    id_tarea = e.IdTarea,
                    id_estudiante = e.IdEstudiante,
                    archivo_entregado = e.ArchivoEntregado,
                    fecha_entrega = e.FechaEntrega,
                    estado = e.Estado
                })
                .ToListAsync();

            return lista;
        }

    }
}
