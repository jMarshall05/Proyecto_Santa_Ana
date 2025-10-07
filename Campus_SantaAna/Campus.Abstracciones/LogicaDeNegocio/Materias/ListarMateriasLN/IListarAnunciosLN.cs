using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN
{
    public interface IListarMateriasLN
    {
        IEnumerable<MateriaDto> ListarMaterias();

        MateriaDto ObtenerMateriaPorId(int id);
    }
}

