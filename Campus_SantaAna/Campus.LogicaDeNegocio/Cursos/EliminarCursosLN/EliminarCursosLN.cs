using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Cursos.EliminarCursoAD;
using Campus.Abstracciones.AccesoDatos.Cursos.EliminarCursoLN;
using Campus.AccesoDatos.Cursos.EliminarCursoAD;

namespace Campus.LogicaDeNegocio.Cursos.EliminarCursosLN
{
    public class EliminarCursosLN : IEliminarCursoLN
    {
        private readonly IEliminarCursoAD _eliminarCursoAD;
        public EliminarCursosLN()
        {
            _eliminarCursoAD = new EliminarCursoAD();
        }
        public async Task<bool> EliminarCurso(int idCurso)
        {
            if (idCurso <= 0)
            {
                throw new ArgumentException("El ID del curso debe ser un número positivo.", nameof(idCurso));
            }
            return await _eliminarCursoAD.EliminarCursoAD(idCurso);
        }
    }
}
