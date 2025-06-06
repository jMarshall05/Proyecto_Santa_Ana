using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.editarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN;
using Campus.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EditarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EliminarTareaLN;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System;
using System.Reflection;

namespace Campus.UI.Controllers
{
    public class TareasController : Controller
    {
        private readonly IListarTareaLN _listarTareaLN;
        private readonly IAgregarTareaLN _agregarTareaLN;
        private readonly IEditarTareaLN _editarTareaLN;
        private readonly IEliminarTareaLN _eliminarTareaLN;

        public TareasController()
        {
            _listarTareaLN = new ListarTareaLN();
            _agregarTareaLN = new AgregarTareaLN();
            _editarTareaLN = new EditarTareaLN();
            _eliminarTareaLN = new EliminarTareaLN();
        }

        // GET: Tareas/ListarTareas
        public async Task<ActionResult> ListarTareas()
        {
            var tareas = await _listarTareaLN.ListarTareasAsync();
            return View(tareas);
        }

        // GET: Tareas/Create
        // GET: Tareas/Create
        public ActionResult Create()
        {
            var materias = ObtenerMaterias(); // Método que retorna una lista de materias
            ViewBag.Materias = new SelectList(materias, "IdMateria", "Nombre");
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TareaDto tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (tarea.Archivo != null && tarea.Archivo.ContentLength > 0)
                    {
                        // Ruta del servidor donde se guardará el archivo
                        var nombreArchivo = Path.GetFileName(tarea.Archivo.FileName);
                        var rutaCarpeta = Server.MapPath("~/Uploads/");
                        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                        // Crear carpeta si no existe
                        if (!Directory.Exists(rutaCarpeta))
                            Directory.CreateDirectory(rutaCarpeta);

                        // Guardar archivo
                        tarea.Archivo.SaveAs(rutaCompleta);

                        // Guardar solo la ruta relativa en la base de datos
                        tarea.ArchivoAdjunto = "~/Uploads/" + nombreArchivo;
                    }

                    // Fechas automáticas
                    tarea.FechaCreacion = DateTime.Now;

                    await _agregarTareaLN.AgregarTarea(tarea);
                    return RedirectToAction("ListarTareas");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la tarea: " + ex.Message);
                }
            }

            return View(tarea);
        }


        // Método auxiliar para simular la carga de materias
        private List<MateriaDto> ObtenerMaterias()
        {
            // Aquí podés cambiarlo para obtenerlos de una lógica de negocio real
            return new List<MateriaDto>
    {
        new MateriaDto { IdMateria = 1, Nombre = "Matemáticas" },
        new MateriaDto { IdMateria = 2, Nombre = "Español" },
        new MateriaDto { IdMateria = 3, Nombre = "Ciencias" }
    };
        }


        // GET: Tareas/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TareaDto tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (tarea.Archivo != null && tarea.Archivo.ContentLength > 0)
                    {
                        // Ruta del servidor donde se guardará el archivo
                        var nombreArchivo = Path.GetFileName(tarea.Archivo.FileName);
                        var rutaCarpeta = Server.MapPath("~/Uploads/");
                        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                        // Crear carpeta si no existe
                        if (!Directory.Exists(rutaCarpeta))
                            Directory.CreateDirectory(rutaCarpeta);

                        // Guardar archivo
                        tarea.Archivo.SaveAs(rutaCompleta);

                        // Guardar solo la ruta relativa en la base de datos
                        tarea.ArchivoAdjunto = "~/Uploads/" + nombreArchivo;
                    }

                    // Mantenemos la fecha original de creación
                    var tareaOriginal = await _listarTareaLN.ObtenerPorIdAsync(id);
                    // Actualizamos la fecha de modificación
                    tarea.FechaModificacion = DateTime.Now;

                    // Validación de fecha de publicación
                    if (tarea.FechaPublicacion < tarea.FechaCreacion)
                    {
                        ModelState.AddModelError("FechaPublicacion", "La fecha de publicación no puede ser anterior a la fecha de creación");
                        return View(tarea);
                    }

                    await _editarTareaLN.EditarTarea(id, tarea);
                    return RedirectToAction("ListarTareas");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al editar la tarea: " + ex.Message);
                }
            }
            return View(tarea);
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
            try
            {
                await _eliminarTareaLN.EliminarTarea(id);
                return RedirectToAction("ListarTareas");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la tarea.");
                var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
                return View("Delete", tarea);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }
    }
}
