using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PL_Web.Controllers
{
    public class ProductoStockController : Controller
    {
        // GET: ProductoStock
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal
            {
                Sucursal = new ML.Sucursal(),
                Producto = new ML.Producto()
            };
            ML.Result resultSucursales = BL.Sucursal.GetAll();
            productoSucursal.Sucursal.Sucursales = resultSucursales.Objects;
            productoSucursal.ProductosSucursales = new List<object>();
            return View(productoSucursal);
        }

        [HttpPost]
        public ActionResult GetAll(ML.ProductoSucursal productoSucursalIn)
        {
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal
            {
                Sucursal = new ML.Sucursal(),
                Producto = new ML.Producto()
            };
            ML.Result resultSucursales = BL.Sucursal.GetAll();
            productoSucursal.Sucursal.Sucursales = resultSucursales.Objects;
            ML.Result result = BL.ProductoSucursal.GetAll(productoSucursalIn.Sucursal.IdSucursal);
            if (result.Correct)
            {
                productoSucursal.ProductosSucursales = result.Objects;
            }
            else
            {
                productoSucursal.ProductosSucursales = new List<object>();
            }
            return View(productoSucursal);
        }

        [HttpGet]
        public JsonResult GetAllProductos(int IdSucursal)
        {
            ML.Result result = BL.ProductoSucursal.GetAll(IdSucursal);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}