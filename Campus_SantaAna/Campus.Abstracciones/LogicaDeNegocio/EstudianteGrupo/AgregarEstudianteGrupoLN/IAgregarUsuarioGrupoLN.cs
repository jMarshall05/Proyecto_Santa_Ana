﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo
{
    public interface IAgregarEstudianteGrupoLN
    {

        Task<int> AgregarEstudianteGrupo(EstudianteGrupoDto estudianteGrupoDto);
    }
}
