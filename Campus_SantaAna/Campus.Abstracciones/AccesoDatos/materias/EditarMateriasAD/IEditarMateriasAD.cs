using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Materias.EditarMateriasAD
{
    public interface IEditarMateriasAD
    {
        Task<bool> EditarMateria(MateriaDto materia);
    }
}
