using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.ListarUsuariosAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Usuarios.ListarUsuariosAD;

namespace Campus.LogicaDeNegocio.Usuarios.ListarUsuarios
{
    public class ListarUsuariosLN : IListarUsuariosLN
    {
        private  IListarUsuariosAD _listarUsuarios;
        public ListarUsuariosLN()
        {
            _listarUsuarios = new ListarUsuariosAD();
        }
        public List<UsuariosDto> ListarUsuarios()
        {
            return _listarUsuarios.ListarUsuarios();
        }
    }
}
