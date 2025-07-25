using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Materias.EditarMateriasAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Materias.EditarMateriasAD
{
    public class EditarMateriasAD : IEditarMateriasAD
    {
        private readonly Contexto _elContexto;

        public EditarMateriasAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<bool> EditarMateria(MateriaDto materia)
        {
            var materiaExistente = await _elContexto.Materias.FindAsync(materia.Id_Materia);
            if (materiaExistente == null)
                return false;

            materiaExistente.Nombre = materia.Nombre;

            _elContexto.Entry(materiaExistente).State = System.Data.Entity.EntityState.Modified;
            await _elContexto.SaveChangesAsync();

            return true;
        }
    }
}
