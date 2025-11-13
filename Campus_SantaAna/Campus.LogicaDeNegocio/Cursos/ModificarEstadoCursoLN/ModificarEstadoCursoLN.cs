using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoAD;
using Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoLN;
using Campus.AccesoDatos.Cursos.ModificarEstadoCursoAD;

namespace Campus.LogicaDeNegocio.Cursos.ModificarEstadoCursoLN
{
    public class ModificarEstadoCursoLN : IModificarEstadoCursoLN
    {
        private readonly IModificarEstadoCursoAD _ModificarEstadoCursoAD;
        public ModificarEstadoCursoLN()
        {
            _ModificarEstadoCursoAD = new ModificarEstadoCursoAD();
        }
        public async Task<bool> ModificarEstadoCurso(int idCurso, bool estado)
        {
            if (idCurso <= 0)
            {
                throw new ArgumentException("El ID del curso debe ser un número positivo.", nameof(idCurso));
            }
            return await _ModificarEstadoCursoAD.ModificarEstadoCurso(idCurso, estado);
        }
    }
}
