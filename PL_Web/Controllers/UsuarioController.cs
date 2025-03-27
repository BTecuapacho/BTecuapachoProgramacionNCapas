using Microsoft.AspNet.Identity;
using ML;
using PL_Web.UsuarioReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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
            //ML.Result result = BL.Usuarios.GetAllEF(usuario);// se usa EF
            //usuario.Usuarios = result.Objects; //solo si es correct sin manda una lista vacia
            UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
            var result = objectUsuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects.ToList();
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
            //ML.Result result = BL.Usuarios.GetAllEF(usuario);// se usa EF
            //usuario.Usuarios = result.Objects; //solo si es correct sin manda una lista vacia
            UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
            var result = objectUsuario.GetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects.ToList();
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
                //ML.Result result = BL.Usuarios.GetByIDEF(IdUsuario.Value);// se usa EF
                //usuario = (ML.Usuario)result.Object;// se usa EF
                UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
                var result = objectUsuario.GetById(IdUsuario.Value);
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
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImagenUpload"];
                if (file != null && file.ContentLength != 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }
                if (usuario.IdUsuario == 0)
                {
                    //ML.Result result = BL.Usuarios.AddEF(usuario);// se usa EF
                    UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
                    var result = objectUsuario.Add(usuario);
                    ViewBag.succesMessage = "El usuario se inserto de manera correcta";
                    ViewBag.result = result;
                    return PartialView("_MessageNotification");
                }
                else
                {
                    //ML.Result result = BL.Usuarios.UpdateEF(usuario);// se usa EF
                    UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
                    var result = objectUsuario.Update(usuario);
                    ViewBag.succesMessage = "El usuario se actualizo de manera correcta";
                    ViewBag.result = result;
                    return PartialView("_MessageNotification");
                }
            }
            else
            {
                usuario.Direccion.Colonia.Colonias = new List<object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                ML.Result resultRoles = BL.Rol.GetAllEF();
                usuario.Rol.Roles = resultRoles.Objects;
                ML.Result resultEstado = BL.Estado.GetAllEF();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                if(usuario.Direccion.Colonia.Municipio.Estado.IdEstado != 0)
                {
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                }
                if(usuario.Direccion.Colonia.Municipio.IdMunicipio != 0)
                {
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                }
                return View(usuario);
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
            //ML.Result result = BL.Usuarios.DeleteEF(IdUsuario);// se usa EF
            UsuarioReference.UsuarioClient objectUsuario = new UsuarioReference.UsuarioClient();
            var result = objectUsuario.Delete(IdUsuario);
            ViewBag.succesMessage = "El usuario se elimino de manera correcta";
            ViewBag.result = result;
            return PartialView("_MessageNotification");
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

        [HttpPost]
        public ActionResult CargaMasivaExel()
        {
            string extencionPermitida = ".xlsx";
            HttpPostedFileBase exel = Request.Files["ExelXLSX"];
            if (Session["RutaExcel"] == null)
            {
                if (exel.ContentLength != 0)
                {
                    string extencionObtenida = Path.GetExtension(exel.FileName);
                    if (extencionObtenida == extencionPermitida)
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(exel.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmmssfff") + ".xslx";
                        if (!System.IO.File.Exists(ruta))
                        {
                            exel.SaveAs(ruta);
                            string cadenaConecion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;
                            ML.Result resultLeerExel = BL.Usuarios.LeerExel(cadenaConecion);
                            if (resultLeerExel.Objects.Count > 0)
                            {
                                ML.Result resultValidacionExel = BL.Usuarios.ValidarExel(resultLeerExel.Objects);
                                if (resultValidacionExel.Objects.Count > 0)
                                {
                                    ViewBag.ErroresExel = resultValidacionExel.Objects;
                                    return PartialView("_ModalExcel");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                }
                            }
                            else
                            {
                                ViewBag.ErroresExel = "Ocurrio un error inesperado al leer el excel vuelva a subir el excel";
                                return PartialView("_ModalExcel");
                            }
                        }
                        else {
                            ViewBag.ErroresExel = "Ocurrio un error inesperado vuelva a subir el excel";
                            return PartialView("_ModalExcel");
                        }
                    }
                    else
                    {
                        ViewBag.ErroresExel = "El exel teine que tener la extencion .xlsx (Suba un archivo de exel)";
                        return PartialView("_ModalExcel");
                    }
                }
                else
                {
                    ViewBag.ErroresExel = "No se recivio ningun archivo";
                    return PartialView("_ModalExcel");
                }
            }
            else
            {
                string cadenaConecion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();
                ML.Result resultLeerExel = BL.Usuarios.LeerExel(cadenaConecion);
                if (resultLeerExel.Objects.Count > 0)
                {
                    ML.Result resultInsertExcel = InsertDatosExel(resultLeerExel.Objects);
                    ViewBag.ErroresExel = resultInsertExcel.Objects;
                    Session["RutaExcel"] = null;
                    return PartialView("_ModalExcel");
                }
                else
                {
                    ViewBag.ErroresExel = "Ocurrio un error inesperado al leer el excel";
                    return PartialView("_ModalExcel");
                }
            }
            return RedirectToAction("GetAll");
        }

        public static ML.Result InsertDatosExel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int contador = 1;
            foreach (ML.Usuario usuario in usuarios)
            {
                ML.ResultExel resultInsertExcel = new ResultExel();
                ML.Result resultAdd = BL.Usuarios.AddEF(usuario);
                resultInsertExcel.NumeroRegistro = contador;
                if (resultAdd.Correct)
                {
                    resultInsertExcel.ErrorMessage = "El usuario de inserto de manera correcta";
                }
                else
                {
                    resultInsertExcel.ErrorMessage = resultAdd.ErrorMessage;
                }
                result.Objects.Add(resultInsertExcel);
                contador++;
            }
            return result;
        }

        [HttpGet]
        public JsonResult GetAllExcel()
        {
            string cadenaConecion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();
            ML.Result JsonResult = BL.Usuarios.LeerExel(cadenaConecion);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}
