using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.ModificarEstadoCursoAD;

namespace Campus.AccesoDatos.Cursos.ModificarEstadoCursoAD
{
    public class ModificarEstadoCursoAD : IModificarEstadoCursoAD
    {
        private readonly Contexto _elContexto;
        public ModificarEstadoCursoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<bool> ModificarEstadoCurso(int idCurso, bool estado)
        {
            var CursoExistente = await _elContexto.Cursos.FindAsync(idCurso);

            if (CursoExistente == null)
            {
                throw new ArgumentException("El curso no existe");
            }
            CursoExistente.Estado = estado;

            int resultado = await _elContexto.SaveChangesAsync();
            if (resultado <= 0)
                throw new Exception("No se pudo eliminar el curso");
            return true;

        }


    }
}
