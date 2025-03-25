using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class CRUDJsController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllJsonResult()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultRoles = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultRoles.Objects;
            usuario.Rol.IdRol = 0;
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            ML.Result result = BL.Usuarios.GetAllEFJs(usuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CambiarEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuarios.CambiarEstatusEF(IdUsuario, Estatus);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            ML.Result JsonResult = BL.Rol.GetAllEF();
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }        
        
        [HttpGet]
        public JsonResult GetAllEstados()
        {
            ML.Result JsonResult = BL.Estado.GetAllEF();
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int IdUsuario)
        {
            ML.Result JsonResult = BL.Usuarios.DeleteEF(IdUsuario);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(ML.Usuario usuario)
        {
            ML.Result JsonResult = new ML.Result();
            if (ModelState.IsValid)
            {
                usuario.Imagen = !string.IsNullOrEmpty(usuario.ImagenBase64) ? Convert.FromBase64String(usuario.ImagenBase64) : null;
                JsonResult = BL.Usuarios.AddEF(usuario);
            }
            else
            {
                JsonResult.Correct = false; 
                JsonResult.ErrorMessage = "Los datos ingresados no cumplen con el formato correcto";
            }
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(ML.Usuario usuario)
        {
            ML.Result JsonResult = new ML.Result();
            if (ModelState.IsValid)
            {
                usuario.Imagen = !string.IsNullOrEmpty(usuario.ImagenBase64) ? Convert.FromBase64String(usuario.ImagenBase64) : null;
                JsonResult = BL.Usuarios.UpdateEF(usuario);
            }
            else
            {
                JsonResult.Correct = false; 
                JsonResult.ErrorMessage = "Los datos ingresados no cumplen con el formato correcto";
            }
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuarios.GetByIDEF(IdUsuario);
            ML.Usuario usuario = new ML.Usuario();
            usuario = (ML.Usuario)result.Object;
            usuario.ImagenBase64 = usuario.Imagen != null ? Convert.ToBase64String(usuario.Imagen) : "";
            usuario.Imagen = null;
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }
    }
}