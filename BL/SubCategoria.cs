using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static ML.Result GetByIdCategoria(int IdCategoria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var SubCategorias = (from SubCategoria in context.SubCategorias
                                         where SubCategoria.IdCategoria == IdCategoria
                                         select new 
                                        {
                                            IdSubCategoria = SubCategoria.IdSubCategoria,
                                            Nombre = SubCategoria.Nombre
                                        }).ToList();
                    if(SubCategorias.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var dbSubCategoria in SubCategorias)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria();
                            subCategoria.IdSubCategoria = Convert.ToInt32(dbSubCategoria.IdSubCategoria);
                            subCategoria.Nombre = dbSubCategoria.Nombre;

                            result.Objects.Add(subCategoria);
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
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }
    }
}
