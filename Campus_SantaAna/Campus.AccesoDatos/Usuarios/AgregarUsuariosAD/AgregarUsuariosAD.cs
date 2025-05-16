using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.AgregarUsuariosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Usuarios.AgregarUsuariosAD
{
    public class AgregarUsuariosAD : IAgregarUsuariosAD
    {
        private Contexto _elContexto;
        public AgregarUsuariosAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> AgregarUsuario(UsuariosDto usuario)
        {
            var UsuarioTranformado = ConvertirAD(usuario);
            _elContexto.Usuarios.Add(UsuarioTranformado);
            EntityState estado = _elContexto.Entry(UsuarioTranformado).State = System.Data.Entity.EntityState.Added;
            int Resultado = await _elContexto.SaveChangesAsync();
            return Resultado;

        }

        private UsuariosAD ConvertirAD(UsuariosDto usuario)
        {
            return new UsuariosAD
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                FechaDeNacimiento = usuario.FechaDeNacimiento,
                Cedula = usuario.Cedula,
                FechaDeRegistro = usuario.FechaDeRegistro,
                Rol = usuario.Rol,
                Estado = usuario.Estado

            };
        }


    }
}
