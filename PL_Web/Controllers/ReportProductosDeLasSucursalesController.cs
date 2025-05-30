using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL_Web.Controllers
{
    public class ReportProductosDeLasSucursalesController : Controller
    {
        // GET: ReportProductosDeLasSucursales
        public ActionResult GetAll()
        {
            ML.Result result = BL.ProductoSucursal.GetAllReportingService();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportProductosDeLasSucursales.rdlc";
            List<object> Lista = new List<object>();
            foreach(ML.ProductoSucursal newProduct in result.Objects)
            {
                var producto = new
                {
                    NombreSucursal = newProduct.Sucursal.Nombre,
                    NombreProducto = newProduct.Producto.Nombre,
                    Descripcion = newProduct.Producto.Descripcion,
                    Precio = newProduct.Producto.Precio
                };
                Lista.Add(producto);
            }
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetProductosSucursales", Lista));
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}