using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using Campus.Abstracciones.AccesoDatos.Eventos.AgregarEventoAD;

namespace Campus.AccesoDatos.Eventos.AgregarEventoAD
{
    public class AgregarEventoAD : IAgregarEventoAD
    {
        private readonly Contexto _contexto;

        public AgregarEventoAD()
        {
            _contexto = new Contexto();
        }

        public async Task<int> AgregarEvento(EventoDto evento)
        {
            var entidad = new EventoAD
            {
                Titulo = evento.Titulo,
                FechaInicio = evento.FechaInicio,
                FechaFin = evento.FechaFin,
                IdUsuario = evento.IdUsuario // 👈 Ahora se guarda el usuario dueño del evento
            };

            _contexto.Eventos.Add(entidad);
            await _contexto.SaveChangesAsync();

            return entidad.Id; // Devuelve el ID generado por la base de datos
        }
    }
}
