using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("Telefonos")]
    public class TelefonoAD
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Id_Usuario")]
        public string IdUsuario { get; set; }
        [Column("Codigo_area")]
        public int Codigo { get; set; }
        [Column("Telefono")]
        public int Telefono { get; set; }
        [Column("Tipo")]
        public string Tipo { get; set; }
        [Column("Estado")]
        public bool Estado { get; set; } = true;
    }
}
