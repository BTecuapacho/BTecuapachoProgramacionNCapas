using BL;
using Microsoft.Ajax.Utilities;
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
using System.Net;
using System.Net.Http;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;


namespace PL_Web.Controllers
{
    public class UsuarioController : Controller
    {
        // Crud Consumiendo servicios soap solo con una referencia y un objeto
        //=========================================================================================
        /*
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
        */

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase file)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(file.InputStream);
            byte[] bytes = reader.ReadBytes((int)file.ContentLength);
            return bytes;
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


        // Crud Consumiendo servicios soap haciendo peticiones post y Consumiendo Servicios REST
        //================================================================================================
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
            //Result con servicios soap
            //ML.Result result = GetAllXML(usuario);
            //Result con servicios REST
            ML.Result result = GetAllByREST(usuario);
            if (result.Correct)
            {
                //Con servicios soap
                //usuario.Usuarios = result.Objects.ToList();
                usuario.Usuarios = result.Objects;

                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = Unit.Percentage(900);
                reportViewer.Height = Unit.Percentage(900);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportUsuario.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetUsuario", result.Objects));
                ViewBag.ReportViewer = reportViewer;
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
            //Result con servicios soap
            //ML.Result result = GetAllXML(usuario);
            //Result con servicios REST
            ML.Result result = GetAllByREST(usuario);
            if (result.Correct)
            {
                //Con servicios soap
                //usuario.Usuarios = result.Objects.ToList();
                usuario.Usuarios = result.Objects;

                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = Unit.Percentage(900);
                reportViewer.Height = Unit.Percentage(900);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportUsuario.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetUsuario", result.Objects));
                ViewBag.ReportViewer = reportViewer;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            return View(usuario);
        }

        //FORM
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
                //Result con servicios soap
                //ML.Result result = GetByIdXML(IdUsuario.Value);
                //Result con servicios REST
                ML.Result result = GetByIdREST(IdUsuario.Value);
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
                    ML.Result result = AddByRest(usuario);
                    ViewBag.succesMessage = "El usuario se inserto de manera correcta";
                    ViewBag.result = result;
                    return PartialView("_MessageNotification");
                }
                else
                {
                    ML.Result result = UpdateByRest(usuario);
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
                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado != 0)
                {
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                }
                if (usuario.Direccion.Colonia.Municipio.IdMunicipio != 0)
                {
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                }
                return View(usuario);
            }
        }


        //Form Insert or Update con Soap
        /*[HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ML.Result result = AddOrUpdateXML(usuario);
                ViewBag.succesMessage = usuario.IdUsuario == 0 ? "El usuario se inserto de manera correcta" : "El usuario se actualizo de manera correcta";
                ViewBag.result = result;
                return PartialView("_MessageNotification");
            }
            else
            {
                usuario.Direccion.Colonia.Colonias = new List<object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                ML.Result resultRoles = BL.Rol.GetAllEF();
                usuario.Rol.Roles = resultRoles.Objects;
                ML.Result resultEstado = BL.Estado.GetAllEF();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado != 0)
                {
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                }
                if (usuario.Direccion.Colonia.Municipio.IdMunicipio != 0)
                {
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                }
                return View(usuario);
            }
        }*/

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //Con servicios soap
            //ML.Result result = DeleteXML(IdUsuario);
            ML.Result result = DeleteREST(IdUsuario);
            ViewBag.succesMessage = "El usuario se elimino de manera correcta";
            ViewBag.result = result;
            return PartialView("_MessageNotification");
        }

        //=========================================Peticiones XML=========================================
        //Petición GetAll
        [HttpPost]
        public ML.Result GetAllXML(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string accion = "http://tempuri.org/IUsuario/GetAll";
            string url = "http://localhost:50944/Usuario.svc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", accion);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";
            string myXML = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:GetAll>
         <tem:usuario>
            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
            <ml:Rol>
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
            </ml:Rol>
         </tem:usuario>
      </tem:GetAll>
   </soapenv:Body>
