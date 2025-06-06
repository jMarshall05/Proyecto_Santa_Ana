using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Usuarios.EditarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Campus.UI.Controllers
{
    public class UsuariosController : Controller
    {
        private IListarUsuariosLN _listarUsuariosLN;
        private IObtenerUsuariosPorIdLN _obtenerUsuariosPorIdLN;
        private IEditarUsuarioLN _editarUsuarioLN;
        private ApplicationUserManager _userManager;
        private IListarGruposLN _listarGrupos;
        public UsuariosController()
        {
            _listarUsuariosLN = new ListarUsuariosLN();
            _obtenerUsuariosPorIdLN = new ObtenerUsuariosPorIdLN();
            _editarUsuarioLN = new EditarUsuariosLN();
            _listarGrupos = new ListarGruposLN();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public UsuariosController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        // GET: Usuarios
        public ActionResult ListarUsuarios()
        {
            var listaDeUsuarios = _listarUsuariosLN.ListarUsuarios();
            return View(listaDeUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult DetallesDeUsuarioParcial(string id)
        {
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id.ToString());
            if (usuario.Id_grupo != null)
            {
                var grupo = _listarGrupos.BuscarGruposPorId((int)usuario.Id_grupo); 
                ViewBag.NombreGrupo = grupo.nombre_grupo;
            }
           
            return PartialView("_DetallesDeUsuarioParcial", usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult EditarUsuarioParcial(string id)
        {
            var listaDeGrupos = _listarGrupos.ListarGrupos();
            ViewBag.ListaDeGrupos = new SelectList(listaDeGrupos, "id_grupo", "nombre_grupo");
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
            return PartialView("_EditarUsuarioParcial", usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult EditarUsuarioParcial(string id, UsuariosDto usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _editarUsuarioLN.EditarUsuarioAdmin(id, usuario);
                }
                else
                {
                    ModelState.AddModelError("", "Por favor, corrija los errores en el formulario.");
                    return PartialView("_EditarUsuarioParcial", usuario);
                }


                return RedirectToAction("ListarUsuarios");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditarUsuario(string id, UsuariosDto usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var result = await UserManager.SetEmailAsync(id, usuario.Email);
                        if (result.Succeeded)
                        {
                            await _editarUsuarioLN.EditarUsuario(id, usuario);
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Por favor, corrija los errores en el formulario.");
                    return PartialView("_EditarUsuarioParcial", usuario);
                }



                return RedirectToAction("ListarUsuarios");
            }
            catch
            {
                return View();
            }
        }


        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
