using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
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
using Campus.LogicaDeNegocio.Bitacora;
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
using Image = iText.Layout.Element.Image;
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
        private readonly IBitacoraLN _bitacora;
        private static IEnumerable<UsuariosDto> usuarios;

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
            _bitacora = new BitacoraLN();

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
                    var usuarioActual = User.Identity.GetUserId(); // NUEVO: Obtener usuario actual para bitácora

                    // Roles
                    var rol = await UserManager.GetRolesAsync(id);
                    if (rol.FirstOrDefault() != usuario.Rol)
                    {
                        await UserManager.RemoveFromRoleAsync(id, rol.FirstOrDefault());
                        await UserManager.AddToRoleAsync(id, usuario.Rol);

                        var bitacoraRol = new BitacoraDto
                        {
                            Fecha = DateTime.Now,
                            Usuario = usuarioActual,
                            Accion = "UPDATE",
                            Tabla = "AspNetUserRoles",
                            Descripcion = $"Cambio de rol del usuario ID: {id} - De '{rol.FirstOrDefault()}' a '{usuario.Rol}'"
                        };
                        _bitacora.RegistrarEvento(bitacoraRol);
                    }

                    await _editarUsuarioLN.EditarUsuarioAdmin(id, usuario);
                    await UserManager.SetEmailAsync(id, usuario.Email);

                    var bitacoraUsuario = new BitacoraDto
                    {
                        Fecha = DateTime.Now,
                        Usuario = usuarioActual,
                        Accion = "UPDATE",
                        Tabla = "AspNetUsers",
                        Descripcion = $"Actualización de datos del usuario ID: {id} - Nombre: {usuario.Nombre} {usuario.Apellido}, Email: {usuario.Email}"
                    };
                    _bitacora.RegistrarEvento(bitacoraUsuario);

                    var telefonosValidos = usuario.Telefonos
                        .Where(t => !string.IsNullOrWhiteSpace(t.Telefono.ToString()) && !string.IsNullOrWhiteSpace(t.Tipo))
                        .ToList();

                    var telefonosExistentes = telefonosValidos.Where(t => t.Id > 0).ToList();
                    if (telefonosExistentes.Any())
                    {
                        await _editarTelefonoLN.EditarTelefono(telefonosExistentes);

                        var bitacoraTelefonos = new BitacoraDto
                        {
                            Fecha = DateTime.Now,
                            Usuario = usuarioActual,
                            Accion = "UPDATE",
                            Tabla = "Telefonos",
                            Descripcion = $"Actualización de {telefonosExistentes.Count} teléfono(s) del usuario ID: {id}"
                        };
                        _bitacora.RegistrarEvento(bitacoraTelefonos);
                    }

                    var telefonosNuevos = telefonosValidos.Where(t => t.Id == 0).ToList();
                    if (telefonosNuevos.Any())
                    {
                        telefonosNuevos.ForEach(t => t.IdUsuario = id);
                        await _agregarTelefonoLN.AgregarTelefono(telefonosNuevos);

                        var bitacoraNuevosTelefonos = new BitacoraDto
                        {
                            Fecha = DateTime.Now,
                            Usuario = usuarioActual,
                            Accion = "INSERT",
                            Tabla = "Telefonos",
                            Descripcion = $"Registro de {telefonosNuevos.Count} nuevo(s) teléfono(s) para el usuario ID: {id}"
                        };
                        _bitacora.RegistrarEvento(bitacoraNuevosTelefonos);
                    }

                    if (Idgrupo != null)
                    {
                        var estudianteGrupo = _buscarEstudianteGrupoPorIdLN.BuscarEstudianteGrupoPorEstudianteId(id);
                        var estudiante = new EstudianteGrupoDto { EstudianteId = id, GrupoId = Idgrupo.Value };

                        if (estudianteGrupo == null)
                        {
                            await _agregarEstudianteGrupoLN.AgregarEstudianteGrupo(estudiante);

                            var bitacoraGrupo = new BitacoraDto
                            {
                                Fecha = DateTime.Now,
                                Usuario = usuarioActual,
                                Accion = "INSERT",
                                Tabla = "EstudianteGrupo",
                                Descripcion = $"Asignación del estudiante ID: {id} al grupo ID: {Idgrupo.Value}"
                            };
                            _bitacora.RegistrarEvento(bitacoraGrupo);
                        }
                        else
                        {
                            await _actualizarEstudianteGrupoLN.ActualizarEstudianteGrupo(estudiante);

                            var bitacoraActualizarGrupo = new BitacoraDto
                            {
                                Fecha = DateTime.Now,
                                Usuario = usuarioActual,
                                Accion = "UPDATE",
                                Tabla = "EstudianteGrupo",
                                Descripcion = $"Cambio de grupo del estudiante ID: {id} - Nuevo grupo ID: {Idgrupo.Value}"
                            };
                            _bitacora.RegistrarEvento(bitacoraActualizarGrupo);
                        }
                    }

                    return RedirectToAction("ListarUsuarios");
                }
                var errores = ObtenerErroresModelState();
                ModelState.AddModelError("", "Algo falló al editar.");
                return View("ListarUsuarios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar usuario: {ex.Message}");
                return View("ListarUsuarios", usuarios);
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
                        var result = await _editarUsuarioLN.EditarUsuario(id, usuario);
                        var bitacoraUsuario = new BitacoraDto
                        {
                            Fecha = DateTime.Now,
                            Usuario = id,
                            Accion = "UPDATE",
                            Tabla = "AspNetUsers",
                            Descripcion = $"Usuario actualizó su propio perfil - Nombre: {usuario.Nombre} {usuario.Apellido}"
                        };
                        _bitacora.RegistrarEvento(bitacoraUsuario);

                        if (usuario.Telefonos != null)
                        {
                            var telefonosValidos = usuario.Telefonos
                                .Where(t => t.Telefono > 0 && !string.IsNullOrWhiteSpace(t.Tipo))
                                .ToList();

                            var telefonosExistentes = telefonosValidos.Where(t => t.Id > 0).ToList();
                            if (telefonosExistentes.Any())
                            {
                                await _editarTelefonoLN.EditarTelefono(telefonosExistentes);

                                var bitacoraTelefonos = new BitacoraDto
                                {
                                    Fecha = DateTime.Now,
                                    Usuario = id,
                                    Accion = "UPDATE",
                                    Tabla = "Telefonos",
                                    Descripcion = $"Usuario actualizó {telefonosExistentes.Count} de sus teléfonos"
                                };
                                _bitacora.RegistrarEvento(bitacoraTelefonos);
                            }

                            var telefonosNuevos = telefonosValidos.Where(t => t.Id == 0).ToList();
                            if (telefonosNuevos.Any())
                            {
                                telefonosNuevos.ForEach(t => t.IdUsuario = id);
                                await _agregarTelefonoLN.AgregarTelefono(telefonosNuevos);

                                var bitacoraNuevosTelefonos = new BitacoraDto
                                {
                                    Fecha = DateTime.Now,
                                    Usuario = id,
                                    Accion = "INSERT",
                                    Tabla = "Telefonos",
                                    Descripcion = $"Usuario agregó {telefonosNuevos.Count} nuevo(s) teléfono(s) a su perfil"
                                };
                                _bitacora.RegistrarEvento(bitacoraNuevosTelefonos);
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

        public ActionResult GenerarReportePDFGeneral()
        {
            var datos = usuarios;
            using (var ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate());
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
                PdfFont regular = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);

                Paragraph titulo = new Paragraph("Reporte del Grupo")
                    .SetFont(bold)
                    .SetFontSize(20)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(titulo);

                Paragraph subtituloGrafico = new Paragraph("Estadísticas de Usuarios")
                    .SetFont(bold)
                    .SetFontSize(14)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetMarginBottom(10);
                document.Add(subtituloGrafico);

                byte[] chartBytes = GenerarGraficoPastel(datos.ToList());
                Image chartImage = new Image(iText.IO.Image.ImageDataFactory.Create(chartBytes));
                chartImage.ScaleToFit(400, 300);
                chartImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                chartImage.SetMarginBottom(25);
                document.Add(chartImage);

                Paragraph subtitulo = new Paragraph("Usuarios")
                    .SetFont(bold)
                    .SetFontSize(14)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetMarginBottom(10);
                document.Add(subtitulo);

                Table cursosTable = new Table(new float[] { 1.5f, 1.5f, 2f, 2f, 2.5f, 2.5f, 2f, 2f, 1.5f, 1.5f });
                cursosTable.SetWidth(UnitValue.CreatePercentValue(100));
                cursosTable.SetFontSize(9);

                string[] headers = { "Id Usuario", "Identificacion", "Nombre", "Apelldo", "Correo", "Telefonos", "Fecha de Nacimiento", "Fecha de Registro", "Rol", "Estado" };

                foreach (var header in headers)
                {
                    cursosTable.AddHeaderCell(new Cell()
                        .Add(new Paragraph(header).SetFont(bold).SetFontSize(9))
                        .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE));
                }

                var telefonos = _listarTelefonosLN.ListarTelefono();

                foreach (var u in datos)
                {
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.IdUsuario).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Identificacion.ToString()).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Nombre).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Apellido).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Email).SetFont(regular)));

                    var telefonosUsuario = telefonos.Where(t => t.IdUsuario == u.IdUsuario).ToList();
                    List<string> telefonosFormateados = new List<string>();

                    foreach (var telefono in telefonosUsuario)
                    {
                        string telefonoFormateado = $"(+{telefono.Codigo}) {telefono.Telefono.ToString().Insert(4, "-")}: {telefono.Tipo} {(telefono.Estado ? "(Activo)" : "(Inactivo)")}";
                        telefonosFormateados.Add(telefonoFormateado);
                    }

                    cursosTable.AddCell(new Cell().Add(new Paragraph(telefonosFormateados.Count > 0 ? string.Join("\n", telefonosFormateados) : "Sin teléfonos").SetFont(regular).SetFontSize(8)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.FechaDeNacimiento.ToString("dd/MM/yyyy")).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.FechaDeRegistro.ToString("dd/MM/yyyy")).SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Rol ?? "N/A").SetFont(regular)));
                    cursosTable.AddCell(new Cell().Add(new Paragraph(u.Estado ? "Activo" : "Inactivo").SetFont(regular))
                        .SetBackgroundColor(u.Estado ? iText.Kernel.Colors.ColorConstants.LIGHT_GRAY : iText.Kernel.Colors.ColorConstants.RED)
                        .SetFontColor(u.Estado ? iText.Kernel.Colors.ColorConstants.BLACK : iText.Kernel.Colors.ColorConstants.WHITE));
                }

                document.Add(cursosTable);
                document.Close();

                return File(ms.ToArray(), "application/pdf", $"reporteUsuarios({DateTime.UtcNow.Month}-{DateTime.UtcNow.Year})_{Guid.NewGuid()}.pdf");
            }
        }

        // Método para generar el gráfico de pastel
        private byte[] GenerarGraficoPastel(List<UsuariosDto> usuarios)
        {
            int activos = usuarios.Count(u => u.Estado);
            int inactivos = usuarios.Count(u => !u.Estado);

            var usuariosPorRol = usuarios.GroupBy(u => u.Rol ?? "Sin Rol")
                                          .Select(g => new { Rol = g.Key, Cantidad = g.Count() })
                                          .ToList();

            int width = 600;
            int height = 400;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            graphics.Clear(Color.White);

            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            graphics.DrawString("Usuarios por Rol", titleFont, Brushes.Black, new PointF(width / 2 - 80, 20));

            Rectangle pieRect = new Rectangle(50, 80, 300, 300);

            Color[] colors = { Color.FromArgb(52, 152, 219), Color.FromArgb(46, 204, 113),
                           Color.FromArgb(241, 196, 15), Color.FromArgb(231, 76, 60),
                           Color.FromArgb(155, 89, 182), Color.FromArgb(52, 73, 94) };

            float totalUsuarios = usuarios.Count;
            float startAngle = 0;
            int legendY = 100;

            for (int i = 0; i < usuariosPorRol.Count; i++)
            {
                float sweepAngle = (usuariosPorRol[i].Cantidad / totalUsuarios) * 360;

                using (SolidBrush brush = new SolidBrush(colors[i % colors.Length]))
                {
                    graphics.FillPie(brush, pieRect, startAngle, sweepAngle);
                }

                graphics.DrawPie(Pens.White, pieRect, startAngle, sweepAngle);

                int legendX = 380;
                Rectangle legendRect = new Rectangle(legendX, legendY, 20, 20);
                using (SolidBrush brush = new SolidBrush(colors[i % colors.Length]))
                {
                    graphics.FillRectangle(brush, legendRect);
                }
                graphics.DrawRectangle(Pens.Black, legendRect);

                Font legendFont = new Font("Arial", 10);
                string legendText = $"{usuariosPorRol[i].Rol}: {usuariosPorRol[i].Cantidad} ({(usuariosPorRol[i].Cantidad / totalUsuarios * 100):F1}%)";
                graphics.DrawString(legendText, legendFont, Brushes.Black, legendX + 30, legendY);

                legendY += 30;
                startAngle += sweepAngle;
            }

            Font statsFont = new Font("Arial", 12, FontStyle.Bold);
            graphics.DrawString($"Total: {usuarios.Count} usuarios", statsFont, Brushes.Black, 380, legendY + 20);
            graphics.DrawString($"Activos: {activos}", statsFont, Brushes.Green, 380, legendY + 45);
            graphics.DrawString($"Inactivos: {inactivos}", statsFont, Brushes.Red, 380, legendY + 70);

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                graphics.Dispose();
                bitmap.Dispose();
                return stream.ToArray();
            }
        }

        public ActionResult GenerarReportePDF(string id)
        {
            var datos = _obtenerUsuariosPorIdLN.ObtenerUsuarioPorId(id);
            var telefonos = _listarTelefonosLN.ListarTelefono().Where(t => t.IdUsuario == id);
            List<string> telefonosFormateados = new List<string>();

            foreach (var telefono in telefonos)
            {
                string telefonoFormateado = $"(+{telefono.Codigo}) {telefono.Telefono.ToString().Insert(4, "-")}: {telefono.Tipo} {(telefono.Estado ? "(Activo)" : "(Inactivo)")}";
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
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/logo_SantaAna.jpg"));
                    Image logo = new Image(iText.IO.Image.ImageDataFactory.Create(imageBytes));
                    logo.ScaleToFit(100, 100);
                    logo.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(logo);
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
                AddRow("Tipo de Identificacion",datos.TipoIdentificacion);
                AddRow("Identificacion", datos.Identificacion.ToString());
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