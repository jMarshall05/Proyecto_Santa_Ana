using System.Threading.Tasks;
using Campus.Abstracciones.LogicaNegocio.entregas.editarEntregaLN;
using Campus.Abstracciones.AccesoDatos.entregas.editarEntregaAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.LogicaNegocio.Entregas.EditarEntregaLN
{
    public class EditarEntregaLN : IEditarEntregaLN
    {
        private readonly IEditarEntrega _editarEntrega;

        public EditarEntregaLN()
        {
        }

        public EditarEntregaLN(IEditarEntrega editarEntrega)
        {
            _editarEntrega = editarEntrega;
        }

        public async Task<int> EditarEntrega(EntregasDto entrega)
        {
            return await _editarEntrega.EditarEntrega(entrega);
        }
    }
}
