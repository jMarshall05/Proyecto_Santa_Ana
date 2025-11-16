using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.AgregarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EditarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.EliminarAnunciosLN;
using Campus.Abstracciones.LogicaDeNegocio.Anuncios.ListarAnunciosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Anuncios.AgregarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EditarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.EliminarAnuncios;
using Campus.LogicaDeNegocio.Anuncios.ListarAnuncios;
using Campus.LogicaDeNegocio.Bitacora;
using Microsoft.AspNet.Identity;
public class AnunciosController : Controller
{
    private readonly IListarAnunciosLN _listarAnunciosLN;
    private readonly IAgregarAnunciosLN _agregarAnunciosLN;
    private readonly IEliminarAnunciosLN _eliminarAnunciosLN;
    private readonly IEditarAnunciosLN _editarAnunciosLN;
    private readonly IBitacoraLN _bitacora;


    public AnunciosController()
    {
        _listarAnunciosLN = new ListarAnunciosLN();
        _agregarAnunciosLN = new AgregarAnunciosLN();
        _eliminarAnunciosLN = new EliminarAnunciosLN();
        _editarAnunciosLN = new EditarAnunciosLN();
        _bitacora = new BitacoraLN();
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
    public ActionResult CreateParcial()
    {
        return PartialView("_CreateParcial");
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
                    GenerarRuta(anuncio);
                    using (var fileStream = new FileStream(Server.MapPath(anuncio.ImagenRuta), FileMode.Create))
                    {
                        anuncio.Imagen.InputStream.CopyTo(fileStream);
                    }
                }
                _agregarAnunciosLN.AgregarAnuncio(anuncio);

                // Bitácora: inserción de nuevo anuncio
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = User.Identity.GetUserId(),
                    Accion = "INSERT",
                    Tabla = "Anuncios",
                    Descripcion = $"Creación de anuncio '{anuncio.Titulo}'"
                };
                _bitacora.RegistrarEvento(bitacora);

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
    public ActionResult EditParcial(int id)
    {
        var anuncio = _listarAnunciosLN.ObtenerAnuncioPorId(id);
        if (anuncio == null)
        {
            return HttpNotFound();
        }
        return PartialView("_EditParcial", anuncio);
    }

    // POST: Anuncios/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditParcialAsync(AnuncioDto anuncio)
    {
        if (!ModelState.IsValid) return View(anuncio);
        try
        {
            string imagenAnterior = Request.Form["ImagenActual"];
            bool eliminarImagen = Request.Form["EliminarImagen"] == "true";
            bool nuevaImagen = false;

            if (eliminarImagen && !string.IsNullOrEmpty(imagenAnterior))
            {
                string rutaCompleta = Server.MapPath(imagenAnterior);
                if (System.IO.File.Exists(rutaCompleta)) System.IO.File.Delete(rutaCompleta);
                anuncio.ImagenRuta = null;
            }
            else if (anuncio.Imagen != null && anuncio.Imagen.ContentLength > 0)
            {
                nuevaImagen = true;
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
            }
            else
            {
                anuncio.ImagenRuta = imagenAnterior;
            }
            if (nuevaImagen)
                GenerarRuta(anuncio);

            var resultado = await _editarAnunciosLN.EditarAnuncio(anuncio);

            if (resultado == true && nuevaImagen)
            {
                using (var fileStream = new FileStream(Server.MapPath(anuncio.ImagenRuta), FileMode.Create))
                {
                    anuncio.Imagen.InputStream.CopyTo(fileStream);
                }
            }

            var bitacora = new BitacoraDto
            {
                Fecha = DateTime.Now,
                Usuario = User.Identity.GetUserId(),
                Accion = "UPDATE",
                Tabla = "Anuncios",
                Descripcion = $"Actualización de anuncio ID: {anuncio.IdAnuncio} - '{anuncio.Titulo}'"
            };
            _bitacora.RegistrarEvento(bitacora);

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

            var bitacora = new BitacoraDto
            {
                Fecha = DateTime.Now,
                Usuario = User.Identity.GetUserId(),
                Accion = "DELETE",
                Tabla = "Anuncios",
                Descripcion = $"Eliminación lógica de anuncio ID: {id} - '{anuncio.Titulo}' - Estado cambiado a inactivo"
            };
            _bitacora.RegistrarEvento(bitacora);

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
            return PartialView("_DetailsParcial", anuncio);
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

    private void GenerarRuta(AnuncioDto anuncio)
    {
        var nombreArchivo = Path.GetFileNameWithoutExtension(anuncio.Imagen.FileName);
        var extension = Path.GetExtension(anuncio.Imagen.FileName);
        var rutaCarpeta = Server.MapPath("~/Uploads/Anuncios/");
        var rutaCompleta = Path.Combine(rutaCarpeta, $"{nombreArchivo}_{Guid.NewGuid()}{extension}");
        if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);
        anuncio.ImagenRuta = "~/Uploads/Anuncios/" + Path.GetFileName(rutaCompleta);
    }
}
