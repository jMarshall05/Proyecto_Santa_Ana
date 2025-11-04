using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoAD;
using Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Cursos.AgregarCursoAD;

namespace Campus.LogicaDeNegocio.Cursos.AgregarCursoLN
{
    public class AgregarCursoLN : IAgregarCursoLN
    {
        private readonly IAgregarCursoAD _agregarCursoAD;
        public AgregarCursoLN()
        {
            _agregarCursoAD = new AgregarCursoAD();
        }
        public async Task<int> AgregarCurso(CursoDto curso)
        {
            if (curso == null)
            {
                throw new ArgumentNullException(nameof(curso), "El curso no puede ser nulo");
            }
            return await _agregarCursoAD.AgregarCurso(curso);
        }
    }
}
