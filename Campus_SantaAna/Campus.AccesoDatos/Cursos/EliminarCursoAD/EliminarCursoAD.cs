using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.EliminarCursoAD;

namespace Campus.AccesoDatos.Cursos.EliminarCursoAD
{
    public class EliminarCursoAD : IEliminarCursoAD
    {
        private readonly Contexto _elContexto;
        public EliminarCursoAD()
        {
            _elContexto = new Contexto();
        }

        async Task<bool> IEliminarCursoAD.EliminarCursoAD(int idCurso)
        {
            var CursoExistente = await _elContexto.Cursos.FindAsync(idCurso);

            if (CursoExistente == null)
            {
                throw new ArgumentException("El curso no existe");
            }
            _elContexto.Cursos.Remove(CursoExistente);
            int resultado = await _elContexto.SaveChangesAsync();
            if (resultado <= 0)
                throw new Exception("No se pudo eliminar el curso");
            return true;

        }
    }
}
