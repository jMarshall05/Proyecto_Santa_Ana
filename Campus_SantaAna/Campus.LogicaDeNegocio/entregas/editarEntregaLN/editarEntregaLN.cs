using System.Threading.Tasks;
using Campus.Abstracciones.LogicaNegocio.entregas.editarEntregaLN;
using Campus.Abstracciones.AccesoDatos.entregas.editarEntregaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Entregas.EditarEntregaAD;

namespace Campus.LogicaNegocio.Entregas.EditarEntregaLN
{
    public class EditarEntregaLN : IEditarEntregaLN
    {
        private readonly IEditarEntrega _editarEntrega;

        public EditarEntregaLN()
        {
            _editarEntrega = new EditarEntregaAD();  // inicializar aquí
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

