using System.Web.Mvc;
using Campus.Abstracciones.ModelosUI;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.ListarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.AgregarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EliminarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EditarAnunciosLN;
using Campus.LogicaDeNegocio.Anuncios.ListarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.AgregarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EliminarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EditarAnuncios;

namespace Campus.UI.Controllers
{
    public class AnunciosController : Controller
    {
        private readonly IListarAnunciosLN _listarAnunciosLN;
        private readonly IAgregarAnunciosLN _agregarAnunciosLN;
        private readonly IEliminarAnunciosLN _eliminarAnunciosLN;
        private readonly IEditarAnunciosLN _editarAnunciosLN;

        public AnunciosController()
        {
            _listarAnunciosLN = new ListarAnunciosLN();
            _agregarAnunciosLN = new AgregarAnunciosLN();
            _eliminarAnunciosLN = new EliminarAnunciosLN();
            _editarAnunciosLN = new EditarAnunciosLN();
        }

        public ActionResult ListarAnuncios()
        {
            var listaDeAnuncios = _listarAnunciosLN.ListarAnuncios();
            return View(listaDeAnuncios);
        }

   
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnuncioDto anuncio)
        {
            if (ModelState.IsValid)
            {
                _agregarAnunciosLN.AgregarAnuncio(anuncio);
                return RedirectToAction("ListarAnuncios");
            }

            return View(anuncio);
        }

        public ActionResult Edit(int id)
        {
            var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }

            return View(anuncio);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnuncioDto anuncio)
        {
            if (ModelState.IsValid)
            {
                _editarAnunciosLN.EditarAnuncio(anuncio);
                return RedirectToAction("ListarAnuncios");
            }

            return View(anuncio);
        }

 
        public ActionResult Delete(int id)
        {
            ViewBag.AnuncioId = id;
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _eliminarAnunciosLN.EliminarAnuncio(id);
                return RedirectToAction("ListarAnuncios");
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el anuncio.");
                return View("Delete", id);
            }
        }
    }
}
