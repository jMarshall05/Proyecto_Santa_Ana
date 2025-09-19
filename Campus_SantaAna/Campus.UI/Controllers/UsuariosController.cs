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
        public ActionResult EditarUsuarioParcial(string id)
        {
            var listaDeGrupos = _listarGrupos.ListarGrupos();
            ViewBag.ListaDeGrupos = new SelectList(listaDeGrupos, "id_grupo", "nombre_grupo");
            var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
            return PartialView("_EditarUsuarioParcial", usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditarUsuarioParcial(string id, UsuariosDto usuario, int? Idgrupo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rol = await UserManager.GetRolesAsync(id);
                    if (rol.FirstOrDefault() != usuario.Rol)
                    {
                        await UserManager.RemoveFromRoleAsync(id, rol.FirstOrDefault());
                        await UserManager.AddToRoleAsync(id, usuario.Rol);
                    }
                    await _editarUsuarioLN.EditarUsuarioAdmin(id, usuario);
                    await UserManager.SetEmailAsync(id, usuario.Email);
                    if (Idgrupo != null)
                    {
                        var estudianteGrupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorEstudianteId(id);
                        var estudiante = new EstudianteGrupoDto { EstudianteId = id, GrupoId = Idgrupo };
                        if (estudianteGrupo == null)
                        {
                            await _agregarEstudianteGrupoLN.AgregarEstudianteGrupo(estudiante);
                            return RedirectToAction("ListarUsuarios");
                        }
                        await _actualizarEstudianteGrupoLN.ActualizarEstudianteGrupo(estudiante);
                    }
                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                    ModelState.AddModelError("", "Algo fallo al editar.");
                    return View("ListarUsuarios");
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditarUsuario(string id, UsuariosDto usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var result = await UserManager.SetEmailAsync(id, usuario.Email);
                        if (result.Succeeded)
                        {
                            await _editarUsuarioLN.EditarUsuario(id, usuario);
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Por favor, corrija los errores en el formulario.");
                    return PartialView("_EditarUsuarioParcial", usuario);
                }



                return RedirectToAction("ListarUsuarios");
            }
            catch
            {
                return View();
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
                    string urlLogo = "https://santaana.ed.cr/wp-content/uploads/LOGO-1.png";

                    using (HttpClient client = new HttpClient())
                    {
                        byte[] imageBytes = client.GetByteArrayAsync(urlLogo).Result;
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

                Table table = new Table(2, true); 
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

                // Separador final
                document.Add(new Paragraph("\n"));
                LineSeparator separator = new LineSeparator(new SolidLine());
                document.Add(separator);

                document.Close();
                return File(ms.ToArray(), "application/pdf", "reporte_usuario.pdf");
            }
        }
        public ActionResult GenerarReporteQR(string id)
        {
          string urlPdf= Url.Action("GenerarReportePDF", "Usuarios", new { id = id }, Request.Url.Scheme);
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
