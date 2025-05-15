using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.ModelosUI;

namespace Campus.Abstracciones.AccesoDatos.Usuarios.AgregarUsuariosAD
{
    public interface IAgregarUsuariosAD
    {
        Task<int> AgregarUsuario(UsuariosDto usuario);
    }
}
