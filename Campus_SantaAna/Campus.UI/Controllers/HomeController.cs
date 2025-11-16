using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Documentos;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.Documentos;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QRCoder;

namespace Campus.UI.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly IListarCursoLN _listarCursos;
        private readonly IObtenerUsuariosPorIdLN _obtenerUsuariosPorId;
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IListarGruposLN _listarGruposLN;
        private readonly IListarUsuariosLN _listarUsuariosLN;
        private ApplicationUserManager _userManager;
        private readonly IBuscarEstudianteGrupoPorIdLN _estudianteGrupoLN;
        private readonly IBitacoraLN _bitacora;
        private readonly IAgregarDocumentoLN _agregarDocumentos;
        private readonly IListarDocumentosLN _listarDocumentosLN;

        public HomeController()
        {
            _listarCursos = new ListarCursosLN();
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarGruposLN = new ListarGruposLN();
            _estudianteGrupoLN = new BuscarEstudianteGrupoPorIdLN();
            _listarUsuariosLN = new ListarUsuariosLN();
            _bitacora = new BitacoraLN();
            _agregarDocumentos = new AgregarDocumentoLN();
            _listarDocumentosLN = new ListarDocumentosLN();
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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

        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            if (id == null)
                return RedirectToAction("login", "Account");

            if (User.IsInRole("Profesores"))
            {
                return VistaProfesor(id);
            }
            else if (User.IsInRole("Estudiantes"))
            {
                return VistaEstudiante(id);
            }
            else if (User.IsInRole("Administradores"))
            {
                var Usuarios = _listarUsuariosLN.ListarUsuarios();
                ViewBag.Estudiantes = Usuarios.Where(u => u.Rol == "Estudiantes").Count();
                ViewBag.Profesores = Usuarios.Where(u => u.Rol == "Profesores").Count();

                return View(new List<CursoDto>());
            }

            return View(new List<CursoDto>());
        }

        private ActionResult VistaEstudiante(string id)
        {
            var grupo = _estudianteGrupoLN.BuscarEstudianteGrupoPorEstudianteId(id);

            if (grupo == null)
                return View(new List<CursoDto>());

            var listaDeCursos = _listarCursos.ListarCursos()
                .Where(u => u.GrupoId == grupo.GrupoId)
                .ToList();

            FiltarCursos(listaDeCursos);
            return View(listaDeCursos);
        }

        private ActionResult VistaProfesor(string id)
        {
            var listaDeCursos = _listarCursos.ListarCursos()
                                .Where(u => u.ProfesorId == id && u.Estado == true)
                                .ToList();

            FiltarCursos(listaDeCursos);
            return View(listaDeCursos.Where(c => c.Estado == true));
        }

        private void FiltarCursos(List<CursoDto> listaDeCursos)
        {
            foreach (var item in listaDeCursos)
            {
                var usuario = _obtenerUsuariosPorId.ObtenerUsuarioPorId(item.ProfesorId);
                var materia = _listarMateriasLN.ObtenerMateriaPorId(item.MateriaId);
                var grupo = _listarGruposLN.BuscarGruposPorId(item.GrupoId);


                item.NombreMateria = materia?.Nombre ?? "Sin materia";
                item.NombreGrupo = grupo?.nombre_grupo ?? "Sin grupo";
                item.NombreProfesor = usuario != null ? $"{usuario.Nombre} {usuario.Apellido}" : "Sin profesor";
                if (usuario.Estado == false || materia.Estado == false || grupo.estado == false)
                {
                    item.Estado = false;
                }
            }
        }

        public ActionResult GenerarQR(string url)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (var qrImage = qrCode.GetGraphic(20))
                {
                    using (var ms = new MemoryStream())
                    {
                        qrImage.Save(ms, ImageFormat.Png);
                        return File(ms.ToArray(), "image/png");
                    }
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Documentos()
        {
            var documentos = _listarDocumentosLN.ListarDocumentos().ToList();
            ViewBag.DocumentosAdicionales = documentos;

            return View();
        }
        [HttpPost]
        public ActionResult AgregarDocumento(HttpPostedFileBase Archivo, string Titulo, string Descripcion, string Categoria)
        {
            if (Archivo != null && Archivo.ContentLength > 0)
            {
                ComprobarTipodeArchivo(Archivo, out string[] extensionesPermitidas, out string extensionArchivo);

                if (!extensionesPermitidas.Contains(extensionArchivo))
                {
                    throw new System.Exception("Tipo de archivo prohibido");
                }

                if (Archivo.ContentLength > 10485760)
                {
                    throw new System.Exception("Archivo demasiado pesado");

                }
                var documento = new DocumentosDto
                {
                    Titulo = Titulo,
                    Descripcion = Descripcion,
                    Categoria = Categoria,
                    FechaRegistro = DateTime.Now,
                    Archivo = Archivo
                };
                GuardarArchivo(documento);
                _agregarDocumentos.AgregarDocumento(documento);

                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "Insert",
                    Tabla = "Documentos",
                    Descripcion = $"Insert de un nuevo documento de tipo {Categoria}, Titulo :{Titulo}"
                };
                _bitacora.RegistrarEvento(bitacora);
            }


            return RedirectToAction("Documentos");
        }
        private static void ComprobarTipodeArchivo(HttpPostedFileBase Archivo, out string[] extensionesPermitidas, out string extensionArchivo)
        {
            extensionesPermitidas = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            extensionArchivo = Path.GetExtension(Archivo.FileName).ToLower();
        }

        private void GuardarArchivo(DocumentosDto documento)
        {
            var nombreArchivo = Path.GetFileNameWithoutExtension(documento.Archivo.FileName);
            var extension = Path.GetExtension(documento.Archivo.FileName);
            var rutaCarpeta = Server.MapPath("~/Uploads/Documentos");
            var rutaCompleta = Path.Combine(rutaCarpeta, $"{nombreArchivo}_{Guid.NewGuid()}{extension}");


            // Crear carpeta si no existe
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
            {
                documento.Archivo.InputStream.CopyTo(fileStream);
            }

            // Guardar solo la ruta relativa en la base de datos
            documento.RutaArchivo = "~/Uploads/Documentos" + Path.GetFileName(rutaCompleta);
        }
    }
}