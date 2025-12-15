using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.eliminarCalificacionDA;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.AccesoDatos.calificaciones.eliminarCalificacionAD;

namespace Campus.LogicaDeNegocio.calificaciones.eliminarCalificacionLN
{
    public class EliminarCalificacionLN : IEliminarCalificacionLN
    {
        private readonly IEliminarCalificacion _eliminarCalificacion;

        public EliminarCalificacionLN()
        {
            _eliminarCalificacion = new EliminarCalificacionAD();
        }

        public async Task<int> EliminarCalificacion(int id_calificacion)
        {
            return await _eliminarCalificacion.EliminarCalificacion(id_calificacion);
        }
    }
}
