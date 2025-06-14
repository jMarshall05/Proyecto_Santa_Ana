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
                    IdEstudianteGrupo = eg.IdEstudianteGrupo,
                    EstudianteId = eg.EstudianteId,
                    GrupoId = eg.GrupoId
                }).ToList();
            return estudiantesGrupos;
        }

        public List<EstudianteGrupoDto> ListarEstudiantesPorIdGrupo(int idGrupo)
        {
            var estudiantesGrupos = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                     where EstudianteGrupo.GrupoId == idGrupo
                                     select new EstudianteGrupoDto
                                     {
                                         IdEstudianteGrupo = EstudianteGrupo.IdEstudianteGrupo,
                                         EstudianteId = EstudianteGrupo.EstudianteId,
                                     }).ToList();
            return estudiantesGrupos;
        }

        public List<EstudianteGrupoDto> ListarGruposPorIdEstudiante(string idUsuario)
        {
            var estudiantesGrupos = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                     where EstudianteGrupo.EstudianteId == idUsuario
                                     select new EstudianteGrupoDto
                                     {
                                         IdEstudianteGrupo = EstudianteGrupo.IdEstudianteGrupo,
                                         GrupoId = EstudianteGrupo.GrupoId
                                     }).ToList();
            return estudiantesGrupos;
        }
    }
}
