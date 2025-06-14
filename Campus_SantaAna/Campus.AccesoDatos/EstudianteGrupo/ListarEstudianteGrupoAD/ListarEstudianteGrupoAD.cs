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
                    IdEstudiante = eg.Estudiante_id,
                    IdGrupo = eg.Grupo_id
                }).ToList();
            return estudiantesGrupos;
        }

        public List<EstudianteGrupoDto> ListarEstudiantesPorIdGrupo(int idGrupo)
        {
            var estudiantesGrupos = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                     where EstudianteGrupo.Grupo_id == idGrupo
                                     select new EstudianteGrupoDto
                                     {
                                         EstudianteGrupo_Id = EstudianteGrupo.Id,
                                         IdEstudiante = EstudianteGrupo.Estudiante_id,
                                     }).ToList();
            return estudiantesGrupos;
        }

        public List<EstudianteGrupoDto> ListarGruposPorIdEstudiante(string idUsuario)
        {
            var estudiantesGrupos = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                     where EstudianteGrupo.Estudiante_id == idUsuario
                                     select new EstudianteGrupoDto
                                     {
                                         EstudianteGrupo_Id = EstudianteGrupo.Id,
                                         IdGrupo = EstudianteGrupo.Grupo_id
                                     }).ToList();
            return estudiantesGrupos;
        }
    }
}
