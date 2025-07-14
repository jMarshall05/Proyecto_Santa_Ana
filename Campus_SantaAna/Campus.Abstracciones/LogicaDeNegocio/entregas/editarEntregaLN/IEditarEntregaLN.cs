using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaNegocio.entregas.editarEntregaLN
{
    public interface IEditarEntregaLN
    {
        Task<int> EditarEntrega(EntregasDto entrega);
    }
}
