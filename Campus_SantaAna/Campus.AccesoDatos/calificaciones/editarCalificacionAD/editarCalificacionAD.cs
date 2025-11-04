using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.editarCalificacionAD;
using Campus.Abstracciones.ModelosUI;


namespace Campus.AccesoDatos.calificaciones.editarCalificacionAD
{
    public class EditarCalificacionAD : IEditarCalificacion
    {
        private readonly Contexto _elContexto;

        public EditarCalificacionAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> EditarCalificacion(int id, CalificacionesDto calificacion)
        {
            var calificacionExistente = await _elContexto.Calificaciones
                .FindAsync(calificacion.id_calificacion);

            if (calificacionExistente == null)
            {
                throw new ArgumentException("La calificación especificada no existe");
            }

            // Validar que la entrega exista
            var entregaExiste = await _elContexto.Entregas
                .AnyAsync(e => e.IdEntrega == calificacion.id_entrega);
            if (!entregaExiste)
            {
                throw new ArgumentException("La entrega especificada no existe");
            }


            // Actualizar campos
            calificacionExistente.Calificacion = calificacion.calificacion;
            calificacionExistente.Comentario = calificacion.comentario;
            calificacionExistente.FechaCalificacion = calificacion.fecha_calificacion < new DateTime(1753, 1, 1)
                ? DateTime.Now
                : calificacion.fecha_calificacion;

            _elContexto.Entry(calificacionExistente).State = EntityState.Modified;
            int resultado = await _elContexto.SaveChangesAsync();

            return resultado;
        }

    }
}
