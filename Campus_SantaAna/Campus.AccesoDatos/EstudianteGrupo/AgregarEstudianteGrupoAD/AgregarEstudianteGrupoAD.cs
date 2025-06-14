using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.EstudianteGrupo.AgregarEstudianteGrupoAD
{
    public class AgregarEstudianteGrupoAD : IAgregarEstudianteGrupoAD
    {
        private Contexto _elContexto;
        public AgregarEstudianteGrupoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarEstudianteGrupo(EstudianteGrupoDto estudianteGrupo)
        {
            var EstudianteGrupo = ConvertirAD(estudianteGrupo);
            _elContexto.EstudianteGrupos.Add(EstudianteGrupo);
            int resultado = await _elContexto.SaveChangesAsync();
            return resultado;
        }
        private EstudianteGrupoAD ConvertirAD(EstudianteGrupoDto estudianteGrupo)
        {
            return new EstudianteGrupoAD
            {
                Estudiante_id = estudianteGrupo.IdEstudiante,
                Grupo_id = estudianteGrupo.IdGrupo
            };
        }
    }
}
