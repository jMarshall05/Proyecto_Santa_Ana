using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupoLN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.AgregarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.EditarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.ListarTelefonos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupo;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Telefonos.AgregarTelefonoLN;
using Campus.LogicaDeNegocio.Telefonos.EditarTelefonoLN;
using Campus.LogicaDeNegocio.Telefonos.ListarTelefonosLN;
using Campus.LogicaDeNegocio.Usuarios.EditarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QRCoder;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
namespace Campus.UI.Controllers
{
    //[Authorize(Roles = "Administradores")]
    public class UsuariosController : Controller
    {
        private readonly IListarUsuariosLN _listarUsuariosLN;
        private readonly IObtenerUsuariosPorIdLN _obtenerUsuariosPorIdLN;
        private readonly IEditarUsuarioLN _editarUsuarioLN;
        private ApplicationUserManager _userManager;
        private readonly IListarGruposLN _listarGrupos;
        private readonly IAgregarEstudianteGrupoLN _agregarEstudianteGrupoLN;
        private readonly IListarEstudianteGrupoLN _listarEstudianteGrupoLN;
        private readonly IBuscarEstudianteGrupoPorIdLN _buscarEstudianteGrupoPorIdLN;
        private readonly IActualizarEstudianteGrupoLN _actualizarEstudianteGrupoLN;
        private readonly IListarTelefonosLN _listarTelefonosLN;
        private readonly IEditarTelefonoLN _editarTelefonoLN;
        private readonly IAgregarTelefonoLN _agregarTelefonoLN;
        private static IEnumerable <UsuariosDto> usuarios;

