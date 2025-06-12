using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.ListarEstudiantesGrupos;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupoLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.EstudianteGrupo.ListarEstudianteGrupoAD;

namespace Campus.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupo
{
    public class ListarEstudianteGrupoLN : IListarEstudianteGrupoLN
    {
        private IListarEstudiantesGruposAD _listarEstudianteGrupoAD;
        public ListarEstudianteGrupoLN()
        {
            _listarEstudianteGrupoAD = new ListarEstudianteGrupoAD();
        }

        public List<EstudianteGrupoDto> ListarEstudianteGrupo()
        {
            return _listarEstudianteGrupoAD.ListarEstudiantesGrupos();
        }

    }
}
