using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.AgregarUsuariosAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.AgregarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Usuarios.AgregarUsuariosAD;

namespace Campus.LogicaDeNegocio.Usuarios.AgregarUsuarios
{
    public class AgregarUsuariosLN : IAgregarUsuariosLN
    {
        private  IAgregarUsuariosAD _agregarUsuarios;
        public AgregarUsuariosLN()
        {
            _agregarUsuarios = new AgregarUsuariosAD();
        }

        public async Task<int> AgregarUsuario(UsuariosDto usuario)
        {
            try
            {
                return await _agregarUsuarios.AgregarUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario", ex);
            }
        }
    }
}
