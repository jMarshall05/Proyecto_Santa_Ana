using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("tareas")]
    public class TareasAD
    {
        [Key]
        [Column("id_tarea")]
        public int IdTarea { get; set; }

        [Column("titulo")]
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Column("descripcion")]
        [Required]
        public string Descripcion { get; set; }

        [Column("fecha_entrega")]
        [Required]
        public DateTime FechaEntrega { get; set; }

        [Column("archivo_adjunto")]
        public string ArchivoAdjunto { get; set; }

        [Column("fecha_creacion")]
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Column("fecha_modificacion")]
        public DateTime FechaModificacion { get; set; }

        [Column("FechaPublicacion")]
        public DateTime FechaPublicacion { get; set; }

        [Column("id_materia")]
        [ForeignKey("Materia")]
        public int IdMateria { get; set; }

        public MateriaAD Materia { get; set; }
    }
}