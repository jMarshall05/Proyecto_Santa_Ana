using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.Usuarios.ObtenerUsuariosPorIdAD;

namespace Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId
{
    public class ObtenerUsuariosPorIdLN : IObtenerUsuariosPorIdLN
    {
        private IObtenerUsuariosPorIdAD _obtenerUsuariosPorId;
        public ObtenerUsuariosPorIdLN()
        {
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdAD();
        }
        public UsuariosDto ObtenerUsuarioPorId(string idUsuario)
        {
            return _obtenerUsuariosPorId.ObtenerUsuarioPorId(idUsuario);
        }
    }
}
