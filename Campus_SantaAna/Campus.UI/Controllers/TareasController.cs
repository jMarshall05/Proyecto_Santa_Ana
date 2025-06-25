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
            _listarTareaLN = new ListarTareaLN(new Campus.AccesoDatos.tareas.listarTareaAD.ListarTareaAD());
            _agregarTareaLN = new AgregarTareaLN(new Campus.AccesoDatos.Tareas.AgregarTareaAD.AgregarTareaAD());
            _editarTareaLN = new EditarTareaLN(new Campus.AccesoDatos.Tareas.EditarTareaAD.EditarTareaAD());
            _eliminarTareaLN = new EliminarTareaLN(new Campus.AccesoDatos.tareas.eliminarTareaAD.EliminarTareaAD());
        }

        // GET: Tareas/ListarTareas
        public async Task<ActionResult> ListarTareas(int? grupoId)
        {
            var tareas = await _listarTareaLN.ListarTareasAsync();

            // Filtrar si se pasó grupoId
            if (grupoId.HasValue && grupoId.Value > 0)
            {
                tareas = tareas.Where(t => t.id_grupo == grupoId.Value);
            }

            var grupos = await _listarTareaLN.ListarGruposAsync();

            ViewBag.IdGrupo = new SelectList(grupos, "id_grupo", "nombre_grupo", grupoId ?? 0);

            return View(tareas);
        }

        // GET: Tareas/Create
        public async Task<ActionResult> Create()
        {
            var grupos = await _listarTareaLN.ListarGruposAsync();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo");
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TareaDto tarea)
        {
            if (ModelState.IsValid)
            {
                await _agregarTareaLN.AgregarTarea(tarea);
                return RedirectToAction("ListarTareas");
            }

            var grupos = await _listarTareaLN.ListarGruposAsync();
            ViewBag.IdGrupo = new SelectList(grupos, "id_grupo", "nombre_grupo", tarea.id_grupo);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            var grupos = await _listarTareaLN.ListarGruposAsync();
            ViewBag.Grupos = new SelectList(grupos, "id_grupo", "nombre_grupo", tarea.id_grupo);
            return View(tarea);
        }



        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TareaDto tarea)
        {
            if (ModelState.IsValid)
            {
                await _editarTareaLN.EditarTarea(id, tarea);
                return RedirectToAction("ListarTareas");
            }

            var grupos = await _listarTareaLN.ListarGruposAsync();
            ViewBag.IdGrupo = new SelectList(grupos, "id_grupo", "nombre_grupo", tarea.id_grupo);
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
            await _eliminarTareaLN.EliminarTarea(id);
            return RedirectToAction("ListarTareas");
        }
        // GET: Tareas/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tarea = await _listarTareaLN.ObtenerPorIdAsync(id);
            if (tarea == null)
                return HttpNotFound();

            return View(tarea);
        }
       




    }
}
