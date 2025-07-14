using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.entregas.listarEntregaAD;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;

namespace Campus.LogicaNegocio.Entregas.ListarEntregaLN
{
    public class ListarEntregasLN : IListarEntregasLN
    {
        private readonly IListarEntregas _listarEntregas;

        public ListarEntregasLN()
        {
            _listarEntregas = new Campus.AccesoDatos.Entregas.ListarEntregaAD.ListarEntregasAD();
        }

        public async Task<List<EntregasDto>> ListarEntregas()
        {
            try
            {
                return await _listarEntregas.ListarEntregas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar entregas: " + ex.Message, ex);
            }
        }

        public async Task<List<EntregasDto>> ListarEntregasPorGrupoAsync(int idGrupo)
        {
            try
            {
                if (idGrupo <= 0)
                    throw new ArgumentException("ID de grupo inválido");

                return await _listarEntregas.ListarEntregasPorGrupo(idGrupo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar entregas por grupo: " + ex.Message, ex);
            }
        }
        public async Task<List<EntregasDto>> ListarEntregasPorEstudianteAsync(string idEstudiante)
        {
            return await _listarEntregas.ListarEntregasPorEstudiante(idEstudiante);
        }

    }
}
