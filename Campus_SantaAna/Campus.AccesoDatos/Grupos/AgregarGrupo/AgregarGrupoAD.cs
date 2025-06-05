using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Grupos.AgregarGrupo;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Grupos.AgregarGrupo
{
    public class AgregarGrupoAD : IAgregarGrupoAD
    {
        private Contexto _elContexto;
        public AgregarGrupoAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarGrupo(GruposDto grupo)
        {
            GruposAD Nuevogrupo = ConvertirAD(grupo);
            _elContexto.Grupos.Add(Nuevogrupo);
            EntityState estado = _elContexto.Entry(Nuevogrupo).State = System.Data.Entity.EntityState.Added;
            int resultado =await _elContexto.SaveChangesAsync();
            return resultado;
        }

        private GruposAD ConvertirAD(GruposDto grupo)
        {
            return new GruposAD
            {
                nombre_grupo = grupo.nombre_grupo,
                descripcion = grupo.descripcion,
                creado_por = grupo.creado_por,
                estado = grupo.estado,
                FechaDeCreacion = grupo.FechaDeCreacion
            };
        }
    }
}
