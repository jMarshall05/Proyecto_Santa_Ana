using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.eliminarCalificacionDA;
using Campus.Abstracciones.AccesoDatos.entregas.eliminarEntregaAD;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;

namespace Campus.LogicaDeNegocio.calificaciones.eliminarCalificacionLN
{
    public class EliminarCalificacionLN : IEliminarCalificacionLN
    {
        private readonly IEliminarCalificacion _eliminarCalificacion;

        public EliminarCalificacionLN()
        {
        }

        public EliminarCalificacionLN(IEliminarCalificacion eliminarCalificacion)
        {
            _eliminarCalificacion = eliminarCalificacion;
        }

        public async Task<int> EliminarCalificacion(int id_calificacion)
        {
            return await _eliminarCalificacion.EliminarCalificacion(id_calificacion);
        }
    }
}
