using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.agregarCalificacionAD;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.agregarCalificacionLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.calificaciones.agregarCalificacionAD;

public class AgregarCalificacionLN : IAgregarCalificacionLN
{
    private readonly IAgregarCalificacion _agregarCalificacion;

    public AgregarCalificacionLN()
    {
        _agregarCalificacion = new AgregarCalificacionAD();
    }

    public AgregarCalificacionLN(IAgregarCalificacion agregarCalificacion)
    {
        _agregarCalificacion = agregarCalificacion;
    }

    public async Task<int> AgregarCalificacion(CalificacionesDto calificacion)
    {
        return await _agregarCalificacion.AgregarCalificacion(calificacion);
    }
}
