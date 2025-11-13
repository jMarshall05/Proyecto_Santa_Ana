using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.AgregarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EditarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.EliminarEventoLN;
using Campus.Abstracciones.LogicaDeNegocio.Eventos.ListarEventosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Eventos.AgregarEventoLN;
using Campus.LogicaDeNegocio.Eventos.EditarEventoLN;
using Campus.LogicaDeNegocio.Eventos.EliminarEventoLN;
using Campus.LogicaDeNegocio.Eventos.ListarEventosLN;
using Microsoft.AspNet.Identity;

namespace Campus.UI.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {
        private readonly IAgregarEventoLN _agregarEventoLN;
        private readonly IListarEventosLN _listarEventosLN;
        private readonly IEditarEventoLN _editarEventoLN;
        private readonly IEliminarEventoLN _eliminarEventoLN;
        private readonly IBitacoraLN _bitacora;

        public EventosController()
        {
            _agregarEventoLN = new AgregarEventoLN();
            _listarEventosLN = new ListarEventosLN();
            _editarEventoLN = new EditarEventoLN();
            _eliminarEventoLN = new EliminarEventoLN();
            _bitacora = new BitacoraLN();
        }

        public ActionResult Calendario() => View();

        [HttpGet]

        public async Task<JsonResult> ObtenerEventos()
        {
            try
            {
                var idUsuario = User.Identity.GetUserId();
                var eventos = await _listarEventosLN.ListarEventos(idUsuario);


                var eventosUsuario = eventos.Where(e => e.Estado == true).Select(e => new
                {
                    id = e.Id,
                    title = e.Titulo,
                    start = e.FechaInicio.ToString("s"),
                    end = e.FechaFin.ToString("s")
                });

                return Json(eventosUsuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public async Task<JsonResult> AgregarEvento(string titulo, string fecha, string idUsuario)
        {
            try
            {
                var nuevoEvento = new EventoDto
                {
                    Titulo = titulo,
                    FechaInicio = DateTime.Parse(fecha),
                    FechaFin = DateTime.Parse(fecha),
                    IdUsuario = idUsuario
                };

                var idGenerado = await _agregarEventoLN.AgregarEvento(nuevoEvento);

                // Bitácora: inserción de nuevo evento
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "INSERT",
                    Tabla = "Eventos",
                    Descripcion = $"Creación de evento '{titulo}' - Fecha: {DateTime.Parse(fecha):dd/MM/yyyy}"
                };
                _bitacora.RegistrarEvento(bitacora);

                return Json(new { success = true, id = idGenerado });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditarEvento(int id, string titulo, string fecha, string idUsuario)
        {
            try
            {
                var evento = new EventoDto
                {
                    Id = id,
                    Titulo = titulo,
                    FechaInicio = DateTime.Parse(fecha),
                    FechaFin = DateTime.Parse(fecha),
                    IdUsuario = idUsuario
                };

                await _editarEventoLN.EditarEvento(evento);

                // Bitácora: actualización de evento
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "UPDATE",
                    Tabla = "Eventos",
                    Descripcion = $"Actualización de evento ID: {id} - '{titulo}' - Nueva fecha: {DateTime.Parse(fecha):dd/MM/yyyy}"
                };
                _bitacora.RegistrarEvento(bitacora);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EliminarEvento(int id)
        {
            try
            {
                var resultado = await _eliminarEventoLN.EliminarEvento(id);

                if (resultado > 0)
                {
                    // Bitácora: eliminación lógica de evento
                    var bitacora = new BitacoraDto
                    {
                        Fecha = DateTime.Now,
                        Usuario = User.Identity.GetUserId(),
                        Accion = "DELETE",
                        Tabla = "Eventos",
                        Descripcion = $"Eliminación lógica de evento ID: {id} - Estado cambiado a inactivo"
                    };
                    _bitacora.RegistrarEvento(bitacora);
                }

                return Json(new { success = resultado > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

