using System.Collections.Generic;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Materias.ListarMateriasAD
{
    public interface IListarMateriasAD
    {
        List<MateriaDto> ListarMaterias();

        MateriaDto ObtenerMateriaPorId(int id);
    }
}
