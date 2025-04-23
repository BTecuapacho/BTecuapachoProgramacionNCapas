using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Categorias = (from Categoria in context.Categorias
                                      select new
                                      {
                                          IdCategora = Categoria.IdCategoria,
                                          Nombre = Categoria.Nombre
                                      }).ToList();
                    if(Categorias.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var dbCategoria in Categorias)
                        {
                            ML.Categoria categoria = new ML.Categoria();
                            categoria.IdCategoria = Convert.ToInt32(dbCategoria.IdCategora);
                            categoria.Nombre = dbCategoria.Nombre;
                            result.Objects.Add(categoria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
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
