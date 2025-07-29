using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.ListarEventosad;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;

namespace Campus.AccesoDatos.Eventos.ListarEventosAD
{
    public class ListarEventosAD : IListarEventosAD
    {
        private readonly Contexto _contexto;

        public ListarEventosAD()
        {
            _contexto = new Contexto();
        }

        public async Task<List<EventoDto>> ListarEventos()
        {
            return await _contexto.Eventos
                .Select(e => new EventoDto
                {
                    Id = e.Id,
                    Titulo = e.Titulo,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin
                })
                .ToListAsync();
        }
    }
}
