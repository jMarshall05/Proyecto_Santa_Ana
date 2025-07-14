using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaNegocio.entregas.agregarEntregaLN
{
    public interface IAgregarEntregaLN
    {
        Task<int> AgregarEntrega(EntregasDto entrega);
    }
}
