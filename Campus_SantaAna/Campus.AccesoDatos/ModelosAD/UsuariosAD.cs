using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("Usuarios_tb")]
    public class UsuariosAD
    {
        [Key]
        [Column("IdUsuario")]
        public string IdUsuario { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Apellido")]
        public string Apellido { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Telefono")]
        public int Telefono { get; set; }
        [Column("FechaDeNacimiento")]
        public DateTime FechaDeNacimiento { get; set; }
        [Column("Cedula")]
        public int Cedula { get; set; }
        [Column("FechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; }
        [Column("FechaDeModificacion")]
        public DateTime? FechaDeModificacion { get; set; }
        [Column("Rol")]
        public string Rol { get; set; }
        [Column("Estado")]
        public bool Estado { get; set; }

    }
}
