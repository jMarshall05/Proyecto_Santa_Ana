using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Materias.EditarMateriasLN
{
    public interface IEditarMateriasLN
    {
        Task<bool> EditarMateria(MateriaDto materia);
    }
}
