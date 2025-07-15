using System.Threading.Tasks;

namespace Campus.Abstracciones.AccesoDatos.entregas.eliminarEntregaAD
{
    public interface IEliminarEntrega
    {
        Task<int> EliminarEntrega(int id_entrega);
    }
}
