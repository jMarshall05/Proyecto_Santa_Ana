using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.Grupos.AgregarGrupo
{
    public interface IAgregarGrupoLN
    {
        Task<int> AgregarGrupo(GruposDto grupo);
    }
}
