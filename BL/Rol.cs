using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var roles = context.RolGetAll().ToList();
                    if (roles.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var rolGet in roles) { 
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rolGet.IdRol;
                            rol.Nombre = rolGet.Nombre;
                            result.Objects.Add(rol);
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

        public static ML.Result GetByIdEF(int IdRol) { 
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var rolGet = context.RolGetById(IdRol).Single();
                    if (rolGet != null) {
                        result.Object = new ML.Rol()
                        {
                            IdRol = rolGet.IdRol,
                            Nombre = rolGet.Nombre,
                        };
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Rol no Encontrado";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
