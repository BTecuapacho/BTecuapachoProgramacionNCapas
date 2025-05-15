using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        [HttpGet]
        public ActionResult GetAll()
        {
            if (Session["llave"] == null)
            {
                Session["llave"] = ConfigurationManager.AppSettings["llaveGMP"].ToString();
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetAllRest()
        {
            ML.Result result = BL.Sucursal.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Form(int? IdSucursal)
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            if (IdSucursal != null)
            {
                ML.Result result = BL.Sucursal.GetById(IdSucursal.Value);
                if (result.Correct)
                {
                    sucursal = (ML.Sucursal)result.Object;
                }
            }
            return View(sucursal);
        }

        [HttpPost]
        public ActionResult Form(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            if (sucursal.IdSucursal == 0)
            {
                result = BL.Sucursal.Add(sucursal);
                ViewBag.succesMessage = "El producto se inserto de manera correcta";
                ViewBag.result = result;
                return PartialView("_MessageNotification");
            }
            else
            {
                result = BL.Sucursal.Update(sucursal);
                ViewBag.succesMessage = "El producto se actualizo de manera correcta";
                ViewBag.result = result;
                return PartialView("_MessageNotification");
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdSucursal)
        {
            ML.Result result = BL.Sucursal.Delete(IdSucursal);
            ViewBag.succesMessage = "La sucursal se elimino de manera correcta";
            ViewBag.result = result;
            return PartialView("_MessageNotification");
        }
    }
}