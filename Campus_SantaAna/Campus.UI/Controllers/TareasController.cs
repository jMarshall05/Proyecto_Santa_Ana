using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.editarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.eliminarTareaLN;
using Campus.Abstracciones.LogicaDeNegocio.tareas.listarTareasLN;
using Campus.Abstracciones.LogicaNegocio.entregas.listarEntregaLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.tareas.agregarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EditarTareaLN;
using Campus.LogicaDeNegocio.Tareas.EliminarTareaLN;
using Campus.LogicaDeNegocio.Tareas.ListarTareaLN;
using Campus.LogicaNegocio.Entregas.ListarEntregaLN;
using Microsoft.AspNet.Identity;

namespace Campus.UI.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        private readonly IListarTareaLN _listarTareaLN;
        private readonly IAgregarTareaLN _agregarTareaLN;
        private readonly IEditarTareaLN _editarTareaLN;
        private readonly IEliminarTareaLN _eliminarTareaLN;
        private readonly IListarGruposLN _listarGruposLN;
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IListarCursoLN _listarCursosLN;
        private readonly IListarEntregasLN _listarEntregasLN;
        private readonly IBitacoraLN _bitacora;



        public TareasController()
        {
            _listarTareaLN = new ListarTareaLN();
            _agregarTareaLN = new AgregarTareaLN();
            _editarTareaLN = new EditarTareaLN();
            _eliminarTareaLN = new EliminarTareaLN();
            _listarGruposLN = new ListarGruposLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarCursosLN = new ListarCursosLN();
            _listarEntregasLN = new ListarEntregasLN();
            _bitacora = new BitacoraLN();

        }

        [Authorize(Roles = "Administradores,Profesores")]
        public async Task<ActionResult> ListarTareas(int? grupoId, int? materiaId)
        {
            var tareas = (await _listarTareaLN.ListarTareasAsync()).Where(t => t.Estado == true);
            var cursos = _listarCursosLN.ListarCursos().Where(c => c.Estado == true);
            var grupos = _listarGruposLN.ListarGrupos().Where(g => g.estado == true);
            var materias = _listarMateriasLN.ListarMaterias().Where(g => g.Estado == true);

            if (grupoId.HasValue && materiaId.HasValue)
            {
                tareas = tareas.Where(t => t.Id_grupo == grupoId && t.IdMateria == materiaId);
                ViewBag.Grupo = grupos.Where(g => g.id_grupo == grupoId).FirstOrDefault().nombre_grupo;
                ViewBag.Materia = materias.Where(m => m.Id_Materia == materiaId).FirstOrDefault().Nombre;
                ViewBag.grupoId = grupoId;
                ViewBag.materiaId = materiaId;
            }
            if (User.IsInRole("Profesores"))
            {
                cursos = _listarCursosLN.ListarCursos().Where(c => c.ProfesorId == User.Identity.GetUserId());
                var tareasPorProfesor = new List<TareaDto>();
                foreach (var curso in cursos)
                {
                    if (curso.ProfesorId == User.Identity.GetUserId())
                    {
                        tareasPorProfesor.AddRange(tareas.Where(t => t.Id_grupo == curso.GrupoId && t.IdMateria == curso.MateriaId));
                    }
                }
                tareas = tareasPorProfesor;
            }

            //foreach (var tarea in tareas)
            //{
            //    if (tarea.Calificacion == null)
            //    {
            //        // Aquí necesitarías implementar un método para obtener la calificación por tarea
            //        // tarea.Calificacion = await _obtenerCalificacionPorTarea(tarea.IdTarea);
            //    }
            //}


            ViewBag.IdGrupo = new SelectList(grupos, "id_grupo", "nombre_grupo", grupoId ?? 0);

            return View(tareas);
        }

        [Authorize(Roles = "Administradores,Profesores")]
        public ActionResult Create(int? idMateria, int? idGrupo)
        {
            if (idMateria.HasValue && idGrupo.HasValue)
            {
                ViewBag.idMateria = idMateria;
                ViewBag.idGrupo = idGrupo;
                return PartialView("_CreateParcial", new TareaDto());
            }

            var cursos = _listarCursosLN.ListarCursos().Where(c => c.ProfesorId == User.Identity.GetUserId());
            var materias = _listarMateriasLN.ListarMaterias().Where(m => m.Estado == true);
            var grupos = _listarGruposLN.ListarGrupos().Where(g => g.estado == true);
            var materiaFiltrado = new List<MateriaDto>();
            var gruposFiltrados = new List<GruposDto>();
            foreach (var curso in cursos)
            {
                if (curso.ProfesorId == User.Identity.GetUserId())
                {
                    materiaFiltrado.AddRange(materias.Where(m => m.Id_Materia == curso.MateriaId));
                    gruposFiltrados.AddRange(grupos.Where(g => g.id_grupo == curso.GrupoId));
                }
            }
            materiaFiltrado = materiaFiltrado.Distinct().ToList();
            gruposFiltrados = gruposFiltrados.Distinct().ToList();
            ViewBag.Grupos = new SelectList(gruposFiltrados, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materiaFiltrado, "Id_Materia", "nombre");
            return View();
        }

        [Authorize(Roles = "Administradores,Profesores")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TareaDto tarea)
        {
            tarea.asignado_por = User.Identity.GetUserId();

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

                    await _agregarTareaLN.AgregarTarea(tarea);

                    // Bitácora: inserción de nueva tarea
                    var materiaInfo = _listarMateriasLN.ObtenerMateriaPorId(tarea.IdMateria);
                    var grupoInfo = _listarGruposLN.BuscarGruposPorId(tarea.Id_grupo);
                    var bitacora = new BitacoraDto
                    {
                        Fecha = DateTime.Now,
                        Usuario = User.Identity.GetUserId(),
                        Accion = "INSERT",
                        Tabla = "Tareas",
                        Descripcion = $"Creación de tarea '{tarea.Titulo}' - Materia: {materiaInfo.Nombre}, Grupo: {grupoInfo.nombre_grupo}, Fecha entrega: {tarea.FechaEntrega:dd/MM/yyyy}"
                    };
                    _bitacora.RegistrarEvento(bitacora);

                    return Redirect(Request.Headers["Referer"].ToString());
                }
                catch (Exception ex)
                {
                    FiltrarMateriasCursosGrupos();
                    ModelState.AddModelError("", "Error al crear la tarea: " + ex.Message);
                    return View(tarea);
                }
            }
            FiltrarMateriasCursosGrupos();
            return View(tarea);
        }

        private void FiltrarMateriasCursosGrupos()
        {
            var cursos = _listarCursosLN.ListarCursos().Where(c => c.Estado == true);
            var materias = _listarMateriasLN.ListarMaterias().Where(m => m.Estado == true);
            var grupos = _listarGruposLN.ListarGrupos().Where(g => g.estado == true);

            var materiaFiltrado = new List<MateriaDto>();
            var gruposFiltrados = new List<GruposDto>();
            foreach (var curso in cursos)
            {
                if (curso.ProfesorId == User.Identity.GetUserId())
                {
                    materiaFiltrado.AddRange(materias.Where(m => m.Id_Materia == curso.MateriaId));
                    gruposFiltrados.AddRange(grupos.Where(g => g.id_grupo == curso.GrupoId));
                }
            }
            materiaFiltrado = materiaFiltrado.Distinct().ToList();

            ViewBag.Grupos = new SelectList(gruposFiltrados, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materiaFiltrado, "Id_Materia", "nombre");
        }

        [Authorize(Roles = "Administradores,Profesores")]
        public async Task<ActionResult> Edit(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();
            var materias = _listarMateriasLN.ListarMaterias().Where(m => m.Estado == true);
            var cursos = _listarCursosLN.ListarCursos().Where(c => c.Estado == true);
            var grupos = _listarGruposLN.ListarGrupos().Where(g => g.estado == true);
            var materiaFiltrado = new List<MateriaDto>();
            var gruposFiltrados = new List<GruposDto>();
            foreach (var curso in cursos)
            {
                if (curso.ProfesorId == User.Identity.GetUserId())
                {
                    materiaFiltrado.AddRange(materias.Where(m => m.Id_Materia == curso.MateriaId));
                    gruposFiltrados.AddRange(grupos.Where(g => g.id_grupo == curso.GrupoId));
                }
            }
            materiaFiltrado = materiaFiltrado.Distinct().ToList();

            ViewBag.Grupos = new SelectList(gruposFiltrados, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materiaFiltrado, "Id_Materia", "nombre");
            return View(tarea);
        }



        [Authorize(Roles = "Administradores,Profesores")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TareaDto tarea)
        {
            var grupos = _listarGruposLN.ListarGrupos().Where(g => g.estado == true);
            var materias = _listarMateriasLN.ListarMaterias().Where(m => m.Estado == true);
            var cursos = _listarCursosLN.ListarCursos().Where(c => c.Estado == true);
            var materiaFiltrado = new List<MateriaDto>();
            var gruposFiltrados = new List<GruposDto>();

            foreach (var curso in cursos)
            {
                if (curso.ProfesorId == User.Identity.GetUserId())
                {
                    materiaFiltrado.AddRange(materias.Where(m => m.Id_Materia == curso.MateriaId));
                    gruposFiltrados.AddRange(grupos.Where(g => g.id_grupo == curso.GrupoId));
                }
            }
            materiaFiltrado = materiaFiltrado.Distinct().ToList();

            ViewBag.Grupos = new SelectList(gruposFiltrados, "id_grupo", "nombre_grupo");
            ViewBag.Materia = new SelectList(materiaFiltrado, "Id_Materia", "nombre");

            if (!ModelState.IsValid)
                return View(tarea);

            try
            {
                string archivoAnterior = Request.Form["archivoAdjuntoActual"];
                bool eliminarArchivo = Request.Form["eliminarArchivo"] == "true";
                bool archivoModificado = false;

                if (eliminarArchivo && !string.IsNullOrEmpty(archivoAnterior))
                {
                    string rutaCompleta = Server.MapPath(archivoAnterior);
                    if (System.IO.File.Exists(rutaCompleta))
                        System.IO.File.Delete(rutaCompleta);

                    tarea.ArchivoAdjunto = null;
                    archivoModificado = true;
                }
                else if (tarea.Archivo != null && tarea.Archivo.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(archivoAnterior))
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
                    archivoModificado = true;
                }
                else
                {
                    tarea.ArchivoAdjunto = archivoAnterior;
                }

                if (tarea.FechaEntrega < tarea.FechaPublicacion)
                {
                    ModelState.AddModelError("FechaEntrega", "La fecha de entrega no puede ser anterior a la fecha de publicacion");
                    return View(tarea);
                }

                await _editarTareaLN.EditarTarea(id, tarea);

                // Bitácora: actualización de tarea
                var materiaInfo = _listarMateriasLN.ObtenerMateriaPorId(tarea.IdMateria);
                var grupoInfo = _listarGruposLN.BuscarGruposPorId(tarea.Id_grupo);
                var descripcionArchivo = archivoModificado ? " - Archivo adjunto modificado" : "";
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "UPDATE",
                    Tabla = "Tareas",
                    Descripcion = $"Actualización de tarea ID: {id} - '{tarea.Titulo}' - Materia: {materiaInfo.Nombre}, Grupo: {grupoInfo.nombre_grupo}{descripcionArchivo}"
                };
                _bitacora.RegistrarEvento(bitacora);

                return Redirect(Request.Headers["Referer"].ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al editar la tarea: " + ex.Message);
                return View(tarea);
            }
        }


        [Authorize(Roles = "Administradores,Profesores")]
        public async Task<ActionResult> Delete(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }

        [Authorize(Roles = "Administradores,Profesores")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var tareaInfo = await _listarTareaLN.ObtenerPorIdAsync(id);

            await _eliminarTareaLN.EliminarTarea(id);

            // Bitácora: eliminación lógica de tarea
            var bitacora = new BitacoraDto
            {
                Fecha = DateTime.Now,
                Usuario = User.Identity.GetUserId(),
                Accion = "DELETE",
                Tabla = "Tareas",
                Descripcion = $"Eliminación lógica de tarea ID: {id} - '{tareaInfo.Titulo}' - Estado cambiado a inactivo"
            };
            _bitacora.RegistrarEvento(bitacora);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            ViewBag.Materia = _listarMateriasLN.ObtenerMateriaPorId(tarea.IdMateria).Nombre;
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }
        [Authorize(Roles = "Estudiantes")]
        public async Task<ActionResult> MisTareas(int? materiaId, int? grupoId)
        {
            try
            {
                string idUsuario = User.Identity.GetUserId();


                if (string.IsNullOrWhiteSpace(idUsuario))
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Usuario no identificado");

                var tareas = (await _listarTareaLN.ListarTareasPorEstudiante(idUsuario)).Where(t => t.Estado == true);
                foreach (var tarea in tareas)
                {
                    if (tarea.Calificacion != null)
                        tarea.Calificacion.Entrega = (await _listarEntregasLN.ListarEntregas()).Where(e => e.id_entrega == tarea.Calificacion.id_entrega && e.estado == true).FirstOrDefault();
                }
                if (materiaId.HasValue && grupoId.HasValue)
                {
                    tareas = tareas.Where(t => t.IdMateria == materiaId && t.Id_grupo == grupoId);
                    return View(tareas);
                }
                else
                { return View(tareas); }


            }
            catch (Exception ex)
            {
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
            var nombreArchivo = Path.GetFileNameWithoutExtension(tarea.Archivo.FileName);
            var extension = Path.GetExtension(tarea.Archivo.FileName);
            var rutaCarpeta = Server.MapPath("~/Uploads/");
            var rutaCompleta = Path.Combine(rutaCarpeta, $"{nombreArchivo}_{Guid.NewGuid()}{extension}");


            // Crear carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
            {
                tarea.Archivo.InputStream.CopyTo(fileStream);
            }

            // Guardar solo la ruta relativa en la base de datos
            tarea.ArchivoAdjunto = "~/Uploads/" + Path.GetFileName(rutaCompleta);
        }


    }
}
