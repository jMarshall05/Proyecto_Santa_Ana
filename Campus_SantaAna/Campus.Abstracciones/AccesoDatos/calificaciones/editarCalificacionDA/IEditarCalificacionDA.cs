using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.calificaciones.editarCalificacionAD
{
    public interface IEditarCalificacion
    {
        Task<int> EditarCalificacion(int id,CalificacionesDto calificacion);
    }
}