</soapenv:Envelope>";

            // Envia la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(myXML);
                stream.Write(content, 0, content.Length);
            }

            // Obtengo la respuesta
            try
            {
                using (WebResponse respuesta = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(respuesta.GetResponseStream()))
                    {
                        string resultXML = reader.ReadToEnd();
                        // Deserializar el XML y lo guardo en un result
                        result = ResultGetAll(resultXML); // Captura el objeto completo
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [NonAction]
        public ML.Result ResultGetAll(string xml)
        {
            ML.Result result = new ML.Result();
            var xdoc = XDocument.Parse(xml);
            // Acceder a GetAllUsuarioResult
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");
            if (objects.Count() != 0)
            {
                result.Objects = new List<object>();
                foreach (var usuarioGet in objects)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    //llenamos las propiedades de nuestro modelo usuario
                    usuario.ApellidoMaterno = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                    usuario.ApellidoPaterno = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                    usuario.CURP = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);
                    usuario.Celular = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                    usuario.Email = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                    usuario.Estatus = bool.TryParse(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out bool estatus) && estatus;
                    usuario.FechaNacimiento = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                    usuario.IdUsuario = int.TryParse(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out int IdUsuario) ? IdUsuario : 0;
                    usuario.Nombre = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                    usuario.Password = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                    usuario.Sexo = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                    usuario.Telefono = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                    usuario.UserName = (string)(usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);
                    var rolElement = usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                    if (rolElement != null)
                    {
                        usuario.Rol.Nombre = (string)(rolElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                    }
                    var direccionElement = usuarioGet.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                    if (direccionElement != null)
                    {
                        usuario.Direccion.Calle = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                        usuario.Direccion.NumeroExterior = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                        usuario.Direccion.NumeroInterior = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                        var coloniaElement = direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                        if (coloniaElement != null)
                        {
                            usuario.Direccion.Colonia.Nombre = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                            usuario.Direccion.Colonia.CodigoPostal = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);
                            var municipioElement = coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                            if (municipioElement != null)
                            {
                                usuario.Direccion.Colonia.Municipio.Nombre = (string)(municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                                var estadoElement = municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                                if (estadoElement != null)
                                {
                                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = (string)(estadoElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                                }
                            }
                        }
                    }
                    result.Objects.Add(usuario);
                }
                result.Correct = true;
            }
            else
            {
                result.Correct = false; result.ErrorMessage = "No se encontraron registros.";
            }
            return result;
        }

        //Petición Add or Update
        [NonAction]
        public string CreateXMLUsuario(ML.Usuario usuario)
        {
            string xml = $@"";
            if (usuario.IdUsuario == 0)
            {
                xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:Add>
                        <tem:usuario>
                            <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
                            <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
                            <ml:CURP>{EscapeXml(usuario.CURP)}</ml:CURP>
                            <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
                            <ml:Direccion>
                                <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
                                <ml:Colonia>
                                    <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                    <ml:Municipio>
                                        <ml:Estado>
                                            <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                                        </ml:Estado>
                                        <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                                    </ml:Municipio>
                                </ml:Colonia>
                                <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
                                <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
                            </ml:Direccion>
                           <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
                           <ml:Estatus>{usuario.Estatus.ToString().ToLower()}</ml:Estatus>
                           <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                           <ml:Imagen></ml:Imagen>
                           <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
                           <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
                           <ml:Rol>
                              <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                           </ml:Rol>
                           <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
                           <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
                           <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
                        </tem:usuario>
                      </tem:Add>
                   </soapenv:Body>
                </soapenv:Envelope>";
            }
            else
            {
                xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:Update>
                        <tem:usuario>
                        <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
                        <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
                        <ml:CURP>{EscapeXml(usuario.CURP)}</ml:CURP>
                        <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
                        <ml:Direccion>
                            <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
                            <ml:Colonia>
                                <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                <ml:Municipio>
                                    <ml:Estado>
                                    <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                                    </ml:Estado>
                                    <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                                </ml:Municipio>
                            </ml:Colonia>
                            <ml:IdDireccion>{usuario.Direccion.IdDireccion}</ml:IdDireccion>
                            <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
                            <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
                        </ml:Direccion>
                        <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
                        <ml:Estatus>{usuario.Estatus.ToString().ToLower()}</ml:Estatus>
                        <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                        <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
                        <ml:Imagen></ml:Imagen>
                        <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
                        <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
                        <ml:Rol>
                            <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                        </ml:Rol>
                        <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
                        <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
                        <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
                        </tem:usuario>
                      </tem:Update>
                   </soapenv:Body>
                </soapenv:Envelope>";
            }
            return xml;
        }

        [NonAction]
        public string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        [HttpPost]
        public ML.Result AddOrUpdateXML(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string accion = usuario.IdUsuario == 0 ? "http://tempuri.org/IUsuario/Add" : "http://tempuri.org/IUsuario/Update";
            string url = "http://localhost:50944/Usuario.svc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", accion);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";
            string myXML = CreateXMLUsuario(usuario);
            // Envia la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(myXML);
                stream.Write(content, 0, content.Length);
            }

            // Obtengo la respuesta
            try
            {
                using (WebResponse respuesta = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(respuesta.GetResponseStream()))
                    {
                        string resultXML = reader.ReadToEnd();
                        // Deserializar el XML y lo guardo en un result
                        result = ResultXML(resultXML); // Captura el objeto completo
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //Petición GetById
        [HttpPost]
        public ML.Result GetByIdXML(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string accion = "http://tempuri.org/IUsuario/GetById";
            string url = "http://localhost:50944/Usuario.svc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", accion);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";
            string myXML = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:GetById>
                     <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                  </tem:GetById>
               </soapenv:Body>
            </soapenv:Envelope>";

            // Envia la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(myXML);
                stream.Write(content, 0, content.Length);
            }

            // Obtengo la respuesta
            try
            {
                using (WebResponse respuesta = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(respuesta.GetResponseStream()))
                    {
                        string resultXML = reader.ReadToEnd();
                        // Deserializar el XML y lo guardo en un result
                        result = ResultGetByIdXML(resultXML); // Captura el objeto completo
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [NonAction]
        public ML.Result ResultGetByIdXML(string xml)
        {
            ML.Result result = new ML.Result();
            try {
                var xdoc = XDocument.Parse(xml);
                // Acceder a GetUsuarioByIdResult usando el namespace correcto
                var usuarioElement = xdoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Object" && e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

                if (usuarioElement != null) {
                    ML.Usuario usuario = new ML.Usuario
                    {
                        Rol = new ML.Rol(),
                        Direccion = new ML.Direccion
                        {
                            Colonia = new ML.Colonia
                            {
                                Municipio = new ML.Municipio
                                {
                                    Estado = new ML.Estado()
                                }
                            }
                        }
                    };
                    //Asigno los datos del XMLa mi modelo de usuario
                    XNamespace ns = "http://schemas.datacontract.org/2004/07/ML";
                    usuario.IdUsuario = int.TryParse(usuarioElement.Element(ns + "IdUsuario")?.Value, out int idUsuario) ? idUsuario : 0;
                    usuario.Nombre = usuarioElement.Element(ns + "Nombre")?.Value ?? string.Empty;
                    usuario.ApellidoPaterno = usuarioElement.Element(ns + "ApellidoPaterno")?.Value ?? string.Empty;
                    usuario.ApellidoMaterno = usuarioElement.Element(ns + "ApellidoMaterno")?.Value ?? string.Empty;
                    usuario.CURP = usuarioElement.Element(ns + "CURP")?.Value ?? string.Empty;
                    usuario.Celular = usuarioElement.Element(ns + "Celular")?.Value ?? string.Empty;
                    usuario.Email = usuarioElement.Element(ns + "Email")?.Value ?? string.Empty;
                    usuario.Estatus = bool.TryParse(usuarioElement.Element(ns + "Estatus")?.Value, out bool estatus) && estatus;
                    usuario.FechaNacimiento = usuarioElement.Element(ns + "FechaNacimiento")?.Value ?? string.Empty;
                    usuario.Password = usuarioElement.Element(ns + "Password")?.Value ?? string.Empty;
                    usuario.Sexo = usuarioElement.Element(ns + "Sexo")?.Value ?? string.Empty;
                    usuario.Telefono = usuarioElement.Element(ns + "Telefono")?.Value ?? string.Empty;
                    usuario.UserName = usuarioElement.Element(ns + "UserName")?.Value ?? string.Empty;
                    var rolElement = usuarioElement.Element(ns + "Rol");
                    if (rolElement != null)
                    {
                        usuario.Rol.IdRol = int.TryParse(rolElement.Element(ns + "IdRol")?.Value, out int idRol) ? idRol : 0;
                    }
                    var direccionElement = usuarioElement.Element(ns + "Direccion");
                    if (direccionElement != null)
                    {
                        usuario.Direccion.IdDireccion = int.TryParse(direccionElement.Element(ns + "IdDireccion")?.Value, out int idDireccion) ? idDireccion : 0;
                        usuario.Direccion.Calle = direccionElement.Element(ns + "Calle")?.Value ?? string.Empty;
                        usuario.Direccion.NumeroExterior = direccionElement.Element(ns + "NumeroExterior")?.Value ?? string.Empty;
                        usuario.Direccion.NumeroInterior = direccionElement.Element(ns + "NumeroInterior")?.Value ?? string.Empty;
                        var coloniaElement = direccionElement.Element(ns + "Colonia");
                        if (coloniaElement != null)
                        {
                            usuario.Direccion.Colonia.IdColonia = int.TryParse(coloniaElement.Element(ns + "IdColonia")?.Value, out int idColonia) ? idColonia : 0;
                            var municipioElement = coloniaElement.Element(ns + "Municipio");
                            if (municipioElement != null)
                            {
                                usuario.Direccion.Colonia.Municipio.IdMunicipio = int.TryParse(municipioElement.Element(ns + "IdMunicipio")?.Value, out int idMunicipio) ? idMunicipio : 0;
                                var estadoElement = municipioElement.Element(ns + "Estado");
                                if (estadoElement != null)
                                {
                                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.TryParse(estadoElement.Element(ns + "IdEstado")?.Value, out int idEstado) ? idEstado : 0;
                                }
                            }
                        }
                    }
                    result.Object = usuario;  result.Correct = true;
                } else {
                    result.Correct = false; result.ErrorMessage = "No se encontro el elemento Object en el XML";
                }
            } catch(Exception ex) {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = "Error al des-serializar el XML";
            }
            return result;
        }

        //Petición Delete
        [HttpPost]
        public ML.Result DeleteXML(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string accion = "http://tempuri.org/IUsuario/Delete";
            string url = "http://localhost:50944/Usuario.svc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", accion);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";
            string myXML = $@"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Delete>
         <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
      </tem:Delete>
   </soapenv:Body>
</soapenv:Envelope>";

            // Envia la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(myXML);
                stream.Write(content, 0, content.Length);
            }

            // Obtengo la respuesta
            try
            {
                using (WebResponse respuesta = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(respuesta.GetResponseStream()))
                    {
                        string resultXML = reader.ReadToEnd();
                        // Deserializar el XML y lo guardo en un result
                        result = ResultXML(resultXML); // Captura el objeto completo
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public ML.Result ResultXML(string xml)
        {
            ML.Result result = new ML.Result();
            var xdoc = XDocument.Parse(xml);
            result.Correct = bool.TryParse(xdoc.Descendants("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct").FirstOrDefault()?.Value, out bool correct) && correct;
            result.ErrorMessage = xdoc.Descendants("{http://schemas.datacontract.org/2004/07/SL_WCF}ErrorMessage").FirstOrDefault()?.Value ?? string.Empty;
            return result;
        }


        //=========================================Peticiones REST=========================================
        [NonAction]
        public ML.Result GetAllByREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["EndPointUsuario"].ToString();
                    //RecuperarBaseAddress de AppSettings 
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.PostAsJsonAsync<ML.Usuario>("GetAll", usuario);
                    responseTask.Wait(); //abrir otro hilo
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        if (readTask.Result.Objects.Count() > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (var resultItem in readTask.Result.Objects)
                            {
                                ML.Usuario newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                                result.Objects.Add(newUser);
                            }
                        }   
                        result.Correct = readTask.Result.Correct; result.ErrorMessage = readTask.Result.ErrorMessage; result.Exception = readTask.Result.Exception;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al obtener los usuarios";
                    }
                }
            }
            catch (Exception ex) { 
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
        
        [NonAction]
        public ML.Result GetByIdREST(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["EndPointUsuario"].ToString();
                    //RecuperarBaseAddress de AppSettings 
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync($"GetById/{IdUsuario}");
                    responseTask.Wait(); //abrir otro hilo
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario newUser = readTask.Result.Object != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString()) : null;
                        result.Object = newUser; result.Correct = readTask.Result.Correct; result.ErrorMessage = readTask.Result.ErrorMessage; result.Exception = readTask.Result.Exception;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al obtener los usuarios";
                    }
                }
            }
            catch (Exception ex) { 
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [NonAction]
        public ML.Result AddByRest(ML.Usuario usuario)
        {
            usuario.ImagenBase64 = usuario.Imagen != null ? Convert.ToBase64String(usuario.Imagen) : "";
            usuario.Imagen = null;
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["EndPointUsuario"].ToString();
                    //RecuperarBaseAddress de AppSettings 
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                    responseTask.Wait(); //abrir otro hilo
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Correct = readTask.Result.Correct; result.ErrorMessage = readTask.Result.ErrorMessage; result.Exception = readTask.Result.Exception;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al hacer la petición";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [NonAction]
        public ML.Result UpdateByRest(ML.Usuario usuario)
        {
            usuario.ImagenBase64 = usuario.Imagen != null ? Convert.ToBase64String(usuario.Imagen) : "";
            usuario.Imagen = null;
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["EndPointUsuario"].ToString();
                    //RecuperarBaseAddress de AppSettings 
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.PutAsJsonAsync<ML.Usuario>($"Update/{usuario.IdUsuario}", usuario);
                    responseTask.Wait(); //abrir otro hilo
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Correct = readTask.Result.Correct; result.ErrorMessage = readTask.Result.ErrorMessage; result.Exception = readTask.Result.Exception;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al hacer la petición";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        [NonAction]
        public ML.Result DeleteREST(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["EndPointUsuario"].ToString();
                    //RecuperarBaseAddress de AppSettings 
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.DeleteAsync($"Delete/{IdUsuario}");
                    responseTask.Wait(); //abrir otro hilo
                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Correct = readTask.Result.Correct; result.ErrorMessage = readTask.Result.ErrorMessage; result.Exception = readTask.Result.Exception;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al hacer la petición";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}
