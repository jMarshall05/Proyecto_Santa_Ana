using System;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Bitacora;
using Campus.Abstracciones.AccesoDatos.Usuarios.AgregarUsuariosAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.AgregarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Bitacora;
using Campus.AccesoDatos.Usuarios.AgregarUsuariosAD;

namespace Campus.LogicaDeNegocio.Usuarios.AgregarUsuarios
{
    public class AgregarUsuariosLN : IAgregarUsuariosLN
    {
        private readonly IAgregarUsuariosAD _agregarUsuarios;
        private readonly IBitacoraAD _bitacora;
        public AgregarUsuariosLN()
        {
            _agregarUsuarios = new AgregarUsuariosAD();
            _bitacora = new BitacoraAD();
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
