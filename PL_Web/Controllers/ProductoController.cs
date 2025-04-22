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
            return View();
        }

        [HttpGet]
        public JsonResult GetProductos(int IdCategoria, int IdSubCategoria) {
            ML.Result result = BL.Producto.GetAll(IdCategoria, IdSubCategoria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}