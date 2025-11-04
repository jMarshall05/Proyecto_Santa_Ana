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
        public DbSet<EntregasAD> Entregas { get; set; }
        public DbSet<MateriasAD> Materias { get; set; }
        public DbSet<EstudianteGrupoAD> EstudianteGrupos { get; set; }
        public DbSet<CalificacionesAD> Calificaciones { get; set; }
        public DbSet<EventoAD> Eventos { get; set; }
        public DbSet<CursosAD> Cursos { get; set; }
        public DbSet<TelefonoAD> Telefonos { get; set; }
        public DbSet<BitacoraAD> Bitacora { get; set; }


    }
}
