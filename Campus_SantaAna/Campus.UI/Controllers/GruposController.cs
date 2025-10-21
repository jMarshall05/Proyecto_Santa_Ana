using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.AgregarGrupo;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.EditarGrupo;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.ListarTelefonos;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.AccesoDatos.ModelosAD;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.Grupos.AgregarGrupo;
using Campus.LogicaDeNegocio.Grupos.EditarGrupo;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Telefonos.ListarTelefonosLN;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Campus.UI.Filtros;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using QRCoder;

namespace Campus.UI.Controllers
{
    [Authorize(Roles = "Administradores")]
    public class GruposController : Controller
    {
        private IListarGruposLN _listarGrupos;
        private IObtenerUsuariosPorIdLN _obtenerUsuariosPorIdLN;
        private IAgregarGrupoLN _agregarGrupoLN;
        private IEditarGrupoLN _editarGrupoLN;
        private IBuscarEstudianteGrupoPorIdLN _buscarEstudianteGrupoPorIdLN;
        private IListarTelefonosLN _listarTelefonosLN;
        private static UsuariosGruposDto UsuariosGruposG;
        public GruposController()
        {
            _listarGrupos = new ListarGruposLN();
            _obtenerUsuariosPorIdLN = new ObtenerUsuariosPorIdLN();
            _agregarGrupoLN = new AgregarGrupoLN();
            _editarGrupoLN = new EditarGrupoLN();
            _buscarEstudianteGrupoPorIdLN = new BuscarEstudianteGrupoPorIdLN();
            _listarTelefonosLN = new ListarTelefonosLN();
        }
        // GET: Grupos
        public ActionResult ListarGrupos()
        {
            ViewBag.Id = User.Identity.GetUserId();
            var listaDeGrupos = _listarGrupos.ListarGrupos();
            return View(listaDeGrupos);
        }

        public ActionResult BuscarGruposPorUsuario()
        {
            string id = User.Identity.GetUserId();
            var Usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);

            //var grupo = _listarGrupos.BuscarGruposPorId((int)Usuario.Id_grupo);
            return View(/*grupo*/);
        }

        //GET: Grupos/Details/5
        public ActionResult DetallesDeGrupoParcial(int id)
        {
            var grupo = _listarGrupos.BuscarGruposPorId(id);
            var usuarios = new List<UsuariosDto>();
            var usuariosEnGrupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorGrupoId(id);
            var UsuariosGrupos = UsuariosGrupo(grupo, usuarios, usuariosEnGrupo);
            UsuariosGruposG = UsuariosGrupos;
            return PartialView("_DetallesDeGrupoParcial", UsuariosGrupos);
        }

        private UsuariosGruposDto UsuariosGrupo(GruposDto grupo, List<UsuariosDto> usuarios, List<EstudianteGrupoDto> usuariosEnGrupo)
        {
            foreach (var usuariosEG in usuariosEnGrupo)
            {
                var usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(usuariosEG.EstudianteId);
                usuarios.Add(usuario);
            }
            var UsuariosGruposDto = new UsuariosGruposDto
            {
                grupo = grupo,
                usuarios = usuarios
            };
            return UsuariosGruposDto;
        }

        // GET: Grupos/Create
        public ActionResult AgregarGrupoParcial()
        {
            return PartialView("_AgregarGrupoParcial");
        }

        // POST: Grupos/Create
        [HttpPost]
        public async Task<ActionResult> AgregarGrupoParcial(GruposDto grupo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Por favor, complete todos los campos requeridos.");
                    return PartialView("_AgregarGrupoParcial", grupo);
                }

                var id = User?.Identity?.GetUserId();
                if (string.IsNullOrWhiteSpace(id))
                {
                    return Content("Usuario no logueado o UserId es null en Azure.");
                }

                var Usuario = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
                if (Usuario == null)
                {
                    return Content($"No se encontró el usuario con Id: {id}");
                }

                grupo.creado_por = Usuario.Nombre + " " + Usuario.Apellido;
                int resultado = await _agregarGrupoLN.AgregarGrupo(grupo);

                if (resultado == 0)
                {
                    ModelState.AddModelError("", "No se pudo agregar el grupo. Por favor, intente nuevamente.");
                    return PartialView("_AgregarGrupoParcial", grupo);
                }

