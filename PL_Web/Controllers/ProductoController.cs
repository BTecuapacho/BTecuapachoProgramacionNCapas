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
        public JsonResult GetByIdCategoria(int IdCategoria) {
            ML.Result result = BL.SubCategoria.GetByIdCategoria(IdCategoria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}