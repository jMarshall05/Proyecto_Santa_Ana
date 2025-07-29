using System;
using System.Threading.Tasks;
using Campus.AccesoDatos.ModelosAD;
using System.Data.Entity;
using Campus.Abstracciones.AccesoDatos.calificaciones.eliminarCalificacionDA;

namespace Campus.AccesoDatos.calificaciones.eliminarCalificacionAD
{
    public class EliminarCalificacionAD : IEliminarCalificacion
    {
        private readonly Contexto _elContexto;

        public EliminarCalificacionAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EliminarCalificacion(int id_calificacion)
        {
            var calificacionExistente = await _elContexto.Cursos.FindAsync(id_calificacion);

            if (calificacionExistente == null)
            {
                throw new ArgumentException("La entrega especificada no existe");
            }

            _elContexto.Cursos.Remove(calificacionExistente);
            int resultado = await _elContexto.SaveChangesAsync();

            return resultado; // filas afectadas
        }
    }
}
