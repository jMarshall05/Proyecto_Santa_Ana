using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Eventos.EliminarEventoAD;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Eventos.EliminarEventoAD
{
    public class EliminarEventoAD : IEliminarEventoAD
    {
        private readonly Contexto _contexto;

        public EliminarEventoAD()
        {
            _contexto = new Contexto();
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
