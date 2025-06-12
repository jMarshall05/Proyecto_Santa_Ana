using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.EditarGrupo;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.EditarGrupo;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Grupos.EditarGrupo;

namespace Campus.LogicaDeNegocio.Grupos.EditarGrupo
{
    public class EditarGrupoLN : IEditarGrupoLN
    {
        private IEditarGrupoAD _editarGrupoAD;
        public EditarGrupoLN()
        {
            _editarGrupoAD = new EditarGrupoAD();
        }

        public Task<int> EditarGrupo(int id, GruposDto grupo)
        {
           return _editarGrupoAD.EditarGrupo(id, grupo);
        }
    }
}
