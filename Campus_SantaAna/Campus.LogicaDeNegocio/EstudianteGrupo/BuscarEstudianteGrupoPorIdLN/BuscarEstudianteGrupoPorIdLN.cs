using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIdAD;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.EstudianteGrupo.BuscarEstudianteGrupoPorIAD;

namespace Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN
{
    public class BuscarEstudianteGrupoPorIdLN : IBuscarEstudianteGrupoPorIdLN
    {
        private IBuscarEstudianteGrupoPorIAD _buscarEstudianteGrupoPorId;
        public BuscarEstudianteGrupoPorIdLN()
        {
            _buscarEstudianteGrupoPorId = new BuscarEstudianteGrupoPorIAD();
        }

        public EstudianteGrupoDto BuscarEstudianteGrupoPorEstudianteId(string idEstudiante)
        {
            return _buscarEstudianteGrupoPorId.BuscarEstudianteGrupoPorEstudianteId(idEstudiante);
        }

        public List<EstudianteGrupoDto> BuscarEstudianteGrupoPorGrupoId(int idGrupo)
        {
            return _buscarEstudianteGrupoPorId.BuscarEstudianteGrupoPorGrupoId(idGrupo);
        }
    }

}
