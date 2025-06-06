﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Campus.Abstracciones.ModelosUI;
namespace Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN
{
    public interface IListarTareaLN
    {
        Task<IEnumerable<TareaDto>> ListarTareasAsync();
        Task<TareaDto> ObtenerPorIdAsync(int idTarea);
    }
}
