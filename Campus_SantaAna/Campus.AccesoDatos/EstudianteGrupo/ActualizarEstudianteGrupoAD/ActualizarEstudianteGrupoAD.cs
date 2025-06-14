using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD
{
    public class ActualizarEstudianteGrupoAD : IActualizarEstudianteGrupoAD
    {
        private Contexto _elContexto;
        public ActualizarEstudianteGrupoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> ActualizarEstudianteGrupo(EstudianteGrupoDto estudiante)
        {
            var EstudianteGrupoEnBase = _elContexto.EstudianteGrupos.FirstOrDefault(x => x.Estudiante_id == estudiante.IdEstudiante);
            EstudianteGrupoEnBase.Grupo_id = estudiante.IdGrupo;
            _elContexto.Entry(EstudianteGrupoEnBase).State = System.Data.Entity.EntityState.Modified;
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }
    }
}
