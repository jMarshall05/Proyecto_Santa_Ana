using System.Collections.Generic;

namespace Campus.Abstracciones.ModelosUI
{
    public class UsuariosGruposDto
    {
        public List<UsuariosDto> usuarios { get; set; }
        public GruposDto grupo { get; set; }
    }
}
