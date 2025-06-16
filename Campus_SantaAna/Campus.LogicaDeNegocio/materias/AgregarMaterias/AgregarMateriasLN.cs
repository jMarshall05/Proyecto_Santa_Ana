using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Materias.AgregarMateriasAD;
using Campus.Abstracciones.LogicaDeNegocio.Materias.AgregarMateriasLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Materias.AgregarMateriasAD;

namespace Campus.LogicaDeNegocio.Materias.AgregarMaterias
{
    public class AgregarMateriasLN : IAgregarMateriasLN
    {
        private readonly IAgregarMateriasAD _agregarMaterias;

        public AgregarMateriasLN()
        {
            _agregarMaterias = new AgregarMateriasAD();
        }

        public async Task<int> AgregarMateria(MateriaDto materia)
        {
            try
            {
                return await _agregarMaterias.AgregarMateria(materia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la materia", ex);
            }
        }
    }
}

