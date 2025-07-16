using Campus.AccesoDatos.ModelosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.AccesoDatos.Eventos;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.Eventos
{
    public class EventoAccesoDatos : IEventoAD
    {
        private readonly Contexto _contexto;

        public EventoAccesoDatos()
        {
            _contexto = new Contexto();
        }

        public async Task<int> AgregarEvento(EventoDto evento)
        {
            var nuevo = new EventoAD
            {
                Titulo = evento.Titulo,
                FechaInicio = evento.FechaInicio,
                FechaFin = evento.FechaFin
            };

            _contexto.Eventos.Add(nuevo);
            return await _contexto.SaveChangesAsync();
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
        public async Task<int> EditarEvento(EventoDto evento)
        {
            var entidad = await _contexto.Eventos.FindAsync(evento.Id);
            if (entidad == null) return 0;

            entidad.Titulo = evento.Titulo;
            entidad.FechaInicio = evento.FechaInicio;
            entidad.FechaFin = evento.FechaFin;

            return await _contexto.SaveChangesAsync();
        }

        public async Task<int> EliminarEvento(int id)
        {
            var entidad = await _contexto.Eventos.FindAsync(id);
            if (entidad == null) return 0;

            _contexto.Eventos.Remove(entidad);
            return await _contexto.SaveChangesAsync();
        }

    }
}
