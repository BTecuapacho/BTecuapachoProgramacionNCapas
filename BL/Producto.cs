using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll(ML.Producto productoIn)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Productos = context.ProductoGetAll(productoIn.SubCategoria.Categoria.IdCategoria, productoIn.SubCategoria.IdSubCategoria).ToList();
                    if(Productos.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var dbProducto in Productos)
                        {
                            ML.Producto producto = new ML.Producto { SubCategoria = new ML.SubCategoria { Categoria = new ML.Categoria() } };
                            producto.IdProducto = Convert.ToInt32(dbProducto.IdProducto);
                            producto.Nombre = dbProducto.NombreProducto;
                            producto.Descripcion = dbProducto.Descripcion;
                            producto.Precio = Convert.ToDecimal(dbProducto.Precio);
                            producto.ImagenBase64 = dbProducto.Imagen != null ? Convert.ToBase64String(dbProducto.Imagen) : "";
                            producto.SubCategoria.IdSubCategoria = Convert.ToInt32(dbProducto.IdProducto);
                            producto.SubCategoria.Nombre = dbProducto.NombreSubCategoria;
                            producto.SubCategoria.Categoria.IdCategoria = Convert.ToInt32(dbProducto.IdCategoria);
                            producto.SubCategoria.Categoria.Nombre = dbProducto.NombreCategoria;

                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontraron productos.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var dbProducto = context.ProductoGetById(IdProducto).Single();
                    if (dbProducto != null)
                    {
                        ML.Producto producto = new ML.Producto{
                            SubCategoria = new ML.SubCategoria
                            {
                                Categoria = new ML.Categoria()
                            }
                        };
                        producto.IdProducto = Convert.ToInt32(dbProducto.IdProducto);
                        producto.Nombre = dbProducto.NombreProducto;
                        producto.Descripcion = dbProducto.Descripcion;
                        producto.Precio = Convert.ToDecimal(dbProducto.Precio);
                        producto.Imagen = dbProducto.Imagen;
                        producto.ImagenBase64 = dbProducto.Imagen != null ? Convert.ToBase64String(dbProducto.Imagen) : "";
                        producto.SubCategoria.Categoria.IdCategoria = Convert.ToInt32(dbProducto.IdCategoria);
                        producto.SubCategoria.IdSubCategoria = Convert.ToInt32(dbProducto.IdSubCategoria);

                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontro el producto.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    int filasAfectados = context.ProductoAdd(producto.Nombre, producto.Descripcion, producto.Precio, producto.SubCategoria.IdSubCategoria, producto.Imagen);
                    if(filasAfectados != 0)
                    {
                        result.Correct = true;
                    }else
                    {
                        result.Correct = false; result.ErrorMessage = "El producto se inserto de manera correcta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    int filasAfectados = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Descripcion, producto.Precio, producto.SubCategoria.IdSubCategoria, producto.Imagen);
                    if (filasAfectados != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "El producto se actualizo de manera correcta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    int filasAfectados = context.ProductoDelete(IdProducto);
                    if (filasAfectados != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "El producto se elimino de manera correcta";
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
