using System.Threading.Tasks;

namespace Campus.Abstracciones.LogicaNegocio.entregas.eliminarEntregaLN
{
    public interface IEliminarEntregaLN
    {
        Task<int> EliminarEntrega(int id_entrega);
    }
}
