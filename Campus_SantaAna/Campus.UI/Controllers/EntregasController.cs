using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.calificaciones.listarCalificacionLN;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.LogicaNegocio.entregas.agregarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.editarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.eliminarEntregaLN;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.calificaciones.listarCalificacionesLN;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Campus.LogicaNegocio.Entregas.EditarEntregaLN;
using Campus.LogicaNegocio.Entregas.EliminarEntregaLN;
using Campus.LogicaNegocio.Entregas.ListarEntregaLN;
using Campus.UI.Filtros;
using Microsoft.AspNet.Identity;

namespace Campus.Web.Controllers
{
    [Authorize]
    public class EntregasController : Controller
    {
        private readonly IAgregarEntregaLN _agregarEntregaLN;
        private readonly IEditarEntregaLN _editarEntregaLN;
        private readonly IEliminarEntregaLN _eliminarEntregaLN;
        private readonly IListarEntregasLN _listarEntregasLN;
        private readonly IListarCalificacionesLN _listarCalificacionesLN; 
        private readonly IObtenerUsuariosPorIdLN _obtenerUsuariosPorId;
        private readonly IListarTareaLN _listarTareas;
        private readonly IListarMateriasLN _listarMaterias;

        public EntregasController()
        {
            _agregarEntregaLN = new AgregarEntregaLN();
            _editarEntregaLN = new EditarEntregaLN();
            _eliminarEntregaLN = new EliminarEntregaLN();
            _listarEntregasLN = new ListarEntregasLN();
            _listarCalificacionesLN = new ListarCalificacionesLN();
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdLN();
            _listarTareas = new ListarTareaLN();
            _listarMaterias = new ListarMateriasLN();
        }

        public async Task<ActionResult> Index(int? idGrupo)
        {
            List<EntregasDto> lista;

            if (idGrupo == null)
                lista = (await _listarEntregasLN.ListarEntregas()).ToList();
            else
                lista = (await _listarEntregasLN.ListarEntregasPorGrupoAsync(idGrupo.Value)).ToList();

            // Llenar el Estudiante para evitar null en la vista
            foreach (var entrega in lista)
            {
                entrega.Estudiante = _obtenerUsuariosPorId.ObtenerUsuarioPorId(entrega.id_estudiante);
            }

            return View(lista);
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
            var calificacion = (await _listarCalificacionesLN.ListarCalificacionesPorEstudianteAsync(entrega.id_estudiante)).Where(c=>c.id_entrega.Equals(id) && c.Estado == true).FirstOrDefault();
            var usuario = _obtenerUsuariosPorId.ObtenerUsuarioPorId(entrega.id_estudiante);
            var tarea = await _listarTareas.ObtenerPorIdAsync(entrega.id_tarea);
            entrega.Estudiante = usuario;
            entrega.Tarea = tarea;
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

            // 🔹 Cargar estudiante y tarea para que la vista no dé NullReference
            entrega.Estudiante = _obtenerUsuariosPorId.ObtenerUsuarioPorId(entrega.id_estudiante);
            entrega.Tarea = await _listarTareas.ObtenerPorIdAsync(entrega.id_tarea);

            return View(entrega);
        }


        [Authorize(Roles = "Estudiantes")]
        public async Task<ActionResult> MisEntregas()
        {
            var idEstudiante = User.Identity.GetUserId();
            var lista = await _listarEntregasLN.ListarEntregasPorEstudianteAsync(idEstudiante);
            ViewBag.Materias = _listarMaterias.ListarMaterias();
            foreach(var tarea in lista)
            {
                tarea.Tarea = await _listarTareas.ObtenerPorIdAsync(tarea.id_tarea);
            }
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
                var nombreArchivo = Path.GetFileNameWithoutExtension(archivo.FileName);
                var extension= Path.GetExtension(archivo.FileName);
                var rutaCarpeta = Server.MapPath("~/Uploads/Entregas");
                var rutaCompleta = Path.Combine(rutaCarpeta, $"{nombreArchivo}_{Guid.NewGuid()}{extension}");

                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                archivo.SaveAs(rutaCompleta);

                entrega.archivo_entregado = "~/Uploads/Entregas/" + Path.GetFileName(rutaCompleta);
            }

            entrega.id_estudiante = User.Identity.GetUserId();
        
            await _agregarEntregaLN.AgregarEntrega(entrega);
            return RedirectToAction("MisEntregas");
        }

    }
}
