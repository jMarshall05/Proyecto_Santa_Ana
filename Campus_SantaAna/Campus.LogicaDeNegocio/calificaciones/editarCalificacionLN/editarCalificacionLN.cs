using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.editarCalificacionAD;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.calificaciones.editarCalificacionAD;

namespace Campus.LogicaDeNegocio.calificaciones
{
    public class EditarCalificacionLN : IEditarCalificacionLN
    {
        private readonly IEditarCalificacion _editarCalificacionAD;

        public EditarCalificacionLN()
        {
            _editarCalificacionAD = new EditarCalificacionAD();
        }

        public EditarCalificacionLN(IEditarCalificacion editarCalificacion)
        {
            _editarCalificacionAD = editarCalificacion;
        }

        public async Task<int> EditarCalificacion(int id, CalificacionesDto calificacion)
        {
            try
            {
                if (calificacion.calificacion < 0 || calificacion.calificacion > 100)
                    throw new ArgumentException("La calificación debe estar entre 0 y 100.");

                if (string.IsNullOrWhiteSpace(calificacion.comentario))
                    calificacion.comentario = string.Empty;

                return await _editarCalificacionAD.EditarCalificacion(id, calificacion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la calificación: " + ex.Message, ex);
            }
        }

    }
}
