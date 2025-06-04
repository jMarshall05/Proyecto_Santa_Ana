﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.ModelosAD
{
    [Table("grupos")]
    public class GruposAD
    {

        [Key]
        [Column("id_grupo")]
        public int id_grupo { get; set; }
        [Column("nombre_grupo")]
        public string nombre_grupo { get; set; }
        [Column("descripcion")]
        public string descripcion { get; set; }
        [Column("creado_por")]
        public string creado_por { get; set; }
        [Column("estado")]
        public bool estado { get; set; }
    }
}
