using System;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Materias.AgregarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EditarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.EliminarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Materias.AgregarMaterias;
using Campus.LogicaDeNegocio.Materias.EditarMaterias;
using Campus.LogicaDeNegocio.Materias.EliminarMaterias;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Microsoft.AspNet.Identity;

namespace Campus.UI.Controllers
{
    [Authorize]
    public class MateriasController : Controller
    {
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IAgregarMateriasLN _agregarMateriasLN;
        private readonly IEliminarMateriasLN _eliminarMateriasLN;
        private readonly IEditarMateriasLN _editarMateriasLN;
        private readonly IBitacoraLN _bitacora;

        public MateriasController()
        {
            _listarMateriasLN = new ListarMateriasLN();
            _agregarMateriasLN = new AgregarMateriasLN();
            _eliminarMateriasLN = new EliminarMateriasLN();
            _editarMateriasLN = new EditarMateriasLN();
            _bitacora = new BitacoraLN();
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

                // Bitácora: inserción de nueva materia
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "INSERT",
                    Tabla = "Materias",
                    Descripcion = $"Creación de materia {materia.Nombre}"
                };
                _bitacora.RegistrarEvento(bitacora);

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

                // Bitácora: actualización de materia
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "UPDATE",
                    Tabla = "Materias",
                    Descripcion = $"Actualización de materia ID: {materia.Id_Materia} - '{materia.Nombre}'"
                };
                _bitacora.RegistrarEvento(bitacora);

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
                var materiaInfo = _listarMateriasLN.ObtenerMateriaPorId(IdMateria);

                _eliminarMateriasLN.EliminarMateria(IdMateria);

                // Bitácora: eliminación lógica de materia
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "DELETE",
                    Tabla = "Materias",
                    Descripcion = $"Eliminación lógica de materia ID: {IdMateria} - '{materiaInfo.Nombre}' - Estado cambiado a inactivo"
                };
                _bitacora.RegistrarEvento(bitacora);

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
