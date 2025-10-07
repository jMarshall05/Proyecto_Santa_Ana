using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupoLN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.EditarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.EstudianteGrupo.ActualizarEstudianteGrupoLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.AgregarEstudianteGrupo;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.ListarEstudianteGrupo;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Usuarios.EditarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
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

            return View(listaDeUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult DetallesDeUsuarioParcial(string id)
        {
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id.ToString());
            usuario.IdUsuario = id;
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
            try
            {
                // ✅ Validación del ID
                if (string.IsNullOrEmpty(id))
                {
                    return Content("<div class='alert alert-danger'>Error: ID de usuario no proporcionado</div>");
                }

                // ✅ Obtener usuario
                var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
                if (usuario == null)
                {
                    return Content("<div class='alert alert-danger'>Error: Usuario no encontrado</div>");
                }

                // ✅ Verificar que el usuario existe en Identity
                var user = UserManager.FindById(id);
                if (user == null)
                {
                    return Content("<div class='alert alert-danger'>Error: Usuario no existe en el sistema de autenticación</div>");
                }

                var listaDeGrupos = _listarGrupos.ListarGrupos();
                ViewBag.ListaDeGrupos = new SelectList(listaDeGrupos, "id_grupo", "nombre_grupo");

                return PartialView("_EditarUsuarioParcial", usuario);
            }
            catch (Exception ex)
            {
                return Content($"<div class='alert alert-danger'>Error al cargar el formulario: {ex.Message}</div>");
            }
        }

        // POST: Usuarios/Edit/5
        // POST: Usuarios/Edit/5
        // POST: Usuarios/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditarUsuarioParcial(string id, UsuariosDto usuario, int? Idgrupo)
        {
            try
            {
                // ✅ VALIDACIÓN CRÍTICA: Verificar que el ID no sea nulo
                if (string.IsNullOrEmpty(id))
                {
                    TempData["ErrorMessage"] = "ID de usuario no proporcionado.";
                    return RedirectToAction("ListarUsuarios");
                }

                // ✅ VALIDACIÓN CRÍTICA: Verificar que el usuario exista
                var userExists = await UserManager.FindByIdAsync(id);
                if (userExists == null)
                {
                    TempData["ErrorMessage"] = "Usuario no encontrado en el sistema.";
                    return RedirectToAction("ListarUsuarios");
                }

                if (ModelState.IsValid)
                {
                    // ✅ Obtener roles actuales del usuario
                    var rolesActuales = await UserManager.GetRolesAsync(id);
                    var rolActual = rolesActuales.FirstOrDefault();

                    // ✅ Cambiar rol si es necesario
                    if (rolActual != usuario.Rol)
                    {
                        if (!string.IsNullOrEmpty(rolActual))
                        {
                            await UserManager.RemoveFromRoleAsync(id, rolActual);
                        }
                        await UserManager.AddToRoleAsync(id, usuario.Rol);
                    }

                    // ✅ Actualizar información del usuario
                    await _editarUsuarioLN.EditarUsuarioAdmin(id, usuario);

                    // ✅ Actualizar email
                    var result = await UserManager.SetEmailAsync(id, usuario.Email);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Error al actualizar el email: " + string.Join(", ", result.Errors));
                        return CargarVistaEdicion(usuario);
                    }

                    // ✅ Manejar grupo si es estudiante
                    if (Idgrupo != null && usuario.Rol == "Estudiantes")
                    {
                        var estudianteGrupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorEstudianteId(id);
                        var estudiante = new EstudianteGrupoDto { EstudianteId = id, GrupoId = Idgrupo };

                        if (estudianteGrupo == null)
                        {
                            await _agregarEstudianteGrupoLN.AgregarEstudianteGrupo(estudiante);
                        }
                        else
                        {
                            await _actualizarEstudianteGrupoLN.ActualizarEstudianteGrupo(estudiante);
                        }
                    }

                    TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                    ModelState.AddModelError("", "Por favor, corrija los errores en el formulario.");
                    return CargarVistaEdicion(usuario);
                }
            }
            catch (Exception ex)
            {
                // ✅ Manejo específico del error "UserId not found"
                if (ex.Message.Contains("UserId not found") || ex.Message.Contains("User ID not found"))
                {
                    TempData["ErrorMessage"] = "El usuario no existe o el ID es inválido.";
                    return RedirectToAction("ListarUsuarios");
                }

                ModelState.AddModelError("", "Error al editar el usuario: " + ex.Message);
                return CargarVistaEdicion(usuario);
            }
        }

        // ✅ Método helper para cargar la vista de edición
        private ActionResult CargarVistaEdicion(UsuariosDto usuario)
        {
            try
            {
                var listaDeGrupos = _listarGrupos.ListarGrupos();
                ViewBag.ListaDeGrupos = new SelectList(listaDeGrupos, "id_grupo", "nombre_grupo");
                return PartialView("_EditarUsuarioParcial", usuario);
            }
            catch (Exception ex)
            {
                return Content($"<div class='alert alert-danger'>Error al cargar la vista: {ex.Message}</div>");
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
                AddRow("Teléfono", datos.Telefono.ToString());
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
            string urlPdf = Url.Action("GenerarReportePDF", "Usuarios", new { id = id }, Request.Url.Scheme);
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
    }
}
