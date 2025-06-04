using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.ListarGrupos;
using Campus.Abstracciones.ModelosUI;

namespace Campus.AccesoDatos.Grupos.ListarGrupos
{
    public class ListarGruposAD : IListarGruposAD  
    {
        private Contexto _elContexto;
        public ListarGruposAD()
        {
            _elContexto = new Contexto();
        }

        public GruposDto BuscarGruposPorUsuario(int idGrupo)
        {
            GruposDto grupo = (from Grupos in _elContexto.Grupos
                               where Grupos.id_grupo == idGrupo
                               select new GruposDto
                               {
                                   id_grupo = Grupos.id_grupo,
                                   nombre_grupo = Grupos.nombre_grupo,
                                   descripcion = Grupos.descripcion,
                                   creado_por = Grupos.creado_por,
                                   estado = Grupos.estado
                               }).FirstOrDefault();
            return grupo;
        }

        public List<GruposDto> ListarGrupos()
        {
            List<GruposDto> ListaDeGrupos = (from Grupos in _elContexto.Grupos
                                             select new GruposDto
                                             {
                                                 id_grupo = Grupos.id_grupo,
                                                 nombre_grupo = Grupos.nombre_grupo,
                                                 descripcion = Grupos.descripcion,
                                                 creado_por = Grupos.creado_por,
                                                 estado = Grupos.estado
                                             }).ToList();
            return ListaDeGrupos;
        }
    }
}
