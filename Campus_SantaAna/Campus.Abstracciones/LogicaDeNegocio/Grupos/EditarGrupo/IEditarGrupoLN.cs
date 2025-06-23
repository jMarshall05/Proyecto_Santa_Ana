using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Grupos.EditarGrupo
{
    public interface IEditarGrupoLN
    {
        Task<int> EditarGrupo(int id, GruposDto grupo);
    }
}
