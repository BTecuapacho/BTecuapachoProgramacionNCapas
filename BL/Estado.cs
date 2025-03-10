using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var estados = context.EstadoGetAll().ToList();
                    if(estados.Count() != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var estadoGet in estados) {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = estadoGet.IdEstado;
                            estado.Nombre = estadoGet.Nombre;
                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontraron registros/datos";
                    }
                }
            }
            catch (Exception ex) {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
