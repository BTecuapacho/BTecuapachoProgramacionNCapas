using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductoSucursal
    {
        public static ML.Result GetAll(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var ProductosSucursales = (from ProductoSucursal in context.ProductoSucursals
                                               join Producto in context.Productoes on ProductoSucursal.IdProducto equals Producto.IdProducto
                                               join Sucursal in context.Sucursals on ProductoSucursal.IdSucursal equals Sucursal.IdSucursal
                                               where ProductoSucursal.IdSucursal == IdSucursal
                                               select new
                                               {
                                                   IdProductoSucursal = ProductoSucursal.IdProductoSucursal,
                                                   IdProducto = Producto.IdProducto,
                                                   NombreProducto = Producto.Nombre,
                                                   IdSucursal = Sucursal.IdSucursal,
                                                   NombreSucursal = Sucursal.Nombre,
                                                   Stok = ProductoSucursal.Stock,
                                                   Imagen = Producto.Imagen
                                               }).ToList();
                    if(ProductosSucursales.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var dbProductoSucursal in ProductosSucursales)
                        {
                            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal{
                                Producto = new ML.Producto(), Sucursal = new ML.Sucursal()
                            };
                            productoSucursal.IdProductoSucursal = Convert.ToInt32(dbProductoSucursal.IdProductoSucursal);
                            productoSucursal.Producto.IdProducto = Convert.ToInt32(dbProductoSucursal.IdProducto);
                            productoSucursal.Producto.Nombre = dbProductoSucursal.NombreProducto;
                            productoSucursal.Producto.ImagenBase64 = dbProductoSucursal.Imagen != null ? Convert.ToBase64String(dbProductoSucursal.Imagen) : "";
                            productoSucursal.Sucursal.IdSucursal = Convert.ToInt32(dbProductoSucursal.IdSucursal);
                            productoSucursal.Sucursal.Nombre = dbProductoSucursal.NombreSucursal;
                            productoSucursal.Stock = Convert.ToInt32(dbProductoSucursal.Stok);

                            result.Objects.Add(productoSucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontraron Datos/Registros.";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateStock(int stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {

                }
            }catch(Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
