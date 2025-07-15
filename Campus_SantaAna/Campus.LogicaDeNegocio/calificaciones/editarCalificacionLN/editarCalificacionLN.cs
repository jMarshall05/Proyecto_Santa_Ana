using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.editarCalificacionAD;
using Campus.Abstracciones.AccesoDatos.entregas.editarEntregaAD;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN;
using Campus.Abstracciones.ModelosUI;

namespace Campus.LogicaDeNegocio.calificaciones
{
    public class EditarCalificacionLN : IEditarCalificacionLN
    {
        private readonly IEditarCalificacion _editarCalificacion;

        public EditarCalificacionLN()
        {
        }

        public EditarCalificacionLN(IEditarCalificacion editarCalificacion)
        {
            _editarCalificacion = editarCalificacion;
        }

        public async Task<int> EditarCalificacion(CalificacionesDto calificacion)
        {
            return await _editarCalificacion.EditarCalificacion(calificacion);
        }
    }
}
