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
            var estudianteGrupo = _elContexto.EstudianteGrupos.FirstOrDefault(eg => eg.EstudianteId == idEstudiante);
            if (estudianteGrupo == null)
                return null;
            return new EstudianteGrupoDto
            {
                IdEstudianteGrupo = estudianteGrupo.IdEstudianteGrupo,
                GrupoId = estudianteGrupo.GrupoId
            };

        }

        public List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo)
        {
            var estudianteGrupo = (from EstudianteGrupo in _elContexto.EstudianteGrupos
                                   where EstudianteGrupo.GrupoId == idGrupo
                                   select new EstudianteGrupoDto
                                   {
                                       IdEstudianteGrupo = EstudianteGrupo.IdEstudianteGrupo,
                                       EstudianteId = EstudianteGrupo.EstudianteId,
                                   }).ToList();
            return estudianteGrupo;
        }
    }
}
