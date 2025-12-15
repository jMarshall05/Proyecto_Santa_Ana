using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.agregarCalificacionAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.calificaciones.agregarCalificacionAD
{
    public class AgregarCalificacionAD : IAgregarCalificacion
    {
        private readonly Contexto _elContexto;

        public AgregarCalificacionAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarCalificacion(CalificacionesDto calificacionDto)
        {
            var entregaExiste = await _elContexto.Entregas.AnyAsync(e => e.IdEntrega == calificacionDto.id_entrega);
            if (!entregaExiste)
            {
                throw new ArgumentException("La entrega no existe.");
            }

            var calificacionAD = new CalificacionesAD
            {
                IdEntrega = calificacionDto.id_entrega,
                Calificacion = calificacionDto.calificacion,
                Comentario = calificacionDto.comentario,
                FechaCalificacion = DateTime.Now,
                Estado = true
            };

            _elContexto.Calificaciones.Add(calificacionAD);
            await _elContexto.SaveChangesAsync();

            return calificacionAD.IdCalificacion;
        }
    }
}