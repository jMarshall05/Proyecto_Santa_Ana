using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

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
            EstudianteGrupoAD EstudianteGrupoEnBase = _elContexto.EstudianteGrupos.FirstOrDefault(x => x.EstudianteId == estudiante.EstudianteId);
            EstudianteGrupoEnBase.GrupoId = estudiante.GrupoId;
            _elContexto.Entry(EstudianteGrupoEnBase).State = System.Data.Entity.EntityState.Modified;
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }
    }
}
