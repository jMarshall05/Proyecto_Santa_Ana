using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.calificaciones.listarCalificacionDA;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN;
using Campus.Abstracciones.ModelosUI;

namespace Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN
{
    public class ListarCalificacionesLN : IListarCalificacionesLN
    {
        private readonly IListarCalificaciones _listarCalificaciones;

        public ListarCalificacionesLN()
        {
            _listarCalificaciones = new Campus.AccesoDatos.calificaciones.listarCalificacionAD.ListarCalificacionesAD();
        }

        public async Task<List<CalificacionesDto>> ListarCalificaciones()
        {
            try
            {
                return await _listarCalificaciones.ListarCalificaciones();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar calificaciones: " + ex.Message, ex);
            }
        }

        public async Task<List<CalificacionesDto>> ListarCalificacionesPorGrupoAsync(int idGrupo)
        {
            try
            {
                if (idGrupo <= 0)
                    throw new ArgumentException("ID de grupo inválido");

                return await _listarCalificaciones.ListarCalificacionesPorGrupo(idGrupo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar entregas por grupo: " + ex.Message, ex);
            }
        }
        public async Task<List<CalificacionesDto>> ListarCalificacionesPorEstudianteAsync(string idEstudiante)
        {
            return await _listarCalificaciones.ListarCalificacionesPorEstudiante(idEstudiante);
        }
    }
}
