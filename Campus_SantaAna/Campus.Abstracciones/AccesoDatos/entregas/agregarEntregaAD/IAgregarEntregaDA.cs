using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.entregas.agregarEntregaAD
{
    public interface IAgregarEntrega
    {
        Task<int> AgregarEntrega(EntregasDto entrega);
    }
}
