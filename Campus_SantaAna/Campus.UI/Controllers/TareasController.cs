using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.editarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EditarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EliminarTareaLN;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using System.Web.Mvc.Html;
using System.Linq;
using System.IO;
using System;
using Microsoft.AspNet.Identity;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;

namespace Campus.UI.Controllers
{
    public class TareasController : Controller
    {
        private readonly IListarTareaLN _listarTareaLN;
        private readonly IAgregarTareaLN _agregarTareaLN;
        private readonly IEditarTareaLN _editarTareaLN;
        private readonly IEliminarTareaLN _eliminarTareaLN;
        private readonly IListarGruposLN _listarGruposLN;
        private readonly IListarMateriasLN _listarMateriasLN;

        public TareasController()
        {
            _listarTareaLN = new ListarTareaLN();
            _agregarTareaLN = new AgregarTareaLN();
            _editarTareaLN = new EditarTareaLN();
            _eliminarTareaLN = new EliminarTareaLN();
            _listarGruposLN = new ListarGruposLN();
            _listarMateriasLN = new ListarMateriasLN();

        }

        // GET: Tareas/ListarTareas
        public async Task<ActionResult> ListarTareas(int? grupoId)
        {
            var tareas = await _listarTareaLN.ListarTareasAsync();

            // Filtrar si se pasó grupoId
            if (grupoId.HasValue && grupoId.Value > 0)
            {
                tareas = tareas.Where(t => t.Id_grupo == grupoId.Value);
            }

            var grupos = _listarGruposLN.ListarGrupos();
            ViewBag.IdGrupo = new SelectList(grupos, "id_grupo", "nombre_grupo", grupoId ?? 0);

            return View(tareas);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            var grupos = _listarGruposLN.ListarGrupos();
            var materias = _listarMateriasLN.ListarMaterias();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materias, "Id_Materia", "nombre");
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TareaDto tarea)
        {
            var grupos = _listarGruposLN.ListarGrupos();
            var materias = _listarMateriasLN.ListarMaterias();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materias, "Id_Materia", "nombre");
            if (ModelState.IsValid)
            {
                try
                {

                    if (tarea.Archivo != null && tarea.Archivo.ContentLength > 0)
                    {
                        ComprobarTipodeArchivo(tarea, out string[] extensionesPermitidas, out string extensionArchivo);
                        if (!extensionesPermitidas.Contains(extensionArchivo))
                        {
                            ModelState.AddModelError("", "Tipo de archivo no permitido.");
                            return View(tarea);
                        }
                        GuardarArchivo(tarea);
                    }

                    tarea.FechaModificacion = DateTime.Now;

                    await _agregarTareaLN.AgregarTarea(tarea);
                    return RedirectToAction("ListarTareas");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la tarea: " + ex.Message);
                    return View(tarea);

                }
            }
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            var grupos = _listarGruposLN.ListarGrupos();
            var materias = _listarMateriasLN.ListarMaterias();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materias, "Id_Materia", "nombre");
            return View(tarea);
        }



        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TareaDto tarea)
        {
            var grupos = _listarGruposLN.ListarGrupos();
            var materias = _listarMateriasLN.ListarMaterias();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materias, "Id_Materia", "nombre");

            if (!ModelState.IsValid)
                return View(tarea);

            try
            {
                string archivoAnterior = Request.Form["ArchivoAdjuntoActual"];
                bool eliminarArchivo = Request.Form["EliminarArchivo"] == "true";

                if (eliminarArchivo && !string.IsNullOrEmpty(archivoAnterior))
                {
                    string rutaCompleta = Server.MapPath(archivoAnterior);
                    if (System.IO.File.Exists(rutaCompleta))
                        System.IO.File.Delete(rutaCompleta);

                    tarea.ArchivoAdjunto = null;
                }
                else if (tarea.Archivo != null && tarea.Archivo.ContentLength > 0)
                {
                    if (archivoAnterior != null)
                    {
                        string rutaCompleta = Server.MapPath(archivoAnterior);
                        System.IO.File.Delete(rutaCompleta);
                    }

                    ComprobarTipodeArchivo(tarea, out string[] extensionesPermitidas, out string extensionArchivo);

                    if (!extensionesPermitidas.Contains(extensionArchivo))
                    {
                        ModelState.AddModelError("", "Tipo de archivo no permitido.");
                        return View(tarea);
                    }

                    GuardarArchivo(tarea);
                }
                else
                {
                    tarea.ArchivoAdjunto = archivoAnterior;
                }

                // 5. Validación de fechas
                tarea.FechaModificacion = DateTime.Now;

                if (tarea.FechaEntrega < tarea.FechaPublicacion)
                {
                    ModelState.AddModelError("FechaEntrega", "La fecha de entrega no puede ser anterior a la fecha de publicacion");
                    return View(tarea);
                }

                // 6. Guardar cambios
                await _editarTareaLN.EditarTarea(id, tarea);

                return RedirectToAction("ListarTareas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al editar la tarea: " + ex.Message);
                return View(tarea);
            }
        }


        // GET: Tareas/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _eliminarTareaLN.EliminarTarea(id);
            return RedirectToAction("ListarTareas");
        }

        // GET: Tareas/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            ViewBag.Materia = _listarMateriasLN.ObtenerMateriaPorId(tarea.IdMateria).Nombre;
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }
        // GET: Tareas/MisTareas
        public async Task<ActionResult> MisTareas()
        {
            try
            {
                string idUsuario = User.Identity.GetUserId();


                if (string.IsNullOrWhiteSpace(idUsuario))
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Usuario no identificado");

                var tareas = await _listarTareaLN.ListarTareasPorEstudiante(idUsuario);

                return View(tareas);
            }
            catch (Exception ex)
            {
                // Podés loguear el error acá si querés
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Error al obtener tareas: " + ex.Message);
            }
        }



        private static void ComprobarTipodeArchivo(TareaDto tarea, out string[] extensionesPermitidas, out string extensionArchivo)
        {
            extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".pptx", ".xlsx", ".txt", ".doc" };
            extensionArchivo = Path.GetExtension(tarea.Archivo.FileName).ToLower();
        }

        private void GuardarArchivo(TareaDto tarea)
        {
            // Ruta del servidor donde se guardará el archivo
            var nombreArchivo = Path.GetFileName(tarea.Archivo.FileName);
            var rutaCarpeta = Server.MapPath("~/Uploads/");
            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            // Crear carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
            {
                tarea.Archivo.InputStream.CopyTo(fileStream);
            }

            // Guardar solo la ruta relativa en la base de datos
            tarea.ArchivoAdjunto = "~/Uploads/" + nombreArchivo;
        }


    }
}
