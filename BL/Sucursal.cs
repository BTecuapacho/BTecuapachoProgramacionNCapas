using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Sucursales = (from Sucursal in context.Sucursals
                                      select new
                                      {
                                          IdSucursal = Sucursal.IdSucursal,
                                          Nombre = Sucursal.Nombre
                                      }).ToList();
                    if (Sucursales.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var dbSucursal in Sucursales)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = Convert.ToInt32(dbSucursal.IdSucursal);
                            sucursal.Nombre = dbSucursal.Nombre;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
