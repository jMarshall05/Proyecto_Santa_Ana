using Campus.Abstracciones.AccesoDatos.Eventos;
using Campus.Abstracciones.LogicaDeNegocio.Eventos;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campus.UI.Controllers
{
    public class EventosController : Controller
    {
        private readonly IEventoLN _eventoLN;

        public EventosController()
        {
            _eventoLN = new EventoLN();
        }

        public ActionResult Calendario()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerEventos()
        {
            var eventos = await _eventoLN.ListarEventos();
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

            await _eventoLN.AgregarEvento(nuevoEvento);
            return Json(new { success = true });
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
            await _eventoLN.EditarEvento(evento);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> EliminarEvento(int id)
        {
            await _eventoLN.EliminarEvento(id);
            return Json(new { success = true });
        }

    }
}
