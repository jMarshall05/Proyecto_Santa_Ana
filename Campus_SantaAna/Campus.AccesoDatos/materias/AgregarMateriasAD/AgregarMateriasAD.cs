using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Materias.AgregarMateriasAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Materias.AgregarMateriasAD
{
    public class AgregarMateriasAD : IAgregarMateriasAD
    {
        private Contexto _elContexto;

        public AgregarMateriasAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarMateria(MateriaDto materia)
        {
            var materiaTransformada = ConvertirAD(materia);
            _elContexto.Materias.Add(materiaTransformada);
            _elContexto.Entry(materiaTransformada).State = System.Data.Entity.EntityState.Added;
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }

        private MateriasAD ConvertirAD(MateriaDto materia)
        {
            return new MateriasAD
            {
                Nombre = materia.Nombre
            };
        }
    }
}
