using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.agregarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.editarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.calificaciones;
using Campus.LogicaDeNegocio.calificaciones.eliminarCalificacionLN;
using Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using Campus.LogicaNegocio.Entregas.ListarEntregaLN;
using Microsoft.AspNet.Identity;

namespace Campus.Web.Controllers
{
    [Authorize]
    public class CalificacionesController : Controller
    {
        private readonly IAgregarCalificacionLN _agregarCalificacionLN;
        private readonly IEditarCalificacionLN _editarCalificacionLN;
        private readonly IEliminarCalificacionLN _eliminarCalificacionLN;
        private readonly IListarCalificacionesLN _listarCalificacionesLN;
        private readonly IListarEntregasLN _listarEntregasLN;
        private readonly IListarTareaLN _listarTareas;
        private readonly IBitacoraLN _bitacora;


        public CalificacionesController()
        {
            _agregarCalificacionLN = new AgregarCalificacionLN();
            _editarCalificacionLN = new EditarCalificacionLN();
            _eliminarCalificacionLN = new EliminarCalificacionLN();
            _listarCalificacionesLN = new ListarCalificacionesLN();
            _listarEntregasLN = new ListarEntregasLN();
            _listarTareas = new ListarTareaLN();
            _bitacora = new BitacoraLN();
        }

        public async Task<ActionResult> Index(int? idGrupo)
        {
            if (idGrupo == null)
            {
                var lista = (await _listarCalificacionesLN.ListarCalificaciones()).Where(c => c.Estado == true).ToList();
                return View(lista.Where(l => l.Estado == true));
            }
            else
            {
                var lista = (await _listarCalificacionesLN.ListarCalificacionesPorGrupoAsync(idGrupo.Value)).Where(c => c.Estado == true);
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

            if (ModelState.IsValid)
            {
                await _agregarCalificacionLN.AgregarCalificacion(calificacion);

                var entrega = (await _listarEntregasLN.ListarEntregas()).FirstOrDefault(e => e.id_entrega == idEntrega);
                var tarea = entrega != null ? await _listarTareas.ObtenerPorIdAsync(entrega.id_tarea) : null;
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "INSERT",
                    Tabla = "Calificaciones",
                    Descripcion = $"Creación de calificación para entrega ID: {idEntrega} - Nota: {nota} - Tarea: {tarea?.Titulo ?? "N/A"} - Estudiante: {entrega?.id_estudiante ?? "N/A"}"
                };
                _bitacora.RegistrarEvento(bitacora);

                return RedirectToAction("Index", "Entregas");
            }

            return View(calificacion);
        }




        //Edit calificacion
        public async Task<ActionResult> Edit(int id)
        {
            var calificaciones = (await _listarCalificacionesLN.ListarCalificaciones()).Where(c => c.Estado == true).ToList();
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

                    // Bitácora: actualización de calificación
                    var entrega = (await _listarEntregasLN.ListarEntregas()).FirstOrDefault(e => e.id_entrega == idEntrega);
                    var tarea = entrega != null ? await _listarTareas.ObtenerPorIdAsync(entrega.id_tarea) : null;
                    var bitacora = new BitacoraDto
                    {
                        Fecha = DateTime.Now,
                        Usuario = User.Identity.GetUserId(),
                        Accion = "UPDATE",
                        Tabla = "Calificaciones",
                        Descripcion = $"Actualización de calificación ID: {idCalificacion} - Nueva nota: {calificacionValor} - Entrega ID: {idEntrega} - Tarea: {tarea?.Titulo ?? "N/A"}"
                    };
                    _bitacora.RegistrarEvento(bitacora);

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
            var calificaciones = (await _listarCalificacionesLN.ListarCalificaciones()).Where(c => c.Estado == true).ToList();
            var calificacion = calificaciones.FirstOrDefault(e => e.id_calificacion == id);

            if (calificacion == null)
                return HttpNotFound();

            return View(calificacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var calificacionInfo = (await _listarCalificacionesLN.ListarCalificaciones())
                                   .FirstOrDefault(c => c.id_calificacion == id);

            await _eliminarCalificacionLN.EliminarCalificacion(id);

            // Bitácora: eliminación lógica de calificación
            var entrega = calificacionInfo != null ?
                          (await _listarEntregasLN.ListarEntregas()).FirstOrDefault(e => e.id_entrega == calificacionInfo.id_entrega) : null;
            var bitacora = new BitacoraDto
            {
                Fecha = DateTime.Now,
                Usuario = User.Identity.GetUserId(),
                Accion = "DELETE",
                Tabla = "Calificaciones",
                Descripcion = $"Eliminación lógica de calificación ID: {id} - Nota: {calificacionInfo?.calificacion ?? 0} - Entrega ID: {calificacionInfo?.id_entrega ?? 0} - Estado cambiado a inactivo"
            };
            _bitacora.RegistrarEvento(bitacora);

            return RedirectToAction("Index", "Entregas");
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