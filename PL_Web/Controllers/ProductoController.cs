using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto{ 
                SubCategoria = new ML.SubCategoria { 
                    Categoria = new ML.Categoria()
                } };
            ML.Result resultCategorias = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            producto.SubCategoria.SubCategorias = new List<object>();
            producto.Productos = new List<object>();
            return View(producto);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {

            if (producto.SubCategoria.Categoria.IdCategoria == 0 && producto.SubCategoria.IdSubCategoria == 0)
            {
                return RedirectToAction("GetAll");
            }
            ML.Result resultCategorias = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            if(producto.SubCategoria.Categoria.IdCategoria != 0)
            {
                ML.Result resultSubCategorias = BL.SubCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
            }
            else
            {
                producto.SubCategoria.SubCategorias = new List<object>();
            }
            ML.Result result = BL.Producto.GetAll(producto);
            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                producto.Productos = new List<object>();
            }
            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto{
                SubCategoria = new ML.SubCategoria
                {
                    Categoria = new ML.Categoria()
                }
            };
            if (IdProducto == null)
            {
                producto.SubCategoria.SubCategorias = new List<object>();
            }
            else
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value); 
                producto = (ML.Producto)result.Object;
                ML.Result resultSubCategorias = BL.SubCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
            }
            ML.Result resultCategorias = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            return View(producto);
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImagenUpload"];
                if (file != null && file.ContentLength != 0)
                {
                    producto.Imagen = ConvertirAArrayBytes(file);
                }
                if (producto.IdProducto == 0)
                {
                    ML.Result result = BL.Producto.Add(producto);
                    ViewBag.succesMessage = "El producto se inserto de manera correcta";
                    ViewBag.result = result;
                    return PartialView("_MessageNotification");
                }
                else
                {
                    ML.Result result = BL.Producto.Update(producto);
                    ViewBag.succesMessage = "El producto se actualizo de manera correcta";
                    ViewBag.result = result;
                    return PartialView("_MessageNotification");
                }
            }
            else
            {
                ML.Result resultCategorias = BL.Categoria.GetAll();
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
                if (producto.SubCategoria.Categoria.IdCategoria != 0)
                {
                    ML.Result resultSubCategorias = BL.SubCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                    producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
                }
                return View(producto);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);
            ViewBag.succesMessage = "El producto se elimino de manera correcta";
            ViewBag.result = result;
            return PartialView("_MessageNotification");
        }

        [HttpGet]
        public JsonResult GetByIdCategoria(int IdCategoria) {
            ML.Result result = BL.SubCategoria.GetByIdCategoria(IdCategoria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase file)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(file.InputStream);
            byte[] bytes = reader.ReadBytes((int)file.ContentLength);
            return bytes;
        }
    }
}