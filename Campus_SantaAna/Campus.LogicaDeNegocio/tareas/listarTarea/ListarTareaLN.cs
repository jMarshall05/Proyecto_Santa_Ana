using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
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
                throw new Exception("Error al listar las tareas: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<TareaDto>> ListarTareasPorGrupoAsync(int idGrupo)
        {
            try
            {
                if (idGrupo <= 0)
                    throw new ArgumentException("ID de grupo no válido");

                var todasTareas = await _listarTareaAD.ListarTareasAsync();
                return todasTareas.Where(t => t.id_grupo == idGrupo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tareas por grupo: " + ex.Message, ex);
            }
        }
        public async Task<IEnumerable<GruposDto>> ListarGruposAsync()
        {
            try
            {
                return await _listarTareaAD.ListarGruposAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar grupos: " + ex.Message, ex);
            }
        }

        public async Task<TareaDto> ObtenerPorIdAsync(int idTarea)
        {
            try
            {
                if (idTarea <= 0)
                    throw new ArgumentException("ID de tarea no válido");

                return await _listarTareaAD.ObtenerPorIdAsync(idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la tarea por ID: " + ex.Message, ex);
            }
        }

    
       
        
        
        public async Task<List<TareaDto>> ListarTareasPorEstudiante(string idEstudiante)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idEstudiante))
                    throw new ArgumentException("El ID del estudiante no puede ser vacío");

                return await _listarTareaAD.ListarTareasPorEstudiante(idEstudiante);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tareas por estudiante: " + ex.Message, ex);
            }
        }



    }
}
