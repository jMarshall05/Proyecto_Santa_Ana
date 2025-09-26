using System.Collections.Generic;
using System.Data.Entity; // EF6
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.ListarEventosad;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Eventos.ListarEventosAD
{
    public class ListarEventosAD : IListarEventosAD
    {
        private readonly Contexto _contexto;

        public ListarEventosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<EventoDto>> ListarEventos(string idUsuario)
        {
            return await _contexto.Eventos
                .Where(e => e.IdUsuario == idUsuario)
                .Select(e => new EventoDto
                {
                    Id = e.Id,
                    Titulo = e.Titulo,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    IdUsuario = e.IdUsuario
                })
                .ToListAsync();
        }
    }
}
