using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.AccesoDatos.Usuarios.AgregarUsuariosAD
{
    public class AgregarUsuariosAD : IAgregarUsuariosAD
    {
        private Contexto _elContexto;
        public AgregarUsuariosAD()
        {
            _elContexto = new Contexto();
        }


    }
}