        public UsuariosController()
        {
            _listarUsuariosLN = new ListarUsuariosLN();
            _obtenerUsuariosPorIdLN = new ObtenerUsuariosPorIdLN();
            _editarUsuarioLN = new EditarUsuariosLN();
            _listarGrupos = new ListarGruposLN();
            _agregarEstudianteGrupoLN = new AgregarEstudianteGrupoLN();
            _listarEstudianteGrupoLN = new ListarEstudianteGrupoLN();
            _buscarEstudianteGrupoPorIdLN = new BuscarEstudianteGrupoPorIdLN();
            _actualizarEstudianteGrupoLN = new ActualizarEstudianteGrupoLN();
            _listarTelefonosLN = new ListarTelefonosLN();
            _editarTelefonoLN = new EditarTelefonoLN();
            _agregarTelefonoLN = new AgregarTelefonoLN();

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public UsuariosController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        // GET: Usuarios
        public ActionResult ListarUsuarios()
        {
            var listaDeUsuarios = _listarUsuariosLN.ListarUsuarios();
            ViewBag.UsuariosInactivo = listaDeUsuarios.Where(u => u.Estado == true).Count();
            usuarios = listaDeUsuarios;

            return View(listaDeUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult DetallesDeUsuarioParcial(string id)
        {
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id.ToString());
            usuario.IdUsuario = id;
            var telefonos = _listarTelefonosLN.ListarTelefono().Where(t => t.IdUsuario == id);
            ViewBag.Telefonos = telefonos;
            var grupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorEstudianteId(id);
            if (grupo != null)
            {
                var NombreGrupo = _listarGrupos.BuscarGruposPorId((int)grupo.GrupoId);
                ViewBag.Grupo = NombreGrupo.nombre_grupo;
            }
            return PartialView("_DetallesDeUsuarioParcial", usuario);
        }


        // GET: Usuarios/Edit/5
        // GET: Usuarios/Edit/5
        public ActionResult EditarUsuarioParcial(string id)
        {
            var listaDeGrupos = _listarGrupos.ListarGrupos().Where(u => u.estado == true);
            ViewBag.ListaDeGrupos = new SelectList(listaDeGrupos, "id_grupo", "nombre_grupo");
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
            usuario.Telefonos = _listarTelefonosLN.ListarTelefono().Where(t => t.IdUsuario == id).ToList();
            return PartialView("_EditarUsuarioParcial", usuario);
        }

        [HttpPost]
        public async Task<ActionResult> EditarUsuarioParcial(string id, UsuariosDto usuario, int? Idgrupo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Roles
                    var rol = await UserManager.GetRolesAsync(id);
                    if (rol.FirstOrDefault() != usuario.Rol)
                    {
                        await UserManager.RemoveFromRoleAsync(id, rol.FirstOrDefault());
                        await UserManager.AddToRoleAsync(id, usuario.Rol);
                    }

                    await _editarUsuarioLN.EditarUsuarioAdmin(id, usuario);
                    await UserManager.SetEmailAsync(id, usuario.Email);


                    var telefonosValidos = usuario.Telefonos
                        .Where(t => !string.IsNullOrWhiteSpace(t.Telefono.ToString()) && !string.IsNullOrWhiteSpace(t.Tipo))
                        .ToList();

                    var telefonosExistentes = telefonosValidos.Where(t => t.Id > 0).ToList();
                    if (telefonosExistentes.Any())
                    {
                        await _editarTelefonoLN.EditarTelefono(telefonosExistentes);
                    }

                    var telefonosNuevos = telefonosValidos.Where(t => t.Id == 0).ToList();
                    if (telefonosNuevos.Any())
                    {
                        telefonosNuevos.ForEach(t => t.IdUsuario = id);
                        await _agregarTelefonoLN.AgregarTelefono(telefonosNuevos);
                    }

                    if (Idgrupo != null)
                    {
                        var estudianteGrupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorEstudianteId(id);
                        var estudiante = new EstudianteGrupoDto { EstudianteId = id, GrupoId = Idgrupo.Value };

                        if (estudianteGrupo == null)
                        {
                            await _agregarEstudianteGrupoLN.AgregarEstudianteGrupo(estudiante);
                        }
                        else
                        {
                            await _actualizarEstudianteGrupoLN.ActualizarEstudianteGrupo(estudiante);
                        }
                    }

                    return RedirectToAction("ListarUsuarios");
                }
               var errores= ObtenerErroresModelState();
                ModelState.AddModelError("", "Algo falló al editar.");
                return View("ListarUsuarios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar usuario: {ex.Message}");
                return View("ListarUsuarios",usuarios);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarUsuario(EditarUsuario usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = User.Identity.GetUserId();
                    var user = await UserManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var usuario = new UsuariosDto
                        {
                            Nombre = usuarioModel.Nombre,
                            Apellido = usuarioModel.Apellido,
                            Telefonos = usuarioModel.Telefonos
                        };
                        await _editarUsuarioLN.EditarUsuario(id, usuario);

                        // Verifica que Telefonos no sea null antes de usar
                        if (usuario.Telefonos != null)
                        {
                            var telefonosValidos = usuario.Telefonos
                                .Where(t => t.Telefono > 0 && !string.IsNullOrWhiteSpace(t.Tipo))
                                .ToList();

                            var telefonosExistentes = telefonosValidos.Where(t => t.Id > 0).ToList();
                            if (telefonosExistentes.Any())
                            {
                                await _editarTelefonoLN.EditarTelefono(telefonosExistentes);
                            }

                            var telefonosNuevos = telefonosValidos.Where(t => t.Id == 0).ToList();
                            if (telefonosNuevos.Any())
                            {
                                telefonosNuevos.ForEach(t => t.IdUsuario = id);
                                await _agregarTelefonoLN.AgregarTelefono(telefonosNuevos);
                            }
                        }

                        return Json(new { success = true, message = "Cambios guardados correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Usuario no encontrado" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Datos inválidos" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VerDocentesAdministrativos()
        {
            var usuarios = _listarUsuariosLN.ListarUsuarios()
                             .Where(u => u.Rol == "Profesores" || u.Rol == "Administradores")
                             .ToList();
            return View(usuarios);
        }

        public ActionResult GenerarReportePDF(string id)
        {
            var datos = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
            var telefonos = _listarTelefonosLN.ListarTelefono().Where(t => t.IdUsuario == id);
            List<string> telefonosFormateados = new List<string>();


            foreach (var telefono in telefonos)
            {
                string telefonoFormateado = $"(+{telefono.Codigo}) {telefono.Telefono.ToString().Insert(4, "-")}: {telefono.Tipo} {(telefono.Estado ? "Activo" : "Inactivo")}";
                telefonosFormateados.Add(telefonoFormateado);
            }


            using (var ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);
                document.SetMargins(40, 40, 40, 40);

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        byte[] imageBytes = client.GetByteArrayAsync("https://santaana.ed.cr/wp-content/uploads/LOGO-1.png").Result;
                        Image logo = new Image(iText.IO.Image.ImageDataFactory.Create(imageBytes));
                        logo.ScaleToFit(100, 100);
                        logo.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        document.Add(logo);
                    }
                }
                catch { }

                // Título
                PdfFont bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
                Paragraph titulo = new Paragraph("Reporte del Usuario")
                    .SetFont(bold)
                    .SetFontSize(20)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(titulo);

                Table table = new Table(2, false);
                table.SetWidth(UnitValue.CreatePercentValue(100));

                void AddRow(string label, string value)
                {
                    table.AddCell(new Cell().Add(new Paragraph(label)).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY));
                    table.AddCell(new Cell().Add(new Paragraph(value)));
                }

                AddRow("ID", id);
                AddRow("Nombre", datos.Nombre);
                AddRow("Apellido", datos.Apellido);
                AddRow("Email", datos.Email);
                AddRow("Teléfonos", string.Join("\n", telefonosFormateados));
                AddRow("Fecha de Nacimiento", datos.FechaDeNacimiento.ToShortDateString());
                AddRow("Cédula", datos.Cedula.ToString());
                AddRow("Rol", datos.Rol);
                AddRow("Estado", datos.Estado ? "Activo" : "Inactivo");

                document.Add(table);

                document.Close();
                return File(ms.ToArray(), "application/pdf", $"reporte_usuario_{id}.pdf");
            }
        }
        public ActionResult GenerarReporteQR(string id)
        {
            string urlPdf = Url.Action("GenerarReportePDF", "Usuarios", new { id }, Request.Url.Scheme);
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(urlPdf, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (var qrImage = qrCode.GetGraphic(20))
                {
                    using (var ms = new MemoryStream())
                    {
                        qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return File(ms.ToArray(), "image/png");
                    }
                }
            }
        }
        protected string ObtenerErroresModelState()
        {
            var errores = new StringBuilder();

            foreach (var key in ModelState.Keys)
            {
                var estado = ModelState[key];
                if (estado.Errors.Count > 0)
                {
                    errores.AppendLine($"Campo: {key}");
                    foreach (var error in estado.Errors)
                    {
                        var mensajeError = !string.IsNullOrEmpty(error.ErrorMessage)
                            ? error.ErrorMessage
                            : error.Exception?.Message ?? "Error desconocido";
                        errores.AppendLine($"  - {mensajeError}");
                    }
                }
            }

            return errores.ToString();
        }
    }
}
