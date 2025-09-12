using System.Web.Mvc;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.AgregarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EliminarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EditarMateriasLN;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Materias.AgregarMaterias;
using Campus.LogicaDeNegocio.Materias.EliminarMaterias;
using Campus.LogicaDeNegocio.Materias.EditarMaterias;

namespace Campus.UI.Controllers
{
    [Authorize]
    public class MateriasController : Controller
    {
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IAgregarMateriasLN _agregarMateriasLN;
        private readonly IEliminarMateriasLN _eliminarMateriasLN;
        private readonly IEditarMateriasLN _editarMateriasLN;

        public MateriasController()
        {
            _listarMateriasLN = new ListarMateriasLN();
            _agregarMateriasLN = new AgregarMateriasLN();
            _eliminarMateriasLN = new EliminarMateriasLN();
            _editarMateriasLN = new EditarMateriasLN();
        }

        // GET: Materias/ListarMaterias
        public ActionResult ListarMaterias()
        {
            var listaDeMaterias = _listarMateriasLN.ListarMaterias();
            return View(listaDeMaterias);
        }

        // GET: Materias/CrearMaterias
        public ActionResult CrearMaterias()
        {
            return View();
        }

        // POST: Materias/CrearMateria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearMateria(MateriaDto materia)
        {
            if (ModelState.IsValid)
            {
                _agregarMateriasLN.AgregarMateria(materia);
                return RedirectToAction("ListarMaterias");
            }

            return View(materia);
        }

        // GET: Materias/EditarMaterias/5
        public ActionResult EditarMaterias(int id)
        {
            var materia = _listarMateriasLN.ObtenerMateriaPorId(id);
            if (materia == null)
            {
                return HttpNotFound();
            }

            return View(materia);
        }

        // POST: Materias/EditarMaterias
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMaterias(MateriaDto materia)
        {
            if (ModelState.IsValid)
            {
                _editarMateriasLN.EditarMateria(materia);
                return RedirectToAction("ListarMaterias");
            }

            return View(materia);
        }

        // GET: Materias/EliminarMaterias/5
        public ActionResult EliminarMaterias(int id)
        {
            var materia = _listarMateriasLN.ObtenerMateriaPorId(id);
            if (materia == null)
                return HttpNotFound();

            return View(materia);
        }

        // POST: Materias/EliminarMaterias
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarMateriaConfirmado(int IdMateria)
        {
            try
            {
                _eliminarMateriasLN.EliminarMateria(IdMateria);
                return RedirectToAction("ListarMaterias");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la materia.");
                var materia = _listarMateriasLN.ObtenerMateriaPorId(IdMateria);
                return View("EliminarMaterias", materia);
            }
        }
    }
}
