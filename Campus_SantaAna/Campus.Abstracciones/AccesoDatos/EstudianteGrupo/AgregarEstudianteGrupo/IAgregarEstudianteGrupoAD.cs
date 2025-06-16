using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.EstudianteGrupo.AgregarEstudianteGrupo
{
    public interface IAgregarEstudianteGrupoAD
    {
        Task<int> AgregarEstudianteGrupo(EstudianteGrupoDto estudianteGrupoDto);
    }
}
