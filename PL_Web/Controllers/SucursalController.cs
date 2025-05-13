using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpGet]
        public JsonResult GetAllRest()
        {
            ML.Result result = BL.Sucursal.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}