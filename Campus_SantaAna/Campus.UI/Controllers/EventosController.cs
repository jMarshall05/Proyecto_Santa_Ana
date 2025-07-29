using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.LogicaDeNegocio.Eventos.AgregarEventoLN;
using Campus.LogicaDeNegocio.Eventos.EditarEventoLN;
using Campus.LogicaDeNegocio.Eventos.EliminarEventoLN;
using Campus.LogicaDeNegocio.Eventos.ListarEventosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.AgregarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EditarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EliminarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.ListarEventosLN;

namespace Campus.UI.Controllers
{
    public class EventosController : Controller
    {
        private readonly IAgregarEventoLN _agregarEventoLN;
        private readonly IListarEventosLN _listarEventosLN;
        private readonly IEditarEventoLN _editarEventoLN;
        private readonly IEliminarEventoLN _eliminarEventoLN;

        public EventosController()
        {
            _agregarEventoLN = new AgregarEventoLN();
            _listarEventosLN = new ListarEventosLN();
            _editarEventoLN = new EditarEventoLN();
            _eliminarEventoLN = new EliminarEventoLN();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerEventos()
        {
            var eventos = await _listarEventosLN.ListarEventos();
            return Json(eventos.Select(e => new {
                id = e.Id,
                title = e.Titulo,
                start = e.FechaInicio.ToString("yyyy-MM-dd"),
                end = e.FechaFin.ToString("yyyy-MM-dd")
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AgregarEvento(string titulo, string fecha)
        {
            var nuevoEvento = new EventoDto
            {
                Titulo = titulo,
                FechaInicio = DateTime.Parse(fecha),
                FechaFin = DateTime.Parse(fecha)
            };

            var idGenerado = await _agregarEventoLN.AgregarEvento(nuevoEvento);
            return Json(new { success = true, id = idGenerado });
        }


        [HttpPost]
        public async Task<JsonResult> EditarEvento(int id, string titulo, string fecha)
        {
            var evento = new EventoDto
            {
                Id = id,
                Titulo = titulo,
                FechaInicio = DateTime.Parse(fecha),
                FechaFin = DateTime.Parse(fecha)
            };
            await _editarEventoLN.EditarEvento(evento);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> EliminarEvento(int id)
        {
            try
            {
                var resultado = await _eliminarEventoLN.EliminarEvento(id);
                return Json(new { success = resultado > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