                return RedirectToAction("ListarGrupos");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                // Loggear en Azure App Service
                System.Diagnostics.Trace.TraceError("Error en AgregarGrupoParcial: " + ex);
                return Content("Error interno del servidor. Revisa logs de Azure para más detalles.");
            }
        }

        // GET: Grupos/Edit/5
        public ActionResult EditarGrupoParcial(int id)
        {
            var grupo = _listarGrupos.BuscarGruposPorId(id);
            return PartialView("_EditarGrupoParcial", grupo);
        }

        // POST: Grupos/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditarGrupoParcial(int id_grupo, GruposDto grupo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Por favor, complete todos los campos requeridos.");
                    return PartialView("_EditarGrupoParcial", grupo);
                }
                int resultado = await _editarGrupoLN.EditarGrupo(id_grupo, grupo);
                if (resultado == 1)
                {
                    return RedirectToAction("ListarGrupos");
                }

                ModelState.AddModelError("", "Por favor, complete todos los campos requeridos.");
                return PartialView("_EditarGrupoParcial", grupo);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GenerarReportePDF(int id)
        {
     
            var datos = UsuariosGruposG;

            using (var ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);
                document.SetMargins(40, 40, 40, 40);

                try
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/logo_SantaAna.jpg"));
                    Image logo = new Image(iText.IO.Image.ImageDataFactory.Create(imageBytes));
                    logo.ScaleToFit(100, 100);
                    logo.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(logo);
                }
                catch { }

                PdfFont bold = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
                Paragraph titulo = new Paragraph("Reporte del Grupo")
                    .SetFont(bold)
                    .SetFontSize(20)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(titulo);

                // Tabla con datos del grupo
                Table infoTable = new Table(2, false).SetWidth(UnitValue.CreatePercentValue(100));

                void AddRow(string label, string value)
                {
                    infoTable.AddCell(new Cell().Add(new Paragraph(label)).SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY));
                    infoTable.AddCell(new Cell().Add(new Paragraph(value ?? "")));
                }

                AddRow("Nombre del Grupo", datos.grupo.nombre_grupo);
                AddRow("Descripción del Grupo", datos.grupo.descripcion);
                AddRow("Creador", datos.grupo.creado_por);
                AddRow("Fecha de Creación", datos.grupo.FechaDeCreacion.ToString("dd/MM/yyyy HH:mm"));
                AddRow("Estado", datos.grupo.estado ? "Activo" : "Inactivo");

                document.Add(infoTable);

                document.Add(new Paragraph("\n"));

                //Tabla con miembros

                Paragraph subtitulo = new Paragraph("Miembros del Grupo")
                    .SetFont(bold)
                    .SetFontSize(14)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetMarginBottom(10);
                document.Add(subtitulo);

                Table cursosTable = new Table(new float[] { 2, 2, 4, 3, 3 });
                cursosTable.SetWidth(UnitValue.CreatePercentValue(100));

                // Encabezados
                string[] headers = { "Nombre", "Apellido", "Email", "Teléfonos", "Cédula" };
                foreach (var header in headers)
                {
                    cursosTable.AddHeaderCell(new Cell()
                        .Add(new Paragraph(header).SetFont(bold))
                        .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY));
                }

                var telefonos = _listarTelefonosLN.ListarTelefono();
                List<string> telefonosFormateados = new List<string>();

                foreach (var u in datos.usuarios)
                {
                    cursosTable.AddCell(new Paragraph(u.Nombre));
                    cursosTable.AddCell(new Paragraph(u.Apellido));
                    cursosTable.AddCell(new Paragraph(u.Email));

                    var telefonosUsuario= telefonos.Where(t => t.IdUsuario == u.IdUsuario);


                    foreach (var telefono in telefonosUsuario)
                    {
                        string telefonoFormateado = $"(+{telefono.Codigo}) {telefono.Telefono.ToString().Insert(4, "-")}: {telefono.Tipo} {(telefono.Estado ? "(Activo)" : "(Inactivo)")}";
                        telefonosFormateados.Add(telefonoFormateado);
                    }

                    cursosTable.AddCell(new Paragraph(string.Join("\n", telefonosFormateados)));
                    cursosTable.AddCell(new Paragraph(u.Cedula.ToString()));
                }

                document.Add(cursosTable);

                document.Close();
                return File(ms.ToArray(), "application/pdf", $"reporte_grupo_{id}.pdf");
            }
        }

        public ActionResult GenerarReporteQR(int id)
        {
            string urlPdf = Url.Action("GenerarReportePDF", "Grupos", new { id = id }, Request.Url.Scheme);
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
