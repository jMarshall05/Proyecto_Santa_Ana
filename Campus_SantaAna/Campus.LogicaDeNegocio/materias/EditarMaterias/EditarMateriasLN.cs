using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Materias.EditarMateriasAD;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EditarMateriasLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Materias.EditarMateriasAD;

namespace Campus.LogicaDeNegocio.Materias.EditarMaterias
{
    public class EditarMateriasLN : IEditarMateriasLN
    {
        private readonly IEditarMateriasAD _editarMaterias;

        public EditarMateriasLN()
        {
            _editarMaterias = new EditarMateriasAD();
        }

        public async Task<bool> EditarMateria(MateriaDto materia)
        {
            try
            {
                return await _editarMaterias.EditarMateria(materia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la materia", ex);
            }
        }
    }
}
