using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Configuration;
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

        [HttpGet]
        public ActionResult SendMail()
        {
            ML.Result result = new ML.Result();
            try
            {
                string correo = ConfigurationManager.AppSettings["CorreoSMTP"].ToString();
                string password = ConfigurationManager.AppSettings["PassworSMTP"].ToString();
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["PuertoSMTP"].ToString());
                bool defaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultCredentialsSMTP"].ToString());
                bool isHtmlBody = Convert.ToBoolean(ConfigurationManager.AppSettings["IsHtmlBodySMTP"].ToString());
                string Asunto = "Asunto";

                string body = "";
                string path = Server.MapPath("~/Content /PlantillaEmail/Email.html");
                StreamReader reader = new StreamReader(path);
                body = reader.ReadToEnd();

                body.Replace("{{destinatario}}", "Jorge Guevara Flores");
                body.Replace("{{nombreUsuario}}", "Jorge Guevara Flores");
                body.Replace("{{Titulo}}", "Test Envio");
                body.Replace("{{cuerpoCorreo}}", "Prueva para verificar el funcionamiento del correo.");

                //Remplazo de valores,

                var SMTPClient = new SmtpClient
                {
                    Port = puerto,
                    UseDefaultCredentials = defaultCredentials,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Benja"),
                    Subject = Asunto,
                    Body = body,
                    IsBodyHtml = isHtmlBody,
                };
                //mensaje.To.Add("jguevaraflores3@gmail.com");
                mensaje.To.Add("bemjamintexis@gmail.com");
                SMTPClient.Send(mensaje);

                result.Correct = true;
            }
            catch (Exception ex) { 
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            ViewBag.succesMessage = "El correo se envio de manera correcta";
            ViewBag.result = result;
            return PartialView("_MessageNotification");
        }
    }
}