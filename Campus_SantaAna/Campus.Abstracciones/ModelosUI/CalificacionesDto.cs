using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Campus.Abstracciones.ModelosUI
{
    public class CalificacionesDto
    {
        public int id_calificacion {  get; set; }
        public int id_entrega { get; set; }
        public decimal calificacion { get; set; }
        public string comentario { get; set; }
        public DateTime fecha_calificacion { get; set; }
        public EntregasDto Entrega { get; set; }
        
        [DisplayName("Estado")]
        public bool Estado { get; set; }

    }
}
