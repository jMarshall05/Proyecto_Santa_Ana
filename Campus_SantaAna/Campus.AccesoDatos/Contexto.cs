using System.Data.Entity;
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

        public DbSet<MateriasAD> Materias { get; set; }

        public DbSet<EstudianteGrupoAD> EstudianteGrupos { get; set; }
        




    }
}
