using Campus.Abstracciones.AccesoDatos.entregas.agregarEntregaAD;
using Campus.Abstracciones.LogicaNegocio.entregas.agregarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Entregas.AgregarEntregaAD;
using System.Threading.Tasks;

public class AgregarEntregaLN : IAgregarEntregaLN
{
    private readonly IAgregarEntrega _agregarEntrega;

    public AgregarEntregaLN()
    {
        // Aquí debes crear la instancia concreta, por ejemplo:
        _agregarEntrega = new AgregarEntregaAD(); // <- Asegúrate que esta clase exista y tenga constructor sin parámetros
    }

    public AgregarEntregaLN(IAgregarEntrega agregarEntrega)
    {
        _agregarEntrega = agregarEntrega;
    }

    public async Task<int> AgregarEntrega(EntregasDto entrega)
    {
        return await _agregarEntrega.AgregarEntrega(entrega);
    }
}
