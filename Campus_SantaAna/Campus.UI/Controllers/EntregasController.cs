using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaNegocio.entregas.agregarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.editarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.eliminarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN;
using Campus.LogicaNegocio.Entregas.EditarEntregaLN;
using Campus.LogicaNegocio.Entregas.EliminarEntregaLN;
using Campus.LogicaNegocio.Entregas.ListarEntregaLN;
using Microsoft.AspNet.Identity;

namespace Campus.Web.Controllers
{
    // [Authorize]
    public class EntregasController : Controller
    {
        private readonly IAgregarEntregaLN _agregarEntregaLN;
        private readonly IEditarEntregaLN _editarEntregaLN;
        private readonly IEliminarEntregaLN _eliminarEntregaLN;
        private readonly IListarEntregasLN _listarEntregasLN;

        public EntregasController()
        {
            _agregarEntregaLN = new AgregarEntregaLN();
            _editarEntregaLN = new EditarEntregaLN();
            _eliminarEntregaLN = new EliminarEntregaLN();
            _listarEntregasLN = new ListarEntregasLN();
        }

        public async Task<ActionResult> Index(int? idGrupo)
        {
            if (idGrupo == null)
            {
                var lista = await _listarEntregasLN.ListarEntregas();
                return View(lista);
            }
            else
            {
                var lista = await _listarEntregasLN.ListarEntregasPorGrupoAsync(idGrupo.Value);
                return View(lista);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        //Create entrega
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EntregasDto entrega)
        {
            if (ModelState.IsValid)
            {
                await _agregarEntregaLN.AgregarEntrega(entrega);
                return RedirectToAction("Index");
            }

            return View(entrega);
        }
        //Edit entrega
        public async Task<ActionResult> Edit(int id)
        {
            var entregas = (await _listarEntregasLN.ListarEntregas()).ToList();
            var entrega = entregas.FirstOrDefault(e => e.id_entrega == id);

            if (entrega == null)
                return HttpNotFound();

            //calificacion 
            var calificaciones = await new ListarCalificacionesLN().ListarCalificaciones();
            var calificacion = calificaciones.FirstOrDefault(c => c.id_entrega == entrega.id_entrega);

            entrega.Calificacion = calificacion;

            return View(entrega);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EntregasDto entrega)
        {
            if (ModelState.IsValid)
            {
                await _editarEntregaLN.EditarEntrega(entrega);
                return RedirectToAction("Index");
            }

            return View(entrega);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entregas = (await _listarEntregasLN.ListarEntregas()).ToList();
            var entrega = entregas.FirstOrDefault(e => e.id_entrega == id);

            if (entrega == null)
                return HttpNotFound();

            return View(entrega);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _eliminarEntregaLN.EliminarEntrega(id);
            return RedirectToAction("Index");
        }

        // GET: Entregas/Entregar/5
        public ActionResult Entregar(int id)
        {
            var entrega = new EntregasDto
            {
                id_tarea = id,
                id_estudiante = User.Identity.Name,
                fecha_entrega = DateTime.Now
            };
            return View(entrega);
        }

        public async Task<ActionResult> ObtenerEntregaPorID(int id)
        {
            var entregas = await _listarEntregasLN.ListarEntregas();
            var entrega = entregas.FirstOrDefault(e => e.id_entrega == id);

            if (entrega == null)
                return HttpNotFound();

            return View(entrega);
        }

        [Authorize(Roles = "Estudiantes")]
        public async Task<ActionResult> MisEntregas()
        {
            var idEstudiante = User.Identity.Name;
            var lista = await _listarEntregasLN.ListarEntregasPorEstudianteAsync(idEstudiante);
            return View(lista);
        }

        // 🚀 NUEVO: POST para entregar tarea
       
        [Authorize(Roles = "Estudiantes")]
        [HttpGet]
        public ActionResult SubirEntrega(int idTarea)
        {
            var entrega = new EntregasDto
            {
                id_tarea = idTarea,
                id_estudiante = User.Identity.GetUserId(), // Asegúrate de tener el using correcto
                fecha_entrega = DateTime.Now
            };

            return View("SubirEntrega", entrega); // Usa vista personalizada
        }

        [Authorize(Roles = "Estudiantes")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubirEntrega(EntregasDto entrega, HttpPostedFileBase archivo)
        {
            if (archivo != null && archivo.ContentLength > 0)
            {
                var nombreArchivo = Path.GetFileName(archivo.FileName);
                var rutaCarpeta = Server.MapPath("~/Uploads/Entregas");
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                archivo.SaveAs(rutaCompleta);

                entrega.archivo_entregado = "~/Uploads/Entregas/" + nombreArchivo;
            }

            entrega.fecha_entrega = DateTime.Now;
            entrega.id_estudiante = User.Identity.GetUserId();

            await _agregarEntregaLN.AgregarEntrega(entrega);
            return RedirectToAction("MisEntregas");
        }

    }
}
