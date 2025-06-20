using Campus.Abstracciones.AccesoDatos.Materias.EliminarMateriasAD;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EliminarMateriasLN;
using Campus.AccesoDatos.Materias.EliminarMateriasAD;

namespace Campus.LogicaDeNegocio.Materias.EliminarMaterias
{
    public class EliminarMateriasLN : IEliminarMateriasLN
    {
        private readonly IEliminarMateriasAD _eliminarMateriasAD;

        public EliminarMateriasLN()
        {
            _eliminarMateriasAD = new EliminarMateriasAD();
        }

        public void EliminarMateria(int materiaId)
        {
            _eliminarMateriasAD.EliminarMateria(materiaId);
        }
    }
}
