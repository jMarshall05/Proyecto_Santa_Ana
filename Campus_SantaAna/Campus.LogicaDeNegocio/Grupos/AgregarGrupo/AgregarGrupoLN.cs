using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.AgregarGrupo;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.AgregarGrupo;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Grupos.AgregarGrupo;

namespace Campus.LogicaDeNegocio.Grupos.AgregarGrupo
{
    public class AgregarGrupoLN : IAgregarGrupoLN
    {
        private IAgregarGrupoAD _agregarGrupoAD;
        public AgregarGrupoLN()
        {
            _agregarGrupoAD = new AgregarGrupoAD();
        }

        public Task<int> AgregarGrupo(GruposDto grupo)
        {
            grupo.FechaDeCreacion = DateTime.Now;
            grupo.estado = true;
            return _agregarGrupoAD.AgregarGrupo(grupo);
        }
    }
}
