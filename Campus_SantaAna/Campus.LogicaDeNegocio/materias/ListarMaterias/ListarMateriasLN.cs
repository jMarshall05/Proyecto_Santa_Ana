using System.Collections.Generic;
using Campus.Abstracciones.AccesoDatos.Materias.ListarMateriasAD;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Materias.ListarMateriasAD;

namespace Campus.LogicaDeNegocio.Materias.ListarMaterias
{
    public class ListarMateriasLN : IListarMateriasLN
    {
        private readonly IListarMateriasAD _listarMaterias;

        public ListarMateriasLN()
        {
            _listarMaterias = new ListarMateriasAD();
        }

        public List<MateriaDto> ListarMaterias()
        {
            return _listarMaterias.ListarMaterias();
        }

        public MateriaDto ObtenerMateriaPorId(int id)
        {
            return _listarMaterias.ObtenerMateriaPorId(id);
        }
    }
}
