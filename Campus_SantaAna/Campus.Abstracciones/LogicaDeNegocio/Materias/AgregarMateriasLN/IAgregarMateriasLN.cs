using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Materias.AgregarMateriasLN
{
    public interface IAgregarMateriasLN
    {
        Task<int> AgregarMateria(MateriaDto materia);
    }
}
