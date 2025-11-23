using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.AgregarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EditarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EliminarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.ListarAnunciosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Anuncios.AgregarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EditarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EliminarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.ListarAnuncios;
using Campus.UI.Filtros;
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

    // GET: Anuncios/ListarAnuncios
    public ActionResult ListarAnuncios()
    {
        try
        {
            var anuncios = _listarAnunciosLN.ListarAnuncios();
            return View(anuncios);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al cargar los anuncios: " + ex.Message);
            return View(new List<AnuncioDto>());
        }
    }

    // GET: Anuncios/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Anuncios/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(AnuncioDto anuncio)
    {
        if (ModelState.IsValid)
        {
            try

            {
                anuncio.FechaPublicacion = DateTime.Now;
                if (anuncio.Imagen != null && anuncio.Imagen.ContentLength > 0)
                {
                    ComprobarTipodeArchivo(anuncio, out string[] extensionesPermitidas, out string extensionArchivo);
                    if (!extensionesPermitidas.Contains(extensionArchivo))
                    {
                        ModelState.AddModelError("", "Tipo de archivo no permitido.");
                        return View(anuncio);
                    }
                    GuardarArchivo(anuncio);
                }
                _agregarAnunciosLN.AgregarAnuncio(anuncio);
                return RedirectToAction("ListarAnuncios");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el anuncio: " + ex.Message);
                return View(anuncio);
            }
        }
        return View(anuncio);
    }

    // GET: Anuncios/Edit/5
    public ActionResult Edit(int id)
    {
        var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
        if (anuncio == null)
        {
            return HttpNotFound();
        }
        return View(anuncio);
    }

    // POST: Anuncios/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(AnuncioDto anuncio)
    {
        if (!ModelState.IsValid) return View(anuncio);
        try
        {
            string imagenAnterior = Request.Form["ImagenActual"];
            bool eliminarImagen = Request.Form["EliminarImagen"] == "true";

            if (eliminarImagen && !string.IsNullOrEmpty(imagenAnterior))
            {
                string rutaCompleta = Server.MapPath(imagenAnterior);
                if (System.IO.File.Exists(rutaCompleta)) System.IO.File.Delete(rutaCompleta);
                anuncio.ImagenRuta = null;
            }
            else if (anuncio.Imagen != null && anuncio.Imagen.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(imagenAnterior))
                {
                    string rutaCompleta = Server.MapPath(imagenAnterior);
                    if (System.IO.File.Exists(rutaCompleta)) System.IO.File.Delete(rutaCompleta);
                }
                ComprobarTipodeArchivo(anuncio, out string[] extensionesPermitidas, out string extensionArchivo);
                if (!extensionesPermitidas.Contains(extensionArchivo))
                {
                    ModelState.AddModelError("", "Tipo de archivo no permitido.");
                    return View(anuncio);
                }
                GuardarArchivo(anuncio);
            }
            else
            {
                anuncio.ImagenRuta = imagenAnterior;
            }

            _editarAnunciosLN.EditarAnuncio(anuncio);
            return RedirectToAction("ListarAnuncios");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al editar el anuncio: " + ex.Message);
            return View(anuncio);
        }
    }

    // GET: Anuncios/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio); 
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al cargar el anuncio para eliminar: " + ex.Message);
            return RedirectToAction("ListarAnuncios");
        }
    }

    // POST: Anuncios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }

            // Eliminar la imagen si existe
            if (!string.IsNullOrEmpty(anuncio.ImagenRuta))
            {
                string rutaCompleta = Server.MapPath(anuncio.ImagenRuta);
                if (System.IO.File.Exists(rutaCompleta)) System.IO.File.Delete(rutaCompleta);
            }

            _eliminarAnunciosLN.EliminarAnuncio(id);
            return RedirectToAction("ListarAnuncios");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al eliminar el anuncio: " + ex.Message);
            return RedirectToAction("ListarAnuncios");
        }
    }

    // GET: Anuncios/AnunciosEstudiantes
    public ActionResult AnunciosEstudiantes()
    {
        try
        {
            var anuncios = _listarAnunciosLN.ListarAnuncios(); 
            return View(anuncios);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al cargar los anuncios: " + ex.Message);
            return View(new List<AnuncioDto>());
        }
    }
    // GET: Anuncios/Details/5
    public ActionResult DetailsParcial(int id)
    {
        try
        {
            var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DetailsParcial",anuncio);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error al cargar el anuncio: " + ex.Message);
            return RedirectToAction("ListarAnuncios");
        }
    }

    private static void ComprobarTipodeArchivo(AnuncioDto anuncio, out string[] extensionesPermitidas, out string extensionArchivo)
    {
        extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        extensionArchivo = Path.GetExtension(anuncio.Imagen.FileName).ToLower();
    }

    private void GuardarArchivo(AnuncioDto anuncio)
    {
        var nombreArchivo = Path.GetFileName(anuncio.Imagen.FileName);
        var rutaCarpeta = Server.MapPath("~/Uploads/Anuncios/");
        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
        if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

        using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
        {
            anuncio.Imagen.InputStream.CopyTo(fileStream);
        }

        anuncio.ImagenRuta = "~/Uploads/Anuncios/" + nombreArchivo;
    }
}
