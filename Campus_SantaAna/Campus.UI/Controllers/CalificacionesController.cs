using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.tareas.listarTareaAD;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.agregarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using Campus.LogicaDeNegocio.calificaciones;
using Campus.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using Campus.LogicaNegocio.Entregas.ListarEntregaLN;
using Campus.UI.Filtros;
using Microsoft.AspNet.Identity;

namespace Campus.Web.Controllers
{
    //[Authorize]
    public class CalificacionesController : Controller
    {
        private readonly IAgregarCalificacionLN _agregarCalificacionLN;
        private readonly IEditarCalificacionLN _editarCalificacionLN;
        private readonly IEliminarCalificacionLN _eliminarCalificacionLN;
        private readonly IListarCalificacionesLN _listarCalificacionesLN;
        private readonly IListarEntregasLN _listarEntregasLN;
        private readonly IListarTareaLN _listarTareas;

        public CalificacionesController()
        {
            _agregarCalificacionLN = new AgregarCalificacionLN();
            _editarCalificacionLN = new EditarCalificacionLN();
            _eliminarCalificacionLN = new EliminarCalificacionLN();
            _listarCalificacionesLN = new ListarCalificacionesLN();
            _listarEntregasLN = new ListarEntregasLN();
            _listarTareas = new ListarTareaLN();
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
        public async Task<ActionResult> Edit(FormCollection form)
        {
            var calificacion = new CalificacionesDto();

            // Binding manual con validación de tipos
            int.TryParse(form["id_calificacion"], out int idCalificacion);
            int.TryParse(form["id_entrega"], out int idEntrega);
            DateTime.TryParse(form["fecha_calificacion"], out DateTime fechaCalificacion);
            decimal.TryParse(form["calificacion"], System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out decimal calificacionValor);

            calificacion.id_calificacion = idCalificacion;
            calificacion.id_entrega = idEntrega;
            calificacion.fecha_calificacion = fechaCalificacion;
            calificacion.calificacion = calificacionValor;
            calificacion.comentario = form["comentario"];

            // Validaciones
            if (calificacion.id_calificacion <= 0)
                ModelState.AddModelError("id_calificacion", "ID de calificación requerido");
            if (calificacion.calificacion < 0 || calificacion.calificacion > 100)
                ModelState.AddModelError("calificacion", "La calificación debe estar entre 0 y 100");
            if (string.IsNullOrEmpty(calificacion.comentario))
                ModelState.AddModelError("comentario", "El comentario es requerido");

            if (ModelState.IsValid)
            {
                var calificacionOriginal = (await _listarCalificacionesLN.ListarCalificaciones())
                                            .FirstOrDefault(c => c.id_calificacion == calificacion.id_calificacion);
                if (calificacionOriginal != null)
                {
                    await _editarCalificacionLN.EditarCalificacion(calificacion.id_calificacion, calificacion);
                    return RedirectToAction("Index", "Entregas");
                }
                else
                {
                    return HttpNotFound();
                }
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
            var entregas = await _listarEntregasLN.ListarEntregas();

            foreach (var calificacion in lista)
            {
                var entrega = entregas.FirstOrDefault(e => e.id_entrega == calificacion.id_entrega);
                if (entrega != null)
                {
                    var tarea = await _listarTareas.ObtenerPorIdAsync(entrega.id_tarea);
                    if (tarea != null)
                    {
                        entrega.Tarea = tarea;
                    }
                    calificacion.Entrega = entrega;
                }
            }

            return View(lista);
        }
    }
}