using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.AccesoDatos.Cursos.ListarCursosLN;
using Campus.Abstracciones.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorILN;
using Campus.Abstracciones.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.Abstracciones.LogicaDeNegocio.Materias.ListarMateriasLN;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorIdLN;
using Campus.LogicaDeNegocio.Cursos.ListarCursosLN;
using Campus.LogicaDeNegocio.EstudianteGrupo.BuscarEstudianteGrupoPorIdLN;
using Campus.LogicaDeNegocio.Grupos.ListarGrupos;
using Campus.LogicaDeNegocio.Materias.ListarMaterias;
using Campus.LogicaDeNegocio.Usuarios.ObtenerUsuariosPorId;
using Campus.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Campus.UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly IListarCursoLN _listarCursos;
        private readonly IObtenerUsuariosPorIdLN _obtenerUsuariosPorId;
        private readonly IListarMateriasLN _listarMateriasLN;
        private readonly IListarGruposLN _listarGruposLN;
        private ApplicationUserManager _userManager;
        private readonly IBuscarEstudianteGrupoPorIdLN _estudianteGrupoLN;

        public HomeController()
        {
            _listarCursos = new ListarCursosLN();
            _obtenerUsuariosPorId = new ObtenerUsuariosPorIdLN();
            _listarMateriasLN = new ListarMateriasLN();
            _listarGruposLN = new ListarGruposLN();
            _estudianteGrupoLN = new BuscarEstudianteGrupoPorIdLN();

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
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("login", "Account");

            var id = User.Identity.GetUserId();
            if (id == null)
                return RedirectToAction("login", "Account");
            
            if (User.IsInRole("Profesores"))
            {
                var listaDeCursos = _listarCursos.ListarCursos().Where(u => u.ProfesorId == id);
                foreach (var item in listaDeCursos)
                {
                    var usuario = _obtenerUsuariosPorId.ObtenerUsuarioPorId(item.ProfesorId);
                    item.NombreMateria = _listarMateriasLN.ObtenerMateriaPorId(item.MateriaId).Nombre;
                    item.NombreGrupo = _listarGruposLN.BuscarGruposPorId(item.GrupoId).nombre_grupo;
                    item.NombreProfesor = usuario.Nombre + " " + usuario.Apellido;
                }
                return View(listaDeCursos);
            }
            else if (User.IsInRole("Estudiantes"))
            {
                var grupo = _estudianteGrupoLN.BuscarEstudianteGrupoPorEstudianteId(id);
                if (grupo == null)
                    return View();
                var listaDeCursos = _listarCursos.ListarCursos().Where(u => u.GrupoId == grupo.GrupoId);
                foreach (var item in listaDeCursos)
                {
                    var usuario = _obtenerUsuariosPorId.ObtenerUsuarioPorId(item.ProfesorId);
                    item.NombreMateria = _listarMateriasLN.ObtenerMateriaPorId(item.MateriaId).Nombre;
                    item.NombreGrupo = _listarGruposLN.BuscarGruposPorId(item.GrupoId).nombre_grupo;
                    item.NombreProfesor = usuario.Nombre + " " + usuario.Apellido;
                }
                return View(listaDeCursos);
            }



            return View();
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