using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ListarUsuariosLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Usuarios.ListarUsuarios;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Campus.UI.Filtros;
using Campus.UI.Models;
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

        public HomeController()
        {
            _listarCursos = new ListarCursosLN();
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarGruposLN = new ListarGruposLN();
            _estudianteGrupoLN = new BuscarEstudianteGrupoPorIdLN();
            _listarUsuariosLN = new ListarUsuariosLN();

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


            return View();
        }
    }
}