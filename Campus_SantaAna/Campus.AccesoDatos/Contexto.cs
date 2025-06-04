using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=Contexto")
        {
        }
        public DbSet<UsuariosAD> Usuarios { get; set; }
        public DbSet<AnunciosAD> Anuncios { get; set; }
        public DbSet<TareasAD> Tareas { get; set; }
        public DbSet<GruposAD> Grupos { get; set; }


    }
}
