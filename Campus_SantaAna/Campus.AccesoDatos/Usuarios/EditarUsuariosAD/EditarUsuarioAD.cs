using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Abstracciones.AccesoDatos.Usuarios.EditarUsuariosAD;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;

namespace Campus.AccesoDatos.Usuarios.EditarUsuariosAD
{
    public class EditarUsuarioAD : IEditarUsuarioAD
    {
        private Contexto _elContexto;
        public EditarUsuarioAD()
        {
            _elContexto = new Contexto();
        }
        public async Task<int> EditarUsuario(string id, UsuariosDto usuario)
        {
            UsuariosAD usuarioExistente = _elContexto.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Telefono = usuario.Telefono;
                usuarioExistente.FechaDeNacimiento = usuario.FechaDeNacimiento;
                usuarioExistente.FechaDeModificacion = DateTime.Now;
                usuarioExistente.Estado = usuario.Estado;

                EntityState estado = _elContexto.Entry(usuarioExistente).State = EntityState.Modified;
                int resultado = await _elContexto.SaveChangesAsync();
                return resultado;
            }
            else
            {
                throw new Exception("El usuario no existe o no se pudo encontrar en la base de datos.");
            }
        }
    }
}
