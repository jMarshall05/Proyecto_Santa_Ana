using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

    }
}
