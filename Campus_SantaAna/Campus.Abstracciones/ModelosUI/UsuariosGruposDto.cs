using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Abstracciones.ModelosUI
{
    public class UsuariosGruposDto
    {
        public List<UsuariosDto> usuarios { get; set; }
        public GruposDto grupo { get; set; }
    }
}
