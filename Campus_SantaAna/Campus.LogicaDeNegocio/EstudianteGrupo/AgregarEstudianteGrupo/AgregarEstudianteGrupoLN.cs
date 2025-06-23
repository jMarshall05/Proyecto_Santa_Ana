using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.EstudianteGrupo.AgregarEstudianteGrupoAD;

namespace Campus.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo
{
    public class AgregarEstudianteGrupoLN : IAgregarEstudianteGrupoLN
    {
        private  IAgregarEstudianteGrupoAD _agregarEstudianteGrupoAD;
        public AgregarEstudianteGrupoLN()
        {
            _agregarEstudianteGrupoAD = new AgregarEstudianteGrupoAD();
        }

        public async Task<int> AgregarEstudianteGrupo(EstudianteGrupoDto estudianteGrupo)
        {
            return await _agregarEstudianteGrupoAD.AgregarEstudianteGrupo(estudianteGrupo);
        }
    }
}
