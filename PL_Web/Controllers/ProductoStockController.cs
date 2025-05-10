using Microsoft.Owin.Security;
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
            return View();
        }

        [HttpGet]
        public JsonResult GetAllProductos(int IdSucursal)
        {
            ML.Result result = BL.ProductoSucursal.GetAll(IdSucursal);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public void SendMail(int stockAnterior, int stockNuevo, string sucursal, string producto)
        {
            try
            {
                string correo = ConfigurationManager.AppSettings["CorreoSMTP"].ToString();
                string password = ConfigurationManager.AppSettings["PassworSMTP"].ToString();
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["PuertoSMTP"].ToString());
                bool defaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultCredentialsSMTP"].ToString());
                bool isHtmlBody = Convert.ToBoolean(ConfigurationManager.AppSettings["IsHtmlBodySMTP"].ToString());
                string Asunto = $"Cambios al stock del producto {producto}";

                string body = "";
                string path = Server.MapPath("~/Content/PlantillaEmail/Email.html");
                string pathImagen = Server.MapPath("~/Content/imagenes/logo.png");
                StreamReader reader = new StreamReader(path);
                body = reader.ReadToEnd();

                //body = body.Replace("{{destinatario}}", "Jorge Guevara Flores");
                //body = body.Replace("{{nombreUsuario}}", "Jorge Guevara Flores");
                body = body.Replace("{{destinatario}}", "Benjamin Tecuapacho");
                body = body.Replace("{{nombreUsuario}}", "Benjamin Tecuapacho Mendez");
                body = body.Replace("{{Titulo}}", "Stock");
                body = body.Replace("{{cuerpoCorreo}}", $"En la sucursar {sucursal} se actualizo el stock de manera correcta del producto {producto}, el stock que tenia anterioremente era de {stockAnterior} unidades y el nuevo stock es de {stockNuevo} unidades");
                body = body.Replace("{{ruta}}", string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Action("GetAll()", "ProductoStock")));

                var SMTPClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = puerto,
                    UseDefaultCredentials = defaultCredentials,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };


                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Productos Naturales"),
                    Subject = Asunto,
                    Body = body,
                    IsBodyHtml = isHtmlBody,
                };

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource imagen = new LinkedResource(pathImagen)
                {
                    ContentId = "imagen",
                    ContentType = new System.Net.Mime.ContentType("image/png")
                };
                htmlView.LinkedResources.Add(imagen); 
                mensaje.AlternateViews.Add(htmlView);
                //mensaje.To.Add("jguevaraflores3@gmail.com");
                mensaje.To.Add("2003tecuapacho@gmail.com");
                SMTPClient.Send(mensaje);
            }
            catch (Exception ex) { 
            }
        }

        [HttpPost]
        public JsonResult Update(ML.ProductoSucursal productoSucursal)
        {
            ML.ProductoSucursal stockAnterior = new ML.ProductoSucursal
            {
                Producto = new ML.Producto(),
                Sucursal = new ML.Sucursal()
            };
            ML.Result resultGetLastStock = BL.ProductoSucursal.GetById(productoSucursal.IdProductoSucursal);
            if (resultGetLastStock.Correct)
            {
                stockAnterior = (ML.ProductoSucursal)resultGetLastStock.Object;
            }

            ML.Result result = BL.ProductoSucursal.UpdateStock(productoSucursal);
            if (result.Correct)
            {
                /*
                 El entero que viene en stockAnterior.Stock es el stock anterior,
                Y lo que vienen en productoSucursal.Stock es el nuevo stock
                 */
                SendMail(stockAnterior.Stock, productoSucursal.Stock, stockAnterior.Sucursal.Nombre, stockAnterior.Producto.Nombre);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSucursales()
        {
            ML.Result result = BL.Sucursal.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}