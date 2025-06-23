using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.EditarGrupo;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Grupos.EditarGrupo
{
    public class EditarGrupoAD : IEditarGrupoAD
    {
        private Contexto _elContexto;
        public EditarGrupoAD()
        {
            _elContexto = new Contexto();
        }
        public async Task<int> EditarGrupo(int id, GruposDto grupo)
        {
            var grupoExistente = await _elContexto.Grupos.FindAsync(id);
            if (grupoExistente == null)
            {
                return 0; 
            }
            grupoExistente.nombre_grupo = grupo.nombre_grupo;
            grupoExistente.descripcion = grupo.descripcion;
            grupoExistente.FechaDeModificacion = DateTime.Now;
            grupoExistente.estado = grupo.estado;
            _elContexto.Entry(grupoExistente).State = System.Data.Entity.EntityState.Modified;
            return await _elContexto.SaveChangesAsync();
        }
    }
}
