using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.AgregarGrupo;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Grupos.AgregarGrupo;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Microsoft.AspNet.Identity;

namespace Campus.UI.Controllers
{
    public class GruposController : Controller
    {
        private IListarGruposLN _listarGrupos;
        private IObtenerUsuariosPorIdLN _obtenerUsuariosPorIdLN;
        private IAgregarGrupoLN _agregarGrupoLN;
        public GruposController()
        {
            _listarGrupos = new ListarGruposLN();
            _obtenerUsuariosPorIdLN = new ObtenerUsuariosPorIdLN();
            _agregarGrupoLN = new AgregarGrupoLN();
        }
        // GET: Grupos
        public ActionResult ListarGrupos()
        {
            ViewBag.Id = User.Identity.GetUserId();
            var listaDeGrupos = _listarGrupos.ListarGrupos();
            return View(listaDeGrupos);
        }

         public ActionResult BuscarGruposPorUsuario()
        {
            string id = User.Identity.GetUserId();
            var Usuario=_obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);  

            var grupo = _listarGrupos.BuscarGruposPorUsuario((int)Usuario.Id_grupo);
            return View(grupo);
        }

        // GET: Grupos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Grupos/Create
        public ActionResult AgregarGrupoParcial(string id)
        {
            return PartialView("_AgregarGrupoParcial");
        }

        // POST: Grupos/Create
        [HttpPost]
        public async Task<ActionResult> AgregarGrupoParcial(string id,GruposDto grupo)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Por favor, complete todos los campos requeridos.");
                    return PartialView("_AgregarGrupoParcial",grupo);
                }
                var Usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
                grupo.creado_por = Usuario.Nombre + " " + Usuario.Apellido;
                int resultado = await _agregarGrupoLN.AgregarGrupo(grupo);
                if (resultado == 0)
                {
                    ModelState.AddModelError("", "No se pudo agregar el grupo. Por favor, intente nuevamente.");
                    return PartialView("_AgregarGrupoParcial",grupo);
                }
                return RedirectToAction("ListarGrupos");
            }
            catch
            {
                return View();
            }
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Grupos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Grupos/Delete/5
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
