using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Microsoft.AspNet.Identity;

namespace Campus.UI.Controllers
{
    public class GruposController : Controller
    {
        private IListarGruposLN _listarGrupos;
        private IObtenerUsuariosPorIdLN _obtenerUsuariosPorIdLN;
        public GruposController()
        {
            _listarGrupos = new ListarGruposLN();
            _obtenerUsuariosPorIdLN = new ObtenerUsuariosPorIdLN();
        }
        // GET: Grupos
        public ActionResult ListarGrupos()
        {
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
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
