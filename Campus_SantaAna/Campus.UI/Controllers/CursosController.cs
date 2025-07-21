using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoLN;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Cursos.AgregarCursoLN;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;

namespace Campus.UI.Controllers
{
    public class CursosController : Controller
    {
        private readonly IListarCursoLN _listarCursoLN;
        private readonly IAgregarCursoLN _agregarCursoLN;
        private readonly IListarGruposLN _listarGruposLN;
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IListarUsuariosLN _listarUsuariosLN;
        public CursosController()
        {
            _listarCursoLN = new ListarCursosLN();
            _agregarCursoLN = new AgregarCursoLN();
            _listarGruposLN = new ListarGruposLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarUsuariosLN = new ListarUsuariosLN();
        }
        // GET: Cursos
        public ActionResult ListarCursos()
        {
            var listaDeCursos = _listarCursoLN.ListarCursos();
            return View(listaDeCursos);
        }

        // GET: Cursos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cursos/Create
        public ActionResult AgregarCursoParcial()
        {
            CargarViewBags();
            return PartialView("_AgregarCursoParcial");
        }

        private void CargarViewBags()
        {
            var Profesores = _listarUsuariosLN.ListarUsuarios().Where(Usuario => Usuario.Rol == "Profesores").Select(Usuario => new
            {
                IdUsuario = Usuario.IdUsuario,
                NombreCompleto = Usuario.Nombre + " " + Usuario.Apellido
            });
            var Materias = _listarMateriasLN.ListarMaterias();
            var Grupos = _listarGruposLN.ListarGrupos();
            ViewBag.Profesores = new SelectList(Profesores, "IdUsuario", "NombreCompleto");
            ViewBag.Materias = new SelectList(Materias, "Id_Materia", "Nombre");
            ViewBag.Grupos = new SelectList(Grupos, "id_grupo", "nombre_grupo");
        }

        // POST: Cursos/Create
        [HttpPost]
        public async Task<ActionResult> AgregarCursoParcial(CursoDto Curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   await _agregarCursoLN.AgregarCurso(Curso);
                    return RedirectToAction("ListarCursos");
                }
                catch
                {
                    CargarViewBags();
                    return PartialView("_AgregarCursoParcial", Curso);
                }
            }
            CargarViewBags();
            return PartialView("_AgregarCursoParcial", Curso);
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cursos/Edit/5
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

        // GET: Cursos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cursos/Delete/5
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
