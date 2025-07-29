using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.agregarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.calificaciones;
using Campus.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN;
using Microsoft.AspNet.Identity;

namespace Campus.Web.Controllers
{
    public class CalificacionesController : Controller
    {
        private readonly IAgregarCalificacionLN _agregarCalificacionLN;
        private readonly IEditarCalificacionLN _editarCalificacionLN;
        private readonly IEliminarCalificacionLN _eliminarCalificacionLN;
        private readonly IListarCalificacionesLN _listarCalificacionesLN;

        public CalificacionesController()
        {
            _agregarCalificacionLN = new AgregarCalificacionLN();
            _editarCalificacionLN = new EditarCalificacionLN();
            _eliminarCalificacionLN = new EliminarCalificacionLN();
            _listarCalificacionesLN = new ListarCalificacionesLN();
        }

        public async Task<ActionResult> Index(int? idGrupo)
        {
            if (idGrupo == null)
            {
                var lista = await _listarCalificacionesLN.ListarCalificaciones();
                return View(lista);
            }
            else
            {
                var lista = await _listarCalificacionesLN.ListarCalificacionesPorGrupoAsync(idGrupo.Value);
                return View(lista);
            }
        }


        // GET: calificar
        [HttpGet]
        public ActionResult Create(int id)
        {
            var modelo = new CalificacionesDto
            {
                id_entrega = id,
                fecha_calificacion = DateTime.Now
            };
            return View(modelo); 
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection form)
        {
            var calificacion = new CalificacionesDto();

            if (int.TryParse(form["id_entrega"], out int idEntrega))
                calificacion.id_entrega = idEntrega;

            if (decimal.TryParse(form["calificacion"], out decimal nota))
                calificacion.calificacion = nota;

            calificacion.comentario = form["comentario"];
            calificacion.fecha_calificacion = DateTime.Now;

            System.Diagnostics.Debug.WriteLine($"Manual POST: id_entrega={calificacion.id_entrega}, calificacion={calificacion.calificacion}, comentario={calificacion.comentario}");

            if (ModelState.IsValid)
            {
                await _agregarCalificacionLN.AgregarCalificacion(calificacion);
                return RedirectToAction("Index", "Entregas");
            }

            return View(calificacion);
        }




        //Edit calificacion
        public async Task<ActionResult> Edit(int id)
        {
            var calificaciones = (await _listarCalificacionesLN.ListarCalificaciones()).ToList();
            var calificacion = calificaciones.FirstOrDefault(e => e.id_calificacion == id);

            if (calificacion == null)
                return HttpNotFound();

            return View(calificacion);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CalificacionesDto calificacion)
        {
            if (ModelState.IsValid)
            {
                await _editarCalificacionLN.EditarCalificacion(calificacion);
                return RedirectToAction("Index");
            }

            return View(calificacion);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var calificaciones = (await _listarCalificacionesLN.ListarCalificaciones()).ToList();
            var calificacion = calificaciones.FirstOrDefault(e => e.id_calificacion == id);

            if (calificacion == null)
                return HttpNotFound();

            return View(calificacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _eliminarCalificacionLN.EliminarCalificacion(id);
            return RedirectToAction("Index");
        }


       

        public async Task<ActionResult> ObtenerCalificacionPorID(int id)
        {
            var calificaciones = await _listarCalificacionesLN.ListarCalificaciones();
            var calificacion = calificaciones.FirstOrDefault(e => e.id_calificacion == id);

            if (calificacion == null)
                return HttpNotFound();

            return View(calificacion);
        }

        [Authorize(Roles = "Estudiantes")]
        public async Task<ActionResult> MisCalificaciones()
        {
            var idEstudiante = User.Identity.GetUserId();
            var lista = await _listarCalificacionesLN.ListarCalificacionesPorEstudianteAsync(idEstudiante);
            return View(lista);
        }
    }
}