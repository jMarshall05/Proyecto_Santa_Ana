using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.entregas.editarEntregaAD
{
    public interface IEditarEntrega
    {
        Task<int> EditarEntrega(EntregasDto entrega);
    }
}
