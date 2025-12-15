using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.EditarEventoAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Eventos.EditarEventoAD
{
    public class EditarEventoAD : IEditarEventoAD
    {
        private readonly Contexto _contexto;

        public EditarEventoAD()
        {
            _contexto = new Contexto();
        }

        public async Task<int> EditarEvento(EventoDto evento)
        {
            var entidad = await _contexto.Eventos.FindAsync(evento.Id);
            if (entidad == null) return 0;

            entidad.Titulo = evento.Titulo;
            entidad.FechaInicio = evento.FechaInicio;
            entidad.FechaFin = evento.FechaFin;

            _contexto.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            return await _contexto.SaveChangesAsync();
        }
    }
}
