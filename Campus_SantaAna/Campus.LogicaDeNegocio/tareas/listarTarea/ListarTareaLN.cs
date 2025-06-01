
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.tareas.listarTareaAD;


namespace Campus.LogicaDeNegocio.Tareas.ListarTareaLN
{
    public class ListarTareaLN : IListarTareaLN
    {
        private readonly IListarTarea _listarTareaAD;

        public ListarTareaLN()
        {
            _listarTareaAD = new ListarTareaAD();
        }

        public async Task<IEnumerable<TareaDto>> ListarTareasAsync()
        {
            try
            {
                return await _listarTareaAD.ListarTareasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las tareas", ex);
            }
        }

        public async Task<TareaDto> ObtenerPorIdAsync(int idTarea)
        {
            try
            {
                return await _listarTareaAD.ObtenerPorIdAsync(idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la tarea por ID", ex);
            }
        }
    }
}
