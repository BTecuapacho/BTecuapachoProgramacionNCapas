using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL_Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultRoles = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultRoles.Objects;
            usuario.Rol.IdRol = 0;
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            ML.Result result = BL.Usuarios.GetAllEF(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result resultRoles = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultRoles.Objects;
            usuario.Nombre = usuario.Nombre != null ? usuario.Nombre : "";
            usuario.ApellidoPaterno = usuario.ApellidoPaterno != null ? usuario.ApellidoPaterno : "";
            usuario.ApellidoMaterno = usuario.ApellidoMaterno != null ? usuario.ApellidoMaterno : "";
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;
            ML.Result result = BL.Usuarios.GetAllEF(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == null)
            {
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Colonias = new List<object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
            }
            else
            {
                ML.Result result = BL.Usuarios.GetByIDEF(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            }
            ML.Result resultRoles = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultRoles.Objects;
            ML.Result resultEstados = BL.Estado.GetAllEF();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["ImagenUpload"];
            if(file != null && file.ContentLength != 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            if (usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuarios.AddEF(usuario);
                if (result.Correct) {
                    return RedirectToAction("GetAll");
                } else {
                    return RedirectToAction("Form");
                }
            }
            else
            {
                ML.Result result = BL.Usuarios.UpdateEF(usuario);
                if (result.Correct) {
                    return RedirectToAction("GetAll");
                } else {
                    return RedirectToAction("Form");
                }
            }
        }

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase file)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(file.InputStream);
            byte[] bytes = reader.ReadBytes((int)file.ContentLength);
            return bytes;
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuarios.DeleteEF(IdUsuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return RedirectToAction("GetAll");
            }
        }

        [HttpPost]
        public JsonResult CambiarEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuarios.CambiarEstatusEF(IdUsuario, Estatus);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMunicipiosByIdEstado(int IdEstado) { 
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetColoniasByIdMunicipio(int IdMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}
