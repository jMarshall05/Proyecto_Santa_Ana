using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ListarEstudiantesGrupos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.EstudianteGrupo.ListarEstudianteGrupoAD
{
    public class ListarEstudianteGrupoAD : IListarEstudiantesGruposAD
    {
        private Contexto _elContexto;
        public ListarEstudianteGrupoAD()
        {
            _elContexto = new Contexto();
        }
        public List<EstudianteGrupoDto> ListarEstudiantesGrupos()
        {
            var estudiantesGrupos = _elContexto.EstudianteGrupos
                .Select(eg => new EstudianteGrupoDto
                {
                    EstudianteGrupo_Id = eg.Id,
                    IdUsuario = eg.Estudiante_id,
                    IdGrupo = eg.Grupo_id
                }).ToList();
            return estudiantesGrupos;
        }
    }
}
