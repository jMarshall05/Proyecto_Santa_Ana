using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIdAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIAD
{
    public class BuscarEstudianteGrupoPorIAD : IBuscarEstudianteGrupoPorIAD
    {
        private Contexto _elContexto;
        public BuscarEstudianteGrupoPorIAD()
        {
            _elContexto = new Contexto();
        }

        public EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante)
        {
            var estudianteGrupo = _elContexto.EstudianteGrupos.FirstOrDefault(eg => eg.Estudiante_id == idEstudiante);
            if (estudianteGrupo == null)
                return null;
            return new EstudianteGrupoDto
            {
                EstudianteGrupo_Id = estudianteGrupo.Id,
                IdGrupo = estudianteGrupo.Grupo_id
            };

        }

        public List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo)
        {
            var estudianteGrupo = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                   where EstudianteGrupo.Grupo_id == idGrupo
                                   select new EstudianteGrupoDto
                                   {
                                       EstudianteGrupo_Id = EstudianteGrupo.Id,
                                       IdEstudiante = EstudianteGrupo.Estudiante_id,
                                   }).ToList();
            return estudianteGrupo;
        }
    }
}
