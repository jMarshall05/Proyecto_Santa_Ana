using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.EstudianteGrupo.ActualizarEstudianteGrupoAD;

namespace Campus.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN
{
    public class ActualizarEstudianteGrupoLN : IActualizarEstudianteGrupoLN
    {
        private IActualizarEstudianteGrupoAD _actualizarEstudianteGrupo;
        public ActualizarEstudianteGrupoLN()
        {
            _actualizarEstudianteGrupo = new ActualizarEstudianteGrupoAD();
        }
        public Task<int> ActualizarEstudianteGrupo(EstudianteGrupoDto estudianteGrupo)
        {
            return _actualizarEstudianteGrupo.ActualizarEstudianteGrupo(estudianteGrupo);
        }
    }
}
