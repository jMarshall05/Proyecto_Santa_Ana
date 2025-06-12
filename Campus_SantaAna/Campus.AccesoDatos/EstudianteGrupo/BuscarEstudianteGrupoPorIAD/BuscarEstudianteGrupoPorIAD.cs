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
            var estudianteGrupo = _elContexto.EstudianteGrupos
                .FirstOrDefault(eg => eg.Estudiante_id == idEstudiante);
            return new EstudianteGrupoDto
            {
                EstudianteGrupo_Id = estudianteGrupo.Id,
                IdGrupo = estudianteGrupo.Grupo_id
            };
        }

        public EstudianteGrupoDto BuscarEstudianteGrupoPorGrupoId(int idGrupo)
        {
            var estudianteGrupo = _elContexto.EstudianteGrupos
                .FirstOrDefault(eg => eg.Grupo_id == idGrupo);
            return new EstudianteGrupoDto
            {
                EstudianteGrupo_Id = estudianteGrupo.Id,
                IdGrupo = estudianteGrupo.Grupo_id
            };
        }
    }
}
