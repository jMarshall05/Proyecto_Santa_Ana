using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.AgregarCursoLN;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Cursos.AgregarCursoLN;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.Cursos.ModificarEstadoCursoLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNet.Identity;
using QRCoder;

namespace Campus.UI.Controllers
{
    [Authorize(Roles = "Administradores")]
    public class CursosController : Controller
    {
        private readonly IListarCursoLN _listarCursoLN;
        private readonly IAgregarCursoLN _agregarCursoLN;
        private readonly IListarGruposLN _listarGruposLN;
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IListarUsuariosLN _listarUsuariosLN;
        private readonly ModificarEstadoCursoLN _modificarEstadoCursoLN;
        private readonly IObtenerUsuariosPorIdLN _obtenerUsuariosPorId;
        private readonly IBitacoraLN _bitacora;

        private static IEnumerable<CursoDto> cursos;
        public CursosController()
        {
            _listarCursoLN = new ListarCursosLN();
            _agregarCursoLN = new AgregarCursoLN();
            _listarGruposLN = new ListarGruposLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarUsuariosLN = new ListarUsuariosLN();
            _modificarEstadoCursoLN = new ModificarEstadoCursoLN();
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdLN();
            _bitacora = new BitacoraLN();
        }
        // GET: Cursos
        public ActionResult ListarCursos()
        {
            List<CursoDto> listaDeCursos = ObtenerCursos();
            cursos = listaDeCursos;
            return View(listaDeCursos);
        }

        private List<CursoDto> ObtenerCursos()
        {
            var listaDeCursos = _listarCursoLN.ListarCursos();
            foreach (var item in listaDeCursos)
            {
                var usuario = _obtenerUsuariosPorId.ObtenerUsuarioPorId(item.ProfesorId);
                item.NombreMateria = _listarMateriasLN.ObtenerMateriaPorId(item.MateriaId).Nombre;
                item.NombreGrupo = _listarGruposLN.BuscarGruposPorId(item.GrupoId).nombre_grupo;
                item.NombreProfesor = usuario.Nombre + " " + usuario.Apellido;
            }

            return listaDeCursos;
        }

        // GET: Cursos/Details/5
        public ActionResult DetallesDeCursoParcial(int id)
        {
            return View();
        }

        // GET: Cursos/Create
        public ActionResult AgregarCursoParcial()
        {
            CargarViewBags();
            return PartialView("_AgregarCursoParcial");
        }

        private void CargarViewBags()
        {
            var Profesores = _listarUsuariosLN.ListarUsuarios().Where(Usuario => Usuario.Rol == "Profesores").Select(Usuario => new
            {
                Usuario.IdUsuario,
                NombreCompleto = Usuario.Nombre + " " + Usuario.Apellido
            });
            var Materias = _listarMateriasLN.ListarMaterias();
            var Grupos = _listarGruposLN.ListarGrupos();
            ViewBag.Profesores = new SelectList(Profesores, "IdUsuario", "NombreCompleto");
            ViewBag.Materias = new SelectList(Materias, "Id_Materia", "Nombre");
            ViewBag.Grupos = new SelectList(Grupos, "id_grupo", "nombre_grupo");
        }

        // POST: Cursos/Create
        [HttpPost]
        public async Task<ActionResult> AgregarCursoParcial(CursoDto Curso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _agregarCursoLN.AgregarCurso(Curso);

                    // Bitácora: inserción de nuevo curso
                    var materia = _listarMateriasLN.ObtenerMateriaPorId(Curso.MateriaId);
                    var grupo = _listarGruposLN.BuscarGruposPorId(Curso.GrupoId);
                    var profesor = _obtenerUsuariosPorId.ObtenerUsuarioPorId(Curso.ProfesorId);
                    var bitacora = new BitacoraDto
                    {
                        Fecha = DateTime.Now,
                        Usuario = User.Identity.GetUserId(),
                        Accion = "INSERT",
                        Tabla = "Cursos",
                        Descripcion = $"Creación de curso - Materia: {materia.Nombre}, Grupo: {grupo.nombre_grupo}, Profesor: {profesor.Nombre} {profesor.Apellido}"
                    };
                    _bitacora.RegistrarEvento(bitacora);

                    return RedirectToAction("ListarCursos");
                }
                catch
                {
                    CargarViewBags();
                    return PartialView("_AgregarCursoParcial", Curso);
                }
            }
            CargarViewBags();
            return PartialView("_AgregarCursoParcial", Curso);
        }

        public ActionResult GenerarReportePDF()
        {
            var datos = cursos;

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
                Paragraph titulo = new Paragraph("Reporte de Cursos")
                    .SetFont(bold)
                    .SetFontSize(20)
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetMarginBottom(20);
                document.Add(titulo);

                Table cursosTable = new Table(new float[] { 2, 3, 3, 4, 3 });
                cursosTable.SetWidth(UnitValue.CreatePercentValue(100));

                // Encabezados
                string[] headers = { "Id", "Materia", "Grupo", "Profesor", "Estado" };
                foreach (var header in headers)
                {
                    cursosTable.AddHeaderCell(new Cell()
                        .Add(new Paragraph(header).SetFont(bold))
                        .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY));
                }

                // Filas de usuarios
                foreach (var dato in datos)
                {
                    cursosTable.AddCell(new Paragraph(dato.IdCurso.ToString()));
                    cursosTable.AddCell(new Paragraph(dato.NombreMateria));
                    cursosTable.AddCell(new Paragraph(dato.NombreGrupo));
                    cursosTable.AddCell(new Paragraph(dato.NombreProfesor));
                    cursosTable.AddCell(new Paragraph(dato.Estado ? "Activo" : "Inactivo")
                        .SetBackgroundColor(dato.Estado ? iText.Kernel.Colors.ColorConstants.LIGHT_GRAY : iText.Kernel.Colors.ColorConstants.RED)
                        .SetFontColor(dato.Estado ? iText.Kernel.Colors.ColorConstants.BLACK : iText.Kernel.Colors.ColorConstants.WHITE));

                }

                document.Add(cursosTable);

                document.Close();
                return File(ms.ToArray(), "application/pdf", $"reporte_Cursos({DateTime.Today.Date.ToShortDateString()}).pdf");
            }
        }


        public ActionResult GenerarReporteQR()
        {
            string urlPdf = Url.Action("GenerarReportePDF", "Cursos", null, Request.Url.Scheme);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModificarEstadoCurso(int id, bool estado)
        {
            try
            {
                var cursoInfo = _listarCursoLN.ListarCursos().FirstOrDefault(c => c.IdCurso == id);

                await _modificarEstadoCursoLN.ModificarEstadoCurso(id, estado);

                var materia = _listarMateriasLN.ObtenerMateriaPorId(cursoInfo.MateriaId);
                var grupo = _listarGruposLN.BuscarGruposPorId(cursoInfo.GrupoId);
                var accionDescripcion = estado ? "Reactivación" : "Eliminación lógica";
                var estadoTexto = estado ? "activo" : "inactivo";

                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = estado ? "UPDATE" : "DELETE",
                    Tabla = "Cursos",
                    Descripcion = $"{accionDescripcion} de curso ID: {id} - Materia: {materia.Nombre}, Grupo: {grupo.nombre_grupo} - Estado cambiado a {estadoTexto}"
                };
                _bitacora.RegistrarEvento(bitacora);

                TempData["SuccessMessage"] = "Curso eliminado correctamente";
                return RedirectToAction("ListarCursos");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el curso: " + ex.Message;
                return RedirectToAction("ListarCursos");
            }
        }
    }
}

