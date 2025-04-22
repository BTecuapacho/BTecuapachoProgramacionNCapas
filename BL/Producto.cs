using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll(int IdCategoria, int IdSubCategoria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Productos = context.ProductoGetAll(IdCategoria, IdSubCategoria).ToList();
                    if(Productos.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var dbProducto in Productos)
                        {
                            ML.Producto producto = new ML.Producto { SubCategoria = new ML.SubCategoria { Categoria = new ML.Categoria() } };
                            producto.IdProducto = Convert.ToInt32(dbProducto.IdProducto);
                            producto.Nombre = dbProducto.NombreProducto;
                            producto.Descripcion = dbProducto.Descripcion;
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
    }
}
