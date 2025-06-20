using System;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Materias.AgregarMateriasAD
{
    public interface IAgregarMateriasAD
    {
        Task<int> AgregarMateria(MateriaDto materia);
    }
}
