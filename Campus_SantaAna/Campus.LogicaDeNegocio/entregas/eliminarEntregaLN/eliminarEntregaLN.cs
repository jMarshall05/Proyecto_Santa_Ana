using System.Threading.Tasks;
using Campus.Abstracciones.LogicaNegocio.entregas.eliminarEntregaLN;
using Campus.Abstracciones.AccesoDatos.entregas.eliminarEntregaAD;
using Campus.AccesoDatos.Entregas.EliminarEntregaAD;

namespace Campus.LogicaNegocio.Entregas.EliminarEntregaLN
{
    public class EliminarEntregaLN : IEliminarEntregaLN
    {
        private readonly IEliminarEntrega _eliminarEntrega;

        public EliminarEntregaLN()
        {
            _eliminarEntrega = new EliminarEntregaAD();
        }

        public async Task<int> EliminarEntrega(int id_entrega)
        {
            return await _eliminarEntrega.EliminarEntrega(id_entrega);
        }
    }
}
